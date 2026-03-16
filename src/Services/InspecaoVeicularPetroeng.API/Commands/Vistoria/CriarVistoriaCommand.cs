using InspecaoVeicularPetroeng.Domain.Entities;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;

namespace InspecaoVeicularPetroeng.API.Commands.Vistoria;

public class CriarVistoriaCommand : IRequest<Result>
{
    public DateTime Data { get; set; }
    public double QuilometragemVeiculo { get; set; }
    public int VeiculoId { get; set; }
    public List<Inspecao> Inspecoes { get; set; } = [];

    public static implicit operator Domain.Entities.Vistoria(CriarVistoriaCommand command)
    {
        return new Domain.Entities.Vistoria
        {
            VeiculoId = command.VeiculoId,
            Data = command.Data,
            QuilometragemVeiculo = command.QuilometragemVeiculo,
            Inspecoes = command.Inspecoes
        };
    }
}

public class CriarVistoriaCommandHandler(AppDbContext context) : IRequestHandler<CriarVistoriaCommand, Result>
{
    public async Task<Result> Handler(CriarVistoriaCommand request, CancellationToken cancellationToken)
    {
    }
}