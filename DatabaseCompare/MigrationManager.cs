using System;
using System.Data.Entity;
using System.Linq;

namespace DatabaseCompare
{
    /// <summary>
    /// Migration Manager: Replacement for EF-Migrations (they don't work with SQLite)
    /// </summary>
    class MigrationManager
    {
        /// <summary>
        /// This is the current migration. On opening a database it will be transformed to this level!
        /// </summary>
        private const int CurrentMigration = 2;

        public MigrationManager()
        {
            // The database file always exists. If not it will be created on the fly
            using (var db = new DataContext())
            {
                int schemaVersion = GetSchemaVersion(db.Database);
                if (schemaVersion < CurrentMigration)
                {
                    applyMigrations(db, schemaVersion);
                }
            }
        }

        /// <summary>
        /// Ask the database about the current migration status
        /// </summary>
        /// <param name="db">A reference to a Entity.Data.Database Object</param>
        /// <returns>The current migration level</returns>
        private int GetSchemaVersion(Database db)
        {
            bool shouldCreateSchemaTable = false;
            int versionNumber = 0;

            try
            {
                versionNumber = db.SqlQuery<SchemaInfo>("SELECT Version from SchemaInfo ORDER BY Version DESC LIMIT 1").First().Version;
            }
            catch (Exception)
            {
                shouldCreateSchemaTable = true;
            }

            if (shouldCreateSchemaTable)
            {
                db.ExecuteSqlCommand("CREATE TABLE `SchemaInfo` (`Version` INTEGER NOT NULL, PRIMARY KEY(`Version`));");
                db.ExecuteSqlCommand("INSERT INTO `SchemaInfo`(`Version`) VALUES (0);");
                versionNumber = 0;
            }

            return versionNumber;
        }

        /// <summary>
        /// Apply all missing migrations. Add new migrations here
        /// </summary>
        /// <param name="db"></param>
        /// <param name="currentVersion"></param>
        private void applyMigrations(DataContext db, int currentVersion)
        {
            if (currentVersion > CurrentMigration)
            {
                throw new Exception("Invalid Migration Status");
            }

            // Insert the neccessary migrations here!
            if (currentVersion < 1)
            {
                MigrateTo01(db);
                currentVersion = 1;
            }

            if (currentVersion < 2)
            {
                MigrateTo02(db);
                currentVersion = 2;
            }
        }

        private void MigrateTo01(DataContext db)
        {
            // Create Table
            db.Database.ExecuteSqlCommand(@"CREATE TABLE `Columns` (`id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `DatabaseName` TEXT NOT NULL, `Schema` TEXT NOT NULL, `TableName` TEXT NOT NULL, `ColumnName` TEXT NOT NULL, `DataType` TEXT NOT NULL);");

            // Update Version in Database
            db.SchemaInfo.Add(new SchemaInfo { Version = 1 });
            db.SaveChanges();
        }

        /// <summary>
        /// Create a __MigrationHistory Table so that the Entity Framework feels happy. <b>This table will NEVER be used</b>, because Code First Migrations are not supported out-of-the-box with SQLite.
        /// </summary>
        /// <param name="db"></param>
        private void MigrateTo02(DataContext db)
        {
            // Create Table
            // Schema from http://forums.devart.com/viewtopic.php?p=103760
            db.Database.ExecuteSqlCommand(@"CREATE TABLE __MigrationHistory(MigrationId varchar(150) NOT NULL, ContextKey varchar(300) NOT NULL, Model blob NOT NULL, ProductVersion varchar(32) NOT NULL, PRIMARY KEY(MigrationId, ContextKey))");

            // Update Version in Database
            db.SchemaInfo.Add(new SchemaInfo { Version = 2 });
            db.SaveChanges();
        }

    }
}
