using DatabaseCompare.Exceptions;
using System.Configuration;

namespace DatabaseCompare.RemoteDataProvider
{
    class DataProviderFactory
    {
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
