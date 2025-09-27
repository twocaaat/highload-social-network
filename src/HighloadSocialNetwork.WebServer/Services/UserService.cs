using HighloadSocialNetwork.WebServer.ApiContracts.User;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using HighloadSocialNetwork.WebServer.Exceptions;
using HighloadSocialNetwork.WebServer.Mappers.DbModels;
using HighloadSocialNetwork.WebServer.Services.Interfaces;

namespace HighloadSocialNetwork.WebServer.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<UserResponse?> GetUserById(Guid id)
    {
        var user = await repository.GetById(id);
        if (user is null)
        {
            throw new EntityNotFoundException<UserInDb>();
        }

        var mapper = new UserInDbMapper();
        return mapper.ToUserResponse(user);
    }
}