using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

public interface IUserRepository
{
    Task<UserInDb?> GetById(Guid id);
    Task<List<UserInDb>> Search(string firstName, string secondName);
}