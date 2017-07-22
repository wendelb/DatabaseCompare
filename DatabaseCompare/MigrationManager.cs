using System;
using System.Data.Entity;
using System.Linq;

namespace DatabaseCompare
{
    class MigrationManager
    {
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

        private int GetSchemaVersion(Database db)
        {
            bool shouldCreateSchemaTable = false;
            int versionNumber = 0;

            try
            {
                versionNumber = db.SqlQuery<SchemaInfo>("SELECT Version from SchemaInfo ORDER BY Version DESC LIMIT 1").First().Version;
            }
            catch (Exception ex)
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
