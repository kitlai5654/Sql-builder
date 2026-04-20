using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SqlBuilder.BuildQuery

{
    public class SqlQuery
    {
        private Table _from;
        private List<Column> _columns = new();
        private List<string> _join = new();
        private Condition _where;
        public static SqlQuery From(string table, string alias) => new SqlQuery().SetFrom(new Table(table, alias));

        public SqlQuery SetFrom(Table from) {
            _from = from;
            return this;
        }

        public SqlQuery Select(params Column[] columns) {
            _columns.AddRange(columns);
            return this;
        }

        public JoinClause InnerJoin(string table, string alias) {
            return new JoinClause(this, "INNER JOIN", new Table(table, alias));
        }

        public JoinClause LeftOuterJoin(string table, string alias)
        {
            return new JoinClause(this, "LEFT OUTER JOIN", new Table(table, alias));
        }

        public JoinClause RightOuterJoin(string table, string alias)
        {
            return new JoinClause(this, "RIGHT OUTER JOIN", new Table(table, alias));
        }

        public JoinClause FullOuterJoin(string table, string alias)
        {
            return new JoinClause(this, "FULL OUTER JOIN", new Table(table, alias));
        }

        public void AddJoin(string joinClause)
        {
            _join.Add(joinClause);
        }

        public SqlQuery Where(Condition condition)
        {
            _where = condition;
            return this;
        }

        public string Convert()
        {
            var sb = new StringBuilder();
            
            var selectClause = _columns.Count > 0 ? string.Join(", ", _columns) : "*";

            sb.AppendLine($"SELECT {selectClause}");
            sb.AppendLine($"FROM {_from}");

            foreach (var join in _join)
            {
                sb.AppendLine(join);
            }

            if (_where != null)
            {
                sb.AppendLine($"WHERE {_where}");
            }
            return sb.ToString();
        }
    }
}
