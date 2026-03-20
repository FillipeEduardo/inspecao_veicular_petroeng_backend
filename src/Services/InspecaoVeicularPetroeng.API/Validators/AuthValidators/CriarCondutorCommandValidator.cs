using FluentValidation;
using InspecaoVeicularPetroeng.API.Commands.AuthCommands;

namespace InspecaoVeicularPetroeng.API.Validators.AuthValidators;

public class CriarCondutorCommandValidator : AbstractValidator<CriarCondutorCommand>
{
    public CriarCondutorCommandValidator()
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

        RuleFor(x => x.ContratoId)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}