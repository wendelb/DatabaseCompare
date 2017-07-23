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
        private const int CurrentMigration = 1;

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
            }
        }

        private void MigrateTo01(DataContext db)
        {
            // Create Table
            db.Database.ExecuteSqlCommand("CREATE TABLE `DBSchema` (`id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `DatabaseName` TEXT NOT NULL, `Schema` TEXT NOT NULL, `TableName` TEXT NOT NULL, `FieldName` TEXT NOT NULL, `DataType` TEXT NOT NULL);");

            // Update Version in Database
            db.SchemaInfo.Add(new SchemaInfo { Version = 1 });
            db.SaveChanges();
        }
    }
}
