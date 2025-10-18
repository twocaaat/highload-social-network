using HighloadSocialNetwork.WebServer.ApiContracts.User;

namespace HighloadSocialNetwork.WebServer.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse?> GetUserById(Guid id);
    Task<List<UserResponse>> Search(string firstName, string secondName);
}