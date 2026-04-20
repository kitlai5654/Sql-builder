# Sql-builder
This application is a lightweight SQL query builder written in C#. It generates SQL statements programmatically and it is designed to construct SQL queries and the query requirements are provided via a JSON file, which is parsed and converted into SQL.

Example file:
https://github.com/kitlai5654/Sql-builder/blob/master/Template.txt

JSON File Input Instruction
```json
{
  "from": {
    "table": "table name",
    "alias": "alias"
  },
  "select": [
    {
      "table": "table name",
      "column": "column name"
    },
    {
      "table": "table name",
      "column": "column name",
      "alias": "alias"
    }
  ],
  "joins": [
    {
      "type": "inner / left / right / full",
      "table": "table name",
      "alias": "alias",
      "left": "left value",
      "right": "right value"
    }
  ],
  "where": {
    "operator": "or / and",
    "conditions": [
      {
        "table": "table name",
        "column": "column name",
        "value": "value"
      }
    ]
  }
}
```

# Example JSON Input
```json
{
  "from": { "table": "Events", "alias": "e" },
  "select": [
    { "table": "e", "column": "Id" },
    { "table": "a", "column": "Name", "alias": "AttendeeName" }
  ],
  "joins": [
    {
      "type": "inner",
      "table": "EventAttendee",
      "alias": "ea",
      "left": "e.Id",
      "right": "ea.EventId"
    },
    {
      "type": "inner",
      "table": "Attendee",
      "alias": "a",
      "left": "ea.AttendeeId",
      "right": "a.Id"
    }
  ],
  "where": {
    "logic": "or",
    "conditions": [
      { "table": "a", "column": "Name", "value": "bob" },
      { "table": "e", "column": "Important", "value": "Y" }
    ]
  }
}
```

# Example Output
SELECT e.Id, a.Name AS AttendeeName
FROM Events e
INNER JOIN EventAttendee ea ON e.Id = ea.EventId
INNER JOIN Attendee a ON ea.AttendeeId = a.Id
WHERE (a.Name = 'bob' OR e.Important = 1)
