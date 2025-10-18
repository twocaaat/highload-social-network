using HighloadSocialNetwork.WebServer.ApiContracts.User;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using HighloadSocialNetwork.WebServer.Exceptions;
using HighloadSocialNetwork.WebServer.Mappers.Interafaces;
using HighloadSocialNetwork.WebServer.Services.Interfaces;

namespace HighloadSocialNetwork.WebServer.Services;

public class UserService(IUserRepository repository, IContractsMapper contractsMapper) : IUserService
{
    public async Task<UserResponse?> GetUserById(Guid id)
    {
        var user = await repository.GetById(id);
        if (user is null)
        {
            throw new EntityNotFoundException<UserInDb>();
        }
        
        return contractsMapper.ToUserResponse(user);
    }

    public async Task<List<UserResponse>> Search(string firstName, string secondName)
    {
        var users = await repository.Search(firstName, secondName);
        return users.Select(contractsMapper.ToUserResponse).ToList();
    }
}