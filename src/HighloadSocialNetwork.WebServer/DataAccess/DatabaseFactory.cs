using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

namespace HighloadSocialNetwork.WebServer.DataAccess;

public class DatabaseFactory(IDataSourceHolder dataSourceHolder) : IDatabaseFactory
{
    public IDatabase Create(string connectionString) =>
        new PostgresDb(dataSourceHolder.GetDataSource(connectionString));
}