using HighloadSocialNetwork.WebServer.ApiContracts.Auth;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using HighloadSocialNetwork.WebServer.Mappers.ApiContracts;
using HighloadSocialNetwork.WebServer.Services.Interfaces;
using HighloadSocialNetwork.WebServer.Utils;

namespace HighloadSocialNetwork.WebServer.Services;

public class AuthService(IAuthRepository repository) : IAuthService
{
    public async Task<Guid> CreateUser(RegisterRequest request)
    {
        var mapper = new RegisterRequestMapper();
        var userId = Guid.NewGuid();
        
        var userInDb = mapper.ToUserInDb(request, userId);
        var userLoginInDb = new UserLoginInDb
        {
            Id = userId,
            Password = PasswordHasher.MakeHash(request.Password)
        };
        
        await repository.CreateUser(userInDb, userLoginInDb);
        return userId;
    }
}