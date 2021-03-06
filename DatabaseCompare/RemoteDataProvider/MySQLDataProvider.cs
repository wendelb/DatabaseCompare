﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DatabaseCompare.RemoteDataProvider
{
    class MySQLDataProvider : GenericDataProvider
    {
        private MySqlConnection client;

        public MySQLDataProvider(DataContext db) : base(db)
        {
        }

        override protected void Connect()
        {
            client = new MySqlConnection();
            client.ConnectionString = this.ConnectionString;
            client.Open();
        }

        protected override void Disconnect()
        {
            client.Close();
            client.Dispose();
            client = null;
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

        protected override void LoadCheckConstraintsFromDatabase(string database)
        {
            throw new NotImplementedException();
        }

        override protected void LoadColumnsFromDatabase(string database)
        {
            // In MySQL all required information is inside the INFORMATION_SCHEMA Database
            client.ChangeDatabase("INFORMATION_SCHEMA");

            // Use a list to store the data in Memory and bulk insert it
            List<Columns> list = new List<Columns>();
            using (MySqlCommand command = client.CreateCommand())
            {
                command.CommandText = "SELECT TABLE_NAME, COLUMN_NAME, COLUMN_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @databaseName";
                command.Parameters.AddWithValue("@databaseName", database);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Columns { DatabaseName = database, Schema = "", TableName = reader.GetString(0), ColumnName = reader.GetString(1), DataType = reader.GetString(2) });
                    }
                }
            }

            // Now that all fetched rows are in our list, lets add them in bulk to the SQLite database
            db.Columns.AddRange(list);
        }

        protected override void LoadPKeysFromDatabase(string database)
        {
            throw new NotImplementedException();
        }

        protected override void LoadTablesFromDatabase(string database)
        {
            throw new NotImplementedException();
        }
    }
}
