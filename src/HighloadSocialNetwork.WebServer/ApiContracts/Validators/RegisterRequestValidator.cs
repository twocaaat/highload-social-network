using FluentValidation;
using HighloadSocialNetwork.WebServer.ApiContracts.Auth;

namespace HighloadSocialNetwork.WebServer.ApiContracts.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(64);
        RuleFor(x => x.SecondName).NotEmpty().MaximumLength(64);
        RuleFor(x => x.City).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Biography).MaximumLength(1024);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Birthdate)
            .NotEmpty()
            .LessThan(DateTime.Today)
            .GreaterThan(DateTime.Today.AddYears(-120));
    }
}