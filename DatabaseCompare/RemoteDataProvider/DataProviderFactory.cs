using DatabaseCompare.Exceptions;
using System.Configuration;

namespace DatabaseCompare.RemoteDataProvider
{
    class DataProviderFactory
    {
        /// <summary>
        /// Create a Data Provider. This Factory is configured via App.config
        /// </summary>
        /// <param name="db">Reference to the DataContect</param>
        /// <returns></returns>
        public static GenericDataProvider createDataProvider(DataContext db)
        {
            string DataProvider = ConfigurationManager.AppSettings["RemoteDataProvider"];

            if (DataProvider == "postgres")
            {
                return new PostgreSQLDataProvider(db);
            }
            else if (DataProvider == "mysql")
            {
                return new MySQLDataProvider(db);
            }
            else
            {
                throw new InvlidConfigurationException("RemoteDataProvider", DataProvider);
            }
        }
    }
}
