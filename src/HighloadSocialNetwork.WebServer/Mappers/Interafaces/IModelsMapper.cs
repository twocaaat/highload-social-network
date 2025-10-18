using HighloadSocialNetwork.WebServer.ApiContracts.Auth;
using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.Mappers.Interafaces;

public interface IModelsMapper
{
    UserInDb ToUserInDb(RegisterRequest request);
}