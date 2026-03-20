using FluentValidation;
using InspecaoVeicularPetroeng.API.Commands.ContratoCommands;

namespace InspecaoVeicularPetroeng.API.Validators.ContratoValidators;

public class CriarContratoCommandValidator : AbstractValidator<CriarContratoCommand>
{
    public CriarContratoCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MaximumLength(100);
    }
}