using HighloadSocialNetwork.WebServer.ApiContracts.User;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using Riok.Mapperly.Abstractions;

namespace HighloadSocialNetwork.WebServer.Mappers.DbModels;

[Mapper]
public partial class UserInDbMapper
{
    [MapperIgnoreSource(nameof(UserInDb.Id))]
    public partial UserResponse ToUserResponse(UserInDb userInDb);
}