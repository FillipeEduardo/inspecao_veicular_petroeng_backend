using System.Net;
using InspecaoVeicularPetroeng.Domain.Entities;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Commands.Vistoria;

public class CriarVistoriaCommand : IRequest<Result>
{
    public DateTime Data { get; set; }
    public double QuilometragemVeiculo { get; set; }
    public Domain.Entities.Veiculo Veiculo { get; set; } = null!;
    public List<Inspecao> Inspecoes { get; set; } = [];

    public static implicit operator Domain.Entities.Vistoria(CriarVistoriaCommand command)
    {
        return new Domain.Entities.Vistoria
        {
            VeiculoId = command.Veiculo.Id,
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
        foreach (var inspecao in request.Inspecoes)
        {
            var existeItemDaInspecao = await context.Itens.AnyAsync(i => i.Id == inspecao.Item.Id, cancellationToken);
            if (!existeItemDaInspecao)
                return new ErrorResult(["Um dos itens da inspeção não existe."], HttpStatusCode.BadRequest);
            inspecao.ItemId = inspecao.Item.Id;

            var existeStatusDaInspecao =
                await context.StatusInspecao.AnyAsync(st => st.Id == inspecao.Status.Id, cancellationToken);
            if (!existeStatusDaInspecao)
                return new ErrorResult(["Um dos status da inspeção não existe."], HttpStatusCode.BadRequest);
            inspecao.StatusId = inspecao.Status.Id;
        }

        Domain.Entities.Vistoria vistoria = request;

        await context.AddAsync(vistoria, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new SuccessResult("Vistoria criada com sucesso.", HttpStatusCode.Created, vistoria.Id);
    }
}