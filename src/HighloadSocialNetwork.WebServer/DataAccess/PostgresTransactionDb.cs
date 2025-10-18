using Dapper;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.Exceptions;
using Npgsql;

namespace HighloadSocialNetwork.WebServer.DataAccess;

public sealed class PostgresTransactionDb : IDatabaseTransaction
{
    private bool _disposed;

    private readonly NpgsqlConnection _connection;
    private readonly NpgsqlTransaction _transaction;

    public PostgresTransactionDb(NpgsqlConnection connection)
    {
        _connection = connection;
        _transaction = _connection.BeginTransaction();
    }

    public async Task<T?> GetOrDefault<T>(string sql, object? parameters = null) =>
        await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters);

    public async Task<List<T>> GetList<T>(string sql, object? parameters = null) =>
        (await _connection.QueryAsync<T>(sql, parameters)).ToList();

    public async Task<int> Execute(string sql, object? parameters = null) =>
        await _connection.ExecuteAsync(sql, parameters);
    

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    internal Task CommitInnerTransaction() => _transaction.CommitAsync();
    
    internal Task RollbackInnerTransaction() => _transaction.RollbackAsync();

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                await _transaction.DisposeAsync();
                await _connection.DisposeAsync();
            }

            _disposed = true;
        }
    }

    public Task Rollback() => throw new RollbackException();

    public Task Rollback<T>(T? result) => throw new RollbackException<T> { Result = result };
}