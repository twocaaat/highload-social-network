using FluentValidation;
using HighloadSocialNetwork.WebServer.ApiContracts.Auth;

namespace HighloadSocialNetwork.WebServer.ApiContracts.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MaximumLength(64);
    }
}