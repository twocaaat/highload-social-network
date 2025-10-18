using Dapper;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.Exceptions;
using Npgsql;

namespace HighloadSocialNetwork.WebServer.DataAccess;

public class PostgresDb(NpgsqlDataSource dataSource) : IDatabase
{
    public async Task<T?> GetOrDefault<T>(string sql, object? parameters = null)
    {
        await using var connection = await dataSource.OpenConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
    }
    
    public async Task<List<T>> GetList<T>(string sql, object? parameters = null)
    {
        await using var connection = await dataSource.OpenConnectionAsync();
        return (await connection.QueryAsync<T>(sql, parameters)).ToList();
    }

    public async Task<int> Execute(string sql, object? parameters = null)
    {
        await using var connection = await dataSource.OpenConnectionAsync();
        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DoInTransaction(Func<IDatabaseTransaction, Task> action)
    {
        await using var transaction = new PostgresTransactionDb(await dataSource.OpenConnectionAsync());

        try
        {
            await action(transaction);
            await transaction.CommitInnerTransaction();
        }
        catch(RollbackException)
        {
            await transaction.RollbackInnerTransaction();
        }
    }
}