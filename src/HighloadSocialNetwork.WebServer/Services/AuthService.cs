using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HighloadSocialNetwork.WebServer.ApiContracts.Auth;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using HighloadSocialNetwork.WebServer.Exceptions;
using HighloadSocialNetwork.WebServer.Mappers.Interafaces;
using HighloadSocialNetwork.WebServer.Services.Interfaces;
using HighloadSocialNetwork.WebServer.Utils;
using Microsoft.IdentityModel.Tokens;

namespace HighloadSocialNetwork.WebServer.Services;

public class AuthService(IAuthRepository repository, IModelsMapper modelsMapper) : IAuthService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    
    public async Task<Guid> CreateUser(RegisterRequest request)
    {
        var userInDb = modelsMapper.ToUserInDb(request);
        var userLoginInDb = new UserLoginInDb
        {
            Id = userInDb.Id,
            Password = PasswordHasher.MakeHash(request.Password)
        };
        
        await repository.CreateUser(userInDb, userLoginInDb);
        return userInDb.Id;
    }

    public async Task<string> Login(LoginRequest request)
    {
        var userLogin = await repository.GetById(request.Id);
        if (userLogin is not null && PasswordHasher.Verify(request.Password, userLogin.Password))
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString())
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Auth__Jwt_SecretKey")!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            return _tokenHandler.WriteToken(token);
        }

        throw new LoginException();
    }
}