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
        /// <summary>
        /// The Connection String. To be used in child classes
        /// </summary>
        protected string ConnectionString;

        /// <summary>
        /// The Regex to be applied against Databases
        /// </summary>
        protected Regex MatchDatabasesRegex;

        /// <summary>
        /// Should there be a filter against the <see cref="MatchSchema"/> list?
        /// </summary>
        protected bool FilterSchema;

        /// <summary>
        /// The List to be the base to match Schemata against (if Child Class supports it)
        /// </summary>
        protected List<string> MatchSchema;

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

        /// <summary>
        /// Will be triggered, when all data has been collected an saving starts
        /// </summary>
        public Action OnSave { get; set; }

        public GenericDataProvider(DataContext db)
        {
            this.ConnectionString = ConfigurationManager.AppSettings["RemoteDataConnectionString"];
            this.db = db;
            this.MatchDatabasesRegex = new Regex(ConfigurationManager.AppSettings["RemoteDataDatabaseRegex"]);
            this.MatchSchema = Properties.Settings.Default.RemoteDataSchemaList.Cast<string>().ToList();
            this.FilterSchema = Properties.Settings.Default.RemoteDataFilterSchema;
        }

        protected abstract void Connect();
        protected abstract void Disconnect();

        protected abstract List<string> ListDatabases();

        protected abstract void LoadColumnsFromDatabase(string database);
        protected abstract void LoadTablesFromDatabase(string database);
        protected abstract void LoadPKeysFromDatabase(string database);

        public void RefreshColumns()
        {
            using (var Transaction = db.Database.BeginTransaction()) {
                try
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM Columns;");

                    // Connect -> List Databases -> Filter Databases -> Load Data for each filtered Database
                    this.Connect();
                    try
                    {
                        List<string> Databases = this.ListDatabases();
                        Databases = this.FilterDatabases(Databases);

                        // Tell the caller, we can now predct a progress
                        OnMaxKnown?.Invoke(Databases.Count);

                        for (int i = 0; i < Databases.Count; i++)
                        {
                            OnProgress?.Invoke(i + 1, Databases[i]);
                            LoadColumnsFromDatabase(Databases[i]);
                            LoadTablesFromDatabase(Databases[i]);
                            LoadPKeysFromDatabase(Databases[i]);
                        }

                        OnSave?.Invoke();
                        db.SaveChanges();
                        Transaction.Commit();
                    }
                    finally
                    {
                        this.Disconnect();
                    }
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                    throw;
                }
                finally
                {
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
