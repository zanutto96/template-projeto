using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador.Model
{
    public class Dictionary
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Type { get; set; }
        public int Length { get; set; }
        public bool IsNullable { get; set; }
    }

    public class Relation
    {
        public string PrimaryTableName { get; set; }
        public string PrimaryColumnName { get; set; }
        public string ReferenceTableName { get; set; }
        public string ReferenceColumnName { get; set; }
    }

    public class PrimaryKey
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
    }
}
