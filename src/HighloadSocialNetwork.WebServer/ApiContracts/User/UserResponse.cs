namespace HighloadSocialNetwork.WebServer.ApiContracts.User;

public record UserResponse(
    Guid Id,
    string FirstName, 
    string SecondName,
    DateTime Birthdate,
    string? Biography,
    string City);