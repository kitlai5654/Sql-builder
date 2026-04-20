using System;
using System.Collections.Generic;
using System.Text;

namespace SqlBuilder.BuildQuery
{
    public class JoinClause
    {
        private readonly SqlQuery _sql;
        private readonly string _type;
        private readonly Table _tableName;

        public JoinClause(SqlQuery sql, string type, Table table)
        {
            _sql = sql;
            _type = type;
            _tableName = table;
        }

        public SqlQuery On(string left, string right)
        {
            _sql.AddJoin($"{_type} {_tableName} ON {left} = {right}");
            return _sql;
        }
    }

}
