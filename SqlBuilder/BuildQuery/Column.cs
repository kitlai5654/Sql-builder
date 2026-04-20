using System;
using System.Collections.Generic;
using System.Text;

namespace SqlBuilder.BuildQuery
{
    public class Column
    {
        public string TableAlias { get; }
        public string ColumnName { get; }
        public string ColumnAlias { get; }

        public Column(string tableAlias, string columnName, string columnAlias)
        {
            TableAlias = tableAlias;
            ColumnName = columnName;
            ColumnAlias = columnAlias;
        }

        public override string ToString()
        {
            return ColumnAlias == null ? $"{TableAlias}.{ColumnName}" : $"{TableAlias}.{ColumnName} AS {ColumnAlias}";
        }
    }

}
