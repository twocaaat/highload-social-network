using Npgsql;

namespace HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

public interface IDataSourceHolder
{
    NpgsqlDataSource GetDataSource(string connectionString);
}