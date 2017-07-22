using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCompare
{
    class DataContext : DbContext
    {
        public DbSet<SchemaInfo> SchemaInfo { get; set; }
        public DbSet<DBSchema> Fields { get; set; }
    }

    [Table("SchemaInfo")]
    class SchemaInfo
    {
        [Key]
        public int Version { get; set; }
    }

    [Table("DBSchema")]
    class DBSchema
    {
        [Key]
        public int id { get; set; }
        public string DatabaseName { get; set; }
        public string Schema { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string DataType { get; set; }

    }
}
