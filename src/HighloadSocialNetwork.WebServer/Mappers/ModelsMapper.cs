using HighloadSocialNetwork.WebServer.ApiContracts.Auth;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using HighloadSocialNetwork.WebServer.Mappers.Interafaces;

namespace HighloadSocialNetwork.WebServer.Mappers;

public class ModelsMapper : IModelsMapper
{
    public UserInDb ToUserInDb(RegisterRequest request) => new()
    {
        Id = Guid.NewGuid(),
        FirstName = request.FirstName,
        SecondName = request.SecondName,
        Biography = request.Biography,
        Birthdate = request.Birthdate,
        City = request.City,
    };
}