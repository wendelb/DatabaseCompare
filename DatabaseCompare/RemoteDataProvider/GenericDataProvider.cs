﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;

namespace DatabaseCompare.RemoteDataProvider
{
    abstract class GenericDataProvider
    {
        /// <summary>
        /// The Connection String. To be used in child classes
        /// </summary>
        protected string ConnectionString;

        /// <summary>
        /// The Regex to be applied against Databases
        /// </summary>
        protected Regex MatchDatabasesRegex;

        /// <summary>
        /// The Regex to be applied against Schemas (if Child Class supports it)
        /// </summary>
        protected Regex MatchSchemaRegex;

        /// <summary>
        /// Reference to the Database
        /// </summary>
        protected DataContext db;

        /// <summary>
        /// Will be triggered when the list of databases has been read and filtered
        /// </summary>
        public Action<int> OnMaxKnown { get; set; }

        /// <summary>
        /// Informs the caller about the current progress
        /// * Param 1: Current Index (1-indexed)
        /// * Param 2: Current Database Name
        /// </summary>
        public Action<int, string> OnProgress { get; set; }

        public GenericDataProvider(DataContext db)
        {
            this.ConnectionString = ConfigurationManager.AppSettings["RemoteDataConnectionString"];
            this.db = db;
            this.MatchDatabasesRegex = new Regex(ConfigurationManager.AppSettings["RemoteDataDatabaseRegex"]);
            this.MatchSchemaRegex = new Regex(ConfigurationManager.AppSettings["RemoteDataSchemaRegex"]);
        }

        protected abstract void Connect();
        protected abstract void Disconnect();

        protected abstract List<string> ListDatabases();

        protected abstract void LoadColumnsFromDatabase(string database);

        public void RefreshColumns()
        {
            using (var Transaction = db.Database.BeginTransaction()) {
                try
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM DBSchema;");

                    // Connect -> List Databases -> Filter Databases -> Load Columns for each filtered Database
                    this.Connect();

                    List<string> Databases = this.ListDatabases();
                    Databases = this.FilterDatabases(Databases);

                    // Tell the caller, we can now predct a progress
                    OnMaxKnown?.Invoke(Databases.Count);

                    for (int i = 0; i < Databases.Count; i++)
                    {
                        OnProgress?.Invoke(i + 1, Databases[i]);
                        LoadColumnsFromDatabase(Databases[i]);
                    }

                    db.SaveChanges();
                    Transaction.Commit();

                }
                catch (Exception)
                {
                    Transaction.Rollback();
                    throw;
                }
                finally
                {
                    this.Disconnect();
                }
            }
        }

        private List<string> FilterDatabases(List<string> databases)
        {
            List<string> Result = databases.Where(item => (this.MatchDatabasesRegex.Match(item).Success)).ToList();
            return Result;
        }
    }
}
