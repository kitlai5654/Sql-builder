using System;
using System.Collections.Generic;
using System.Text;

namespace SqlBuilder.BuildQuery
{
    public class Table
    {
        public string Name { get; }
        public string Alias { get; }

        public Table(string name, string alias)
        {
            Name = name;
            Alias = alias;
        }

        public override string ToString()
        {
            return $"{Name} AS {Alias}";
        }
    }
}
