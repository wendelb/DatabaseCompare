using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DatabaseCompare
{
    public class DataContext : DbContext
    {
        public DataContext(): base()
        {
            // Set Journal to Memory after Initialization
            // As we can recover from any situation by just re-retrieving the Data from the Main Database, this will be fine!
            this.Database.Connection.Open();
            using (var command = this.Database.Connection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode = MEMORY;";
                command.ExecuteNonQuery();
            }
        }

        public DbSet<SchemaInfo> SchemaInfo { get; set; }
        public DbSet<Columns> Columns { get; set; }
    }

    [Table("SchemaInfo")]
    public class SchemaInfo
    {
        [Key]
        public int Version { get; set; }
    }

    [Table("Columns")]
    public class Columns
    {
        [Key]
        public int id { get; set; }
        public string DatabaseName { get; set; }
        public string Schema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }

    }
}
