using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador.Model
{
    public class TableToGenerate
    {
        public string TableName { get; set; }
        public bool Selected { get; set; }
        public bool GenerateFront { get; set; }
        public bool GenerateBack { get; set; }
        public bool GenerateMenu { get; set; }
        public bool GenerateFrontBasic { get; set; }
        public List<TableColum> ColumList { get; set; }
        public List<TableRelation> tableRelations { get; set; }

    }

    public class TableColum
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; }
        public bool IsForeignKey { get; set; }
    }

    public class TableRelation
    {
        public string PrimaryTableName { get; set; }
        public string PrimaryColumnName { get; set; }
        public string ReferenceTableName { get; set; }
        public string ReferenceColumnName { get; set; }
    }
}
