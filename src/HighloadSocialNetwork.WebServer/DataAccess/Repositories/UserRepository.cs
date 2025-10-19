using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.DataAccess.Repositories;

public class UserRepository(IDatabase database) : IUserRepository
{
    public Task<UserInDb?> GetById(Guid id) =>
        database.GetOrDefault<UserInDb>("SELECT * FROM users WHERE id = @id", new { id });

    public Task<List<UserInDb>> Search(string firstName, string secondName) => database.GetList<UserInDb>(
        "SELECT * FROM users WHERE first_name LIKE @firstName AND second_name LIKE @secondName ORDER BY id",
        new { firstName = firstName + "%", secondName = secondName + "%" });
}