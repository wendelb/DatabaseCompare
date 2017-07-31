using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        private DbSet<Columns> BaseCollection;

        public DisplayFilter(DbSet<Columns> BaseCollection)
        {
            this.BaseCollection = BaseCollection;

            // Initialize the filter -> Clear
            this.Clear();
        }

        public List<Columns> applyFilter()
        {
            if (!filterForDifferences)
            {
                IQueryable<Columns> Result = BaseCollection;
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
                    Result = Result.Where(i => (i.ColumnName == filterColumn));
                }

                // Sort in a logical way
                Result = Result.OrderBy(i => i.DatabaseName)
                    .ThenBy(i => i.Schema)
                    .ThenBy(i => i.TableName)
                    .ThenBy(i => i.ColumnName);

                return Result.ToList();
            }
            else
            {
                int NumberOfDatabases = BaseCollection.Select(i => i.DatabaseName).Distinct().Count();


                List<Columns> Result = BaseCollection.SqlQuery(@"SELECT d4.*
FROM Columns d4
INNER JOIN (
	-- Columns with different definitions
	SELECT d1.Schema, d1.TableName, d1.ColumnName, d1.DataType, count(*) as c
	FROM Columns d1
	INNER JOIN (
		-- Columns in all databases
		SELECT Schema, TableName, ColumnName
		FROM Columns
		GROUP BY Schema, TableName, ColumnName
		HAVING COUNT(*) = @numDB
	) d2 ON d1.Schema = d2.Schema AND d1.TableName = d2.TableName AND d1.ColumnName = d2.ColumnName
	GROUP BY d1.Schema, d1.TableName, d1.ColumnName, d1.DataType
	HAVING COUNT(*) < @numDB
) d3 ON d3.Schema = d4.Schema AND d3.TableName = d4.TableName AND d3.ColumnName = d4.ColumnName and d3.DataType = d4.DataType
ORDER BY d4.Schema, d4.TableName, d4.ColumnName, d4.DatabaseName, d4.DataType", new SQLiteParameter("numDB", NumberOfDatabases)).ToList();
                return Result;
            }
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
