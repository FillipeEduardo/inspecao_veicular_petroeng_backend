using FluentValidation;
using InspecaoVeicularPetroeng.API.Commands.Vistoria;

namespace InspecaoVeicularPetroeng.API.Validators.Vistoria;

public class CriarVistoriaCommandValidator : AbstractValidator<CriarVistoriaCommand>
{
    public CriarVistoriaCommandValidator()
    {
        RuleFor(x => x.Data)
            .NotEmpty();

        RuleFor(x => x.QuilometragemVeiculo)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.VeiculoId)
            .GreaterThan(0);

        RuleFor(x => x.Inspecoes)
            .NotEmpty();

        RuleForEach(x => x.Inspecoes)
            .ChildRules(inspecao =>
            {
                inspecao.RuleFor(x => x.ItemId)
                    .GreaterThan(0);

                inspecao.RuleFor(x => x.StatusId)
                    .GreaterThan(0);
            });
    }
}