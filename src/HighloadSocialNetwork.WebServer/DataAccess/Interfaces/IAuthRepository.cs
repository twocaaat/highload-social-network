using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.DataAccess.Interfaces;

public interface IAuthRepository
{
    Task CreateUser(UserInDb user, UserLoginInDb userLogin);
}