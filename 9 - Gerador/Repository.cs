using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Gerador.Model;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;

namespace Gerador
{
    public enum DatabaseType
    {
        SqlServer,
        MySQL
    }

    public class Repository
    {
        private string cnn;
        private IDbConnection _connection;
        private DatabaseType _databaseType;
        
        public Repository(string connectionString)
        {
            cnn = connectionString;
            _databaseType = DetectDatabaseType(connectionString);
            _connection = CreateConnection(connectionString, _databaseType);
        }

        public Repository(string server, string database, string user, string password)
        {
            cnn = $"Data Source={server};Initial Catalog={database};user id={user};password={password};Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True";
            _databaseType = DatabaseType.SqlServer;
            _connection = new System.Data.SqlClient.SqlConnection(cnn);
        }

        private DatabaseType DetectDatabaseType(string connectionString)
        {
            string connStr = connectionString.ToLower();
            if (connStr.Contains("data source") && (connStr.Contains("initial catalog") || connStr.Contains("database")) && !connStr.Contains("server="))
            {
                return DatabaseType.SqlServer;
            }
            else if (connStr.Contains("server=") || connStr.Contains("host="))
            {
                return DatabaseType.MySQL;
            }
            else
            {
                // Default para SQL Server se não conseguir detectar
                return DatabaseType.SqlServer;
            }
        }

        private IDbConnection CreateConnection(string connectionString, DatabaseType dbType)
        {
            return dbType switch
            {
                DatabaseType.MySQL => new MySqlConnection(connectionString),
                DatabaseType.SqlServer => new System.Data.SqlClient.SqlConnection(connectionString),
                _ => new System.Data.SqlClient.SqlConnection(connectionString)
            };
        }

        public List<TableToGenerate> GetTableToGenerates()
        {
            List<TableToGenerate> list = new List<TableToGenerate>();

            var Dictionary = _databaseType == DatabaseType.MySQL 
                ? GetMySqlTableStructure() 
                : GetSqlServerTableStructure();

            var PrimaryKeys = _databaseType == DatabaseType.MySQL 
                ? GetMySqlPrimaryKeys() 
                : GetSqlServerPrimaryKeys();

            var Relations = _databaseType == DatabaseType.MySQL 
                ? GetMySqlRelations() 
                : GetSqlServerRelations();


            var tableList = Dictionary.Select(_ => _.TableName).Distinct().ToList();

            foreach (var t in tableList)
            {
                TableToGenerate tg = new TableToGenerate()
                {
                    TableName = t,
                    Selected = false,
                    GenerateFront = true,
                    GenerateBack = true,
                    GenerateMenu = true,
                    ColumList = new List<TableColum>(),
                    tableRelations = new List<TableRelation>()
                };

                var relationList = Relations.Where(_ => _.ReferenceTableName == t).ToList();
                foreach (var r in relationList)
                {
                    var tr = new TableRelation()
                    {
                        PrimaryTableName = r.PrimaryTableName,
                        PrimaryColumnName = r.PrimaryColumnName,
                        ReferenceTableName = r.ReferenceTableName,
                        ReferenceColumnName = r.ReferenceColumnName
                    };
                    tg.tableRelations.Add(tr);
                }

                

                var columnList = Dictionary.Where(_ => _.TableName == t).ToList(); 
                foreach (var c in columnList)
                {
                    var tc = new TableColum()
                    {
                        Name = c.ColumnName,
                        Type = c.Type,
                        Length = c.Length,
                        IsNullable = c.IsNullable
                    };

                    //Find Primary and Foreing Keys
                    var pk = PrimaryKeys.Where(_ => _.TableName == t && _.ColumnName == c.ColumnName).FirstOrDefault();
                    if (pk != null)
                        tc.IsPrimaryKey = true;

                    var fk = relationList.Where(_ => _.ReferenceColumnName == c.ColumnName).FirstOrDefault();
                    if (fk != null) 
                        tc.IsForeignKey = true;



                    tg.ColumList.Add(tc);

                }

                list.Add(tg);
            }
 

            return list;
        }

        private List<Dictionary> GetSqlServerTableStructure()
        {
            string cmd = " SELECT T.name as TableName, C.name as ColumnName, TY.name as Type, C.prec AS Length, IsNullable ";
            cmd += " FROM syscolumns C ";
            cmd += " INNER JOIN sysobjects T ON T.id = C.id AND T.xtype LIKE 'U' ";
            cmd += " INNER JOIN systypes TY ON C.usertype = TY.usertype AND C.xtype = TY.xtype ";
            cmd += " WHERE T.name <> 'sysdiagrams' ";
            cmd += " ORDER BY T.name, C.name ";
            return _connection.Query<Dictionary>(cmd, null, commandType: CommandType.Text).ToList();
        }

        private List<Dictionary> GetMySqlTableStructure()
        {
            string databaseName = ExtractMySqlDatabase();
            string cmd = @"
                SELECT 
                    t.TABLE_NAME as TableName, 
                    c.COLUMN_NAME as ColumnName, 
                    c.DATA_TYPE as Type, 
                    COALESCE(c.CHARACTER_MAXIMUM_LENGTH, c.NUMERIC_PRECISION, 0) as Length, 
                    CASE WHEN c.IS_NULLABLE = 'YES' THEN 1 ELSE 0 END as IsNullable
                FROM INFORMATION_SCHEMA.TABLES t
                INNER JOIN INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_NAME = c.TABLE_NAME AND t.TABLE_SCHEMA = c.TABLE_SCHEMA
                WHERE t.TABLE_SCHEMA = @databaseName AND t.TABLE_TYPE = 'BASE TABLE'
                ORDER BY t.TABLE_NAME, c.ORDINAL_POSITION";
            return _connection.Query<Dictionary>(cmd, new { databaseName }, commandType: CommandType.Text).ToList();
        }

        private List<PrimaryKey> GetSqlServerPrimaryKeys()
        {
            string cmd = " SELECT OBJECT_NAME(ic.OBJECT_ID) AS TableName, COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName ";
            cmd += " FROM sys.indexes AS i ";
            cmd += " INNER JOIN sys.index_columns AS ic ON i.OBJECT_ID = ic.OBJECT_ID AND i.index_id = ic.index_id ";
            cmd += " WHERE i.is_primary_key = 1 ";
            cmd += " AND OBJECT_NAME(ic.OBJECT_ID) <> 'sysdiagrams' ";
            return _connection.Query<PrimaryKey>(cmd, null, commandType: CommandType.Text).ToList();
        }

        private List<PrimaryKey> GetMySqlPrimaryKeys()
        {
            string databaseName = ExtractMySqlDatabase();
            string cmd = @"
                SELECT 
                    c.TABLE_NAME as TableName, 
                    c.COLUMN_NAME as ColumnName
                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
                WHERE c.TABLE_SCHEMA = @databaseName AND c.CONSTRAINT_NAME = 'PRIMARY'";
            return _connection.Query<PrimaryKey>(cmd, new { databaseName }, commandType: CommandType.Text).ToList();
        }

        private List<Relation> GetSqlServerRelations()
        {
            string cmd = " SELECT ForeignKeys.NAME [ForeignKeyName], ";
            cmd += " PrimaryKeyTable.NAME [PrimaryTableName], ";
            cmd += " PrimaryKeyColumn.NAME [PrimaryColumnName], ";
            cmd += " ForeignKeyTable.NAME [ReferenceTableName], ";
            cmd += " ForeignKeyColumn.NAME [ReferenceColumnName] ";
            cmd += " FROM sys.foreign_keys ForeignKeys ";
            cmd += " JOIN sys.foreign_key_columns ForeignKeyRelationships ON ( ForeignKeys.object_id = ForeignKeyRelationships.constraint_object_id ) ";
            cmd += " JOIN sys.tables ForeignKeyTable ON ForeignKeyRelationships.parent_object_id = ForeignKeyTable.object_id ";
            cmd += " JOIN sys.columns ForeignKeyColumn ON ( ForeignKeyTable.object_id = ForeignKeyColumn.object_id AND ForeignKeyRelationships.parent_column_id = ForeignKeyColumn.column_id ) ";
            cmd += " JOIN sys.tables PrimaryKeyTable ON ForeignKeyRelationships.referenced_object_id = PrimaryKeyTable.object_id ";
            cmd += " JOIN sys.columns PrimaryKeyColumn ON ( PrimaryKeyTable.object_id = PrimaryKeyColumn.object_id AND ForeignKeyRelationships.referenced_column_id = PrimaryKeyColumn.column_id ) ";
            cmd += " ORDER BY ForeignKeys.NAME ";
            return _connection.Query<Relation>(cmd, null, commandType: CommandType.Text).ToList();
        }

        private List<Relation> GetMySqlRelations()
        {
            string databaseName = ExtractMySqlDatabase();
            string cmd = @"
                SELECT 
                    kcu.CONSTRAINT_NAME as ForeignKeyName,
                    kcu.REFERENCED_TABLE_NAME as PrimaryTableName,
                    kcu.REFERENCED_COLUMN_NAME as PrimaryColumnName,
                    kcu.TABLE_NAME as ReferenceTableName,
                    kcu.COLUMN_NAME as ReferenceColumnName
                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
                WHERE kcu.TABLE_SCHEMA = @databaseName 
                AND kcu.REFERENCED_TABLE_NAME IS NOT NULL
                ORDER BY kcu.CONSTRAINT_NAME";
            return _connection.Query<Relation>(cmd, new { databaseName }, commandType: CommandType.Text).ToList();
        }

        private string ExtractMySqlDatabase()
        {
            try
            {
                var parts = cnn.Split(';');
                foreach (var part in parts)
                {
                    var keyValue = part.Split('=');
                    if (keyValue.Length == 2)
                    {
                        var key = keyValue[0].Trim().ToLower();
                        if (key == "database" || key == "initial catalog")
                        {
                            return keyValue[1].Trim();
                        }
                    }
                }
            }
            catch
            {
                // Se não conseguir extrair, usa um nome padrão
            }
            return "test";
        }
    }
}
