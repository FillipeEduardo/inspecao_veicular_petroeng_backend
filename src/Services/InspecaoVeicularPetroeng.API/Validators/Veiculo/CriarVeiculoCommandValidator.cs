using FluentValidation;
using InspecaoVeicularPetroeng.API.Commands.Veiculo;

namespace InspecaoVeicularPetroeng.API.Validators.Veiculo;

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
    }
}