namespace HighloadSocialNetwork.WebServer.DataAccess.Models;

public class UserInDb
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
    public string Biography { get; set; } = null!;
    public string City { get; set; } = null!;
}