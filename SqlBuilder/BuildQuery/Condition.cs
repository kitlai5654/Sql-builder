using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SqlBuilder.BuildQuery
{
    public class Condition
    {
        private readonly string _sql;
        private Condition(string sql) { 
            _sql = sql;
        }

        public static Condition Equal(string tableAlias, string columnName, object value)
        {
            return new Condition($"{tableAlias}.{columnName} = {FormatValue(value)}");
        }

        private static string FormatValue(object value)
        {
            if (value is JsonElement json)
            {
                switch (json.ValueKind)
                {
                    case JsonValueKind.String:
                        var str = json.GetString();

                        if (string.IsNullOrEmpty(str))
                            return "NULL";

                        // try bool inside string
                        if (bool.TryParse(str, out bool parsedBool))
                            return parsedBool ? "1" : "0";

                        return $"'{str.Replace("'", "''")}'";

                    case JsonValueKind.Number:
                        return json.ToString();

                    case JsonValueKind.True:
                        return "1";

                    case JsonValueKind.False:
                        return "0";

                    case JsonValueKind.Null:
                    case JsonValueKind.Undefined:
                        return "NULL";
                }
            }
            return value.ToString();
        }

        public static Condition And(params Condition[] conditions)
        {
            return new Condition("(" + string.Join(" AND ", conditions.Select(con => con.ToString())) + ")");
        }

        public static Condition Or(params Condition[] conditions)
        {
            var test = conditions.Select(con => con.ToString()).ToList();
            return new Condition("(" + string.Join(" OR ", conditions.Select(con => con.ToString())) + ")");
        }

        public override string ToString()
        {
            return _sql;
        }
    }
}
