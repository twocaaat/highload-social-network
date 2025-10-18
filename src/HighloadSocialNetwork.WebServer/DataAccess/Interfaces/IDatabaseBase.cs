namespace HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

public interface IDatabaseBase
{
    Task<T?> GetOrDefault<T>(string sql, object? parameters = null);
    Task<List<T>> GetList<T>(string sql, object? parameters = null);
    Task<int> Execute(string sql, object? parameters = null);
}