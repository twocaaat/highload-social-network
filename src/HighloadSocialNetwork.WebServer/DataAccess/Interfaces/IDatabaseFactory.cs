namespace HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

public interface IDatabaseFactory
{
    public IDatabase Create(string connectionString);
}