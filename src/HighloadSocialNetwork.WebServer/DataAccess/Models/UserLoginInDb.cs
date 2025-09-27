namespace HighloadSocialNetwork.WebServer.DataAccess.Models;

public class UserLoginInDb
{
    public Guid Id { get; set; }
    public string Password { get; set; } = null!;
}