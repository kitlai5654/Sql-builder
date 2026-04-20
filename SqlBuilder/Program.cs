using SqlBuilder.BuildQuery;
using SqlBuilder.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter the Json File Path");
        var path = Console.ReadLine();
        
        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var input = JsonSerializer.Deserialize<JsonInput>(json, options);

        // SQL query -- from
        var sql  = SqlQuery.From(input.From.Table, input.From.Alias);

        //SELECT
        var columns = new List<Column>();
        foreach (var column in input.Select)
        {
            columns.Add(new Column(column.Table, column.Column, column.Alias));
        }
        sql = sql.Select(columns.ToArray());

        Console.WriteLine(sql);

        //JOINS
        if (input.Join != null) { 
            foreach (var join in input.Join)
            {
                switch (join.Type.ToUpper())
                {
                    case "INNER":
                        sql = sql.InnerJoin(join.Table, join.Alias).On(join.LeftColumn, join.RightColumn);
                        break;
                    case "LEFT":
                        sql = sql.LeftOuterJoin(join.Table, join.Alias).On(join.LeftColumn, join.RightColumn);
                        break;
                    case "RIGHT":
                        sql = sql.RightOuterJoin(join.Table, join.Alias).On(join.LeftColumn, join.RightColumn);
                        break;
                    case "FULL":
                        sql = sql.FullOuterJoin(join.Table, join.Alias).On(join.LeftColumn, join.RightColumn);
                        break;
                } 
            }
        }

        //WHERE
        if (input.Where?.Conditions != null) {
            var conditions = new List<Condition>();

            foreach (var condition in input.Where.Conditions)
            {
                conditions.Add(Condition.Equal(condition.Table, condition.Column, condition.Value));
            }

            switch (input.Where.Operator.ToUpper())
            {
                case "AND":
                    var andCondition = Condition.And(conditions.ToArray());
                    sql = sql.Where(andCondition);
                    break;
                case "OR":
                    var orCondition = Condition.Or(conditions.ToArray());
                    sql = sql.Where(orCondition);
                    break;
            }
        }

        //Convert to SQL string
        Console.WriteLine("GENERATED--\n");
        Console.WriteLine(sql.Convert());

    }
}