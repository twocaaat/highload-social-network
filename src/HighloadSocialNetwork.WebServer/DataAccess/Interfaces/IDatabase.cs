namespace HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

public interface IDatabase : IDatabaseBase
{
    Task DoInTransaction(Func<IDatabaseTransaction, Task> action);
}