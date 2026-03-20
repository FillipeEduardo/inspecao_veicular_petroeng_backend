using FluentValidation;
using InspecaoVeicularPetroeng.API.Commands.AuthCommands;

namespace InspecaoVeicularPetroeng.API.Validators.AuthValidators;

public class CriarAdminCommandValidator : AbstractValidator<CriarAdminCommand>
{
    public CriarAdminCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress();

        RuleFor(x => x.Senha)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.NomeCompleto)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Telefone)
            .MinimumLength(10)
            .MaximumLength(11);
    }
}