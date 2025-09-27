namespace HighloadSocialNetwork.WebServer.ApiContracts.Auth;

public record RegisterRequest(
    string FirstName, 
    string SecondName,
    DateTime Birthdate,
    string? Biography,
    string City,
    string Password);