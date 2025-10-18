using HighloadSocialNetwork.WebServer.ApiContracts.User;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using HighloadSocialNetwork.WebServer.Mappers.Interafaces;

namespace HighloadSocialNetwork.WebServer.Mappers;

public class ContractsMapper : IContractsMapper
{
    public UserResponse ToUserResponse(UserInDb userInDb) => new(
        userInDb.Id,
        userInDb.FirstName,
        userInDb.SecondName,
        userInDb.Birthdate,
        userInDb.Biography,
        userInDb.City);
}