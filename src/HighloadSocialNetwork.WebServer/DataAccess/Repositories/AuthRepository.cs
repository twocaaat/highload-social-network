using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.DataAccess.Repositories;

public class AuthRepository(IDatabase _database) : IAuthRepository
{
    private const string InsertUserQuery =
        "INSERT INTO users(id, first_name, second_name, birthdate, biography, city) " +
        "VALUES (@Id, @FirstName, @SecondName, @Birthdate, @Biography, @City)";
    
    private const string InsertUserLoginQuery = "INSERT INTO users_logins(id, password) VALUES (@Id, @Password)";
    
    public Task CreateUser(UserInDb user, UserLoginInDb userLogin) =>
        _database.DoInTransaction(async transaction =>
        {
            await transaction.Execute(InsertUserQuery, user);
            await transaction.Execute(InsertUserLoginQuery, userLogin);
        });
}