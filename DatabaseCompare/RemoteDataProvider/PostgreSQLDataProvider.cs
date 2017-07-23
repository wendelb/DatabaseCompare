using System.Collections.Generic;
using Npgsql;

namespace DatabaseCompare.RemoteDataProvider
{
    class PostgreSQLDataProvider : GenericDataProvider
    {

        private NpgsqlConnection client;

        public PostgreSQLDataProvider(DataContext db) : base(db)
        {
        }

        protected override void Connect()
        {
            client = new NpgsqlConnection();
            client.ConnectionString = this.ConnectionString;
            client.Open();
        }

        protected override void Disconnect()
        {
            client.Close();
            client.Dispose();
            client = null;
        }

        protected override List<string> ListDatabases()
        {
            List<string> result = new List<string>();

            using (NpgsqlCommand command = client.CreateCommand())
            {
                command.CommandText = "SELECT datname FROM pg_database WHERE datistemplate = false and datallowconn = true;";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetValue(0).ToString());
                    }
                }

            }
            return result;
        }

        protected override void LoadColumnsFromDatabase(string database)
        {
            // In PostgreSQL you have to connect to every database to read the Schema
            client.ChangeDatabase(database);

            using (NpgsqlCommand command = client.CreateCommand())
            {
                command.CommandText = @"SELECT c.table_schema, c.table_name, c.column_name, CASE WHEN character_maximum_length is null THEN c.data_type ELSE CONCAT(c.data_type, '(', c.character_maximum_length, ')') END as data_type
FROM INFORMATION_SCHEMA.COLUMNS c
INNER JOIN INFORMATION_SCHEMA.TABLES t USING (table_catalog, table_schema, table_name)
WHERE c.table_schema not in ('pg_catalog', 'information_schema') and t.table_type = 'BASE TABLE'
ORDER BY table_schema, table_name, column_name, ordinal_position";

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Filter for Schema-Regex
                        if (this.MatchSchemaRegex.Match(reader.GetString(0)).Success)
                        {
                            db.Fields.Add(new DBSchema { DatabaseName = database, Schema = reader.GetString(0), TableName = reader.GetString(1), FieldName = reader.GetString(2), DataType = reader.GetString(3) });
                        }
                    }
                }
            }
        }
    }
}
