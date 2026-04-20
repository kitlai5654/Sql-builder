using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SqlBuilder.Model
{
    public class JsonInput
    {
        public FromInput From { get; set; }
        public List<SelectInput> Select { get; set; }
        public List<JoinInput> Join { get; set; }
        public WhereInput Where { get; set; }
    }

    public class FromInput
    {
        public string Table { get; set; }
        public string Alias { get; set; }
    }

    public class SelectInput
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public string Alias { get; set; }
    }

    public class JoinInput
    {
        public string Type { get; set; }
        public string Table { get; set; }
        public string Alias { get; set; }
        public string LeftColumn { get; set; }
        public string RightColumn { get; set; }
    }

    public class WhereInput
    {
        public string Operator { get; set; }
        public List<ConditionInput> Conditions { get; set; }
    }

    public class ConditionInput
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public object Value { get; set; }
    }
}
