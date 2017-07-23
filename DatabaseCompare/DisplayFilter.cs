using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCompare
{
    class DisplayFilter
    {
        public string filterSchema { get; set; }
        public string filterTable { get; set; }
        public string filterColumn { get; set; }

        private DbSet<DBSchema> BaseCollection;

        public DisplayFilter(DbSet<DBSchema> BaseCollection)
        {
            this.BaseCollection = BaseCollection;

            // Initialize the filter -> Clear
            this.Clear();
        }

        public IQueryable<DBSchema> applyFilter()
        {
            IQueryable<DBSchema> Result = BaseCollection;

            if (filterSchema != null)
            {
                Result = Result.Where(i => (i.Schema == filterSchema));
            }

            if (filterTable != null)
            {
                Result = Result.Where(i => (i.TableName == filterTable));
            }

            if (filterColumn != null)
            {
                Result = Result.Where(i => (i.FieldName == filterColumn));
            }

            return Result;
        }

        public void Clear()
        {
            // Clear the filter -> NULL
            filterSchema = null;
            filterTable = null;
            filterColumn = null;

        }
    }
}
