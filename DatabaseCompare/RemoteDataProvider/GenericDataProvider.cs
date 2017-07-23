using System;
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
        protected string ConnectionString;
        protected Regex MatchDatabasesRegex;
        protected Regex MatchSchemaRegex;
        protected DataContext db;

        public Action<int> OnMaxKnown { get; set; }
        public Action<int, string> OnProgress { get; set; }

        public GenericDataProvider(DataContext db)
        {
            this.ConnectionString = ConfigurationManager.AppSettings["RemoteDataConnectionString"];
            this.db = db;
            this.MatchDatabasesRegex = new Regex(ConfigurationManager.AppSettings["RemoteDataDatabaseRegex"]);
            this.MatchSchemaRegex = new Regex(ConfigurationManager.AppSettings["RemoteDataSchemaRegex"]);
        }

        protected abstract void Connect();

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
            }
        }

        private List<string> FilterDatabases(List<string> databases)
        {
            List<string> Result = databases.Where(item => (this.MatchDatabasesRegex.Match(item).Success)).ToList();
            return Result;
        }
    }
}
