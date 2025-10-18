using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

public interface IFakeRepository
{
    Task<int> GetCountOfUsers();
    Task InsertUsers(List<UserInDb> users);
}