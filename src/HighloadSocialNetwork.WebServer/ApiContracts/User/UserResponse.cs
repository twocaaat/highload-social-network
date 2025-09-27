namespace HighloadSocialNetwork.WebServer.ApiContracts.User;

public record UserResponse(
    string FirstName, 
    string SecondName,
    DateTime Birthdate,
    string? Biography,
    string City);