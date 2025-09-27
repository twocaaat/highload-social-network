using System.Collections.Concurrent;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using Npgsql;

namespace HighloadSocialNetwork.WebServer.DataAccess;

public class DataSourceHolder : IDataSourceHolder
{
    private readonly ConcurrentDictionary<string, NpgsqlDataSource> _dataSources = new();

    public NpgsqlDataSource GetDataSource(string connectionString) => _dataSources.AddOrUpdate(connectionString,
        cs => new NpgsqlDataSourceBuilder(cs).Build(), (_, old) => old);
}