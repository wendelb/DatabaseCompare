using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DatabaseCompare.RemoteDataProvider
{
    class MySQLDataProvider : GenericDataProvider
    {
        private MySqlConnection client;

        public MySQLDataProvider(DataContext db) : base(db)
        {
            client = new MySqlConnection();
        }

        override protected void Connect()
        {
            client.ConnectionString = this.ConnectionString;
            client.Open();
        }

        override protected List<string> ListDatabases()
        {
            List<string> result = new List<string>();

            using (MySqlCommand command = client.CreateCommand())
            {
                command.CommandText = "SHOW DATABASES";

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetValue(0).ToString());
                    }
                }

            }
            return result;
        }

        override protected void LoadColumnsFromDatabase(string database)
        {
            client.ChangeDatabase("INFORMATION_SCHEMA");

            using (MySqlCommand command = client.CreateCommand())
            {
                command.CommandText = "SELECT TABLE_NAME, COLUMN_NAME, COLUMN_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @databaseName";
                command.Parameters.AddWithValue("@databaseName", database);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        db.Fields.Add(new DBSchema { DatabaseName = database, Schema = "", TableName = reader.GetString(0), FieldName = reader.GetString(1), DataType = reader.GetString(2) });
                    }
                }
            }

        }
    }
}
