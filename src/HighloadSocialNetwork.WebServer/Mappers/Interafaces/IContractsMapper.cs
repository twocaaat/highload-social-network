using HighloadSocialNetwork.WebServer.ApiContracts.User;
using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.Mappers.Interafaces;

public interface IContractsMapper
{
    UserResponse ToUserResponse(UserInDb userInDb);
}