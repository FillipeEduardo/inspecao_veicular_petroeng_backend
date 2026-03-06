using FluentValidation;
using InspecaoVeicularPetroeng.API.Commands.Auth;

namespace InspecaoVeicularPetroeng.API.Validators.Auth;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Senha)
            .NotEmpty();
    }
}