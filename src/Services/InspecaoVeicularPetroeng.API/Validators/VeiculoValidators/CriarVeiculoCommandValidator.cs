using FluentValidation;
using InspecaoVeicularPetroeng.API.Commands.VeiculoCommands;

namespace InspecaoVeicularPetroeng.API.Validators.VeiculoValidators;

public class CriarVeiculoCommandValidator : AbstractValidator<CriarVeiculoCommand>
{
    public CriarVeiculoCommandValidator()
    {
        RuleFor(x => x.Placa)
            .NotEmpty()
            .Length(7);

        RuleFor(x => x.Ano)
            .InclusiveBetween(1900, DateTime.UtcNow.Year + 1);

        RuleFor(x => x.Modelo)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ContratoId)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}