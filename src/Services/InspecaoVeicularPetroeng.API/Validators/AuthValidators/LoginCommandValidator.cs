using FluentValidation;
using InspecaoVeicularPetroeng.API.Commands.AuthCommands;

namespace InspecaoVeicularPetroeng.API.Validators.AuthValidators;

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