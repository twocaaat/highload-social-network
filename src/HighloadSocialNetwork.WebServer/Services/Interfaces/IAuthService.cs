using HighloadSocialNetwork.WebServer.ApiContracts.Auth;

namespace HighloadSocialNetwork.WebServer.Services.Interfaces;

public interface IAuthService
{
    Task<Guid> CreateUser(RegisterRequest request);
}