using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.DataAccess.Repositories;

public class UserRepository(IDatabase database) : IUserRepository
{
    public Task<UserInDb?> GetById(Guid id) =>
        database.GetOrDefault<UserInDb>("SELECT * FROM users WHERE id = @id", new { id });
}