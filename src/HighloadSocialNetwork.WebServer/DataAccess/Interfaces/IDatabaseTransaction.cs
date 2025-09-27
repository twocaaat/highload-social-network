namespace HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

public interface IDatabaseTransaction : IDatabaseBase, IAsyncDisposable
{
    Task Rollback();
    Task Rollback<T>(T? result);
}