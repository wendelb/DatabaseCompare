using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;

namespace DatabaseCompare
{
    class DisplayFilter
    {
        public string filterSchema { get; set; }
        public string filterTable { get; set; }
        public string filterColumn { get; set; }
        public bool filterForDifferences { get; set; }

        private DbSet<DBSchema> BaseCollection;

        public DisplayFilter(DbSet<DBSchema> BaseCollection)
        {
            this.BaseCollection = BaseCollection;

            // Initialize the filter -> Clear
            this.Clear();
        }

        public IEnumerable<DBSchema> applyFilter()
        {
            IEnumerable<DBSchema> Result = BaseCollection;
            if (!filterForDifferences)
            {
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

                // Sort in a logical way
                Result = Result.OrderBy(i => i.DatabaseName)
                    .ThenBy(i => i.Schema)
                    .ThenBy(i => i.TableName)
                    .ThenBy(i => i.FieldName);
            }
            else {
                int NumberOfDatabases = BaseCollection.Select(i => i.DatabaseName).Distinct().Count();

                Result = BaseCollection.SqlQuery(@"SELECT d4.DatabaseName, d4.Schema, d4.TableName, d4.FieldName, d4.DataType
FROM DBschema d4
INNER JOIN (
	-- Columns with different definitions
	SELECT d1.Schema, d1.TableName, d1.FieldName, d1.DataType, count(*) as c
	FROM DBschema d1
	INNER JOIN (
		-- Columns in all databases
		SELECT Schema, TableName, FieldName
		FROM DBschema
		GROUP BY Schema, TableName, FieldName
		HAVING COUNT(*) = @numDB
	) d2 ON d1.Schema = d2.Schema AND d1.TableName = d2.TableName AND d1.FieldName = d2.FieldName
	GROUP BY d1.Schema, d1.TableName, d1.FieldName, d1.DataType
	HAVING COUNT(*) < @numDB
) d3 ON d3.Schema = d4.Schema AND d3.TableName = d4.TableName AND d3.FieldName = d4.FieldName and d3.DataType = d4.DataType
ORDER BY d4.Schema, d4.TableName, d4.FieldName, d4.DatabaseName, d4.DataType", new SQLiteParameter("@numDB", NumberOfDatabases));
            }

            return Result;
        }

        public void Clear()
        {
            // Clear the filter -> NULL
            filterSchema = null;
            filterTable = null;
            filterColumn = null;
            filterForDifferences = false;
        }
    }
}
