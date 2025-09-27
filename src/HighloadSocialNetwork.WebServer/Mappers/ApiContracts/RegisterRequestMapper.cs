using HighloadSocialNetwork.WebServer.ApiContracts.Auth;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using Riok.Mapperly.Abstractions;

namespace HighloadSocialNetwork.WebServer.Mappers.ApiContracts;

[Mapper]
public partial class RegisterRequestMapper
{
    [MapperIgnoreSource(nameof(RegisterRequest.Password))]
    public partial UserInDb ToUserInDb(RegisterRequest request, Guid id);
}