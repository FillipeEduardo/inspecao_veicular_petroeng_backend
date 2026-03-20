using System.Net;
using InspecaoVeicularPetroeng.Domain.Entities;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Commands.VistoriaCommands;

public class CriarVistoriaCommand : IRequest<Result>
{
    public DateTime Data { get; set; }
    public double QuilometragemVeiculo { get; set; }
    public int VeiculoId { get; set; }
    public List<InspecaoDto> Inspecoes { get; set; } = [];

    public static implicit operator Vistoria(CriarVistoriaCommand command)
    {
        return new Vistoria
        {
            VeiculoId = command.VeiculoId,
            Data = command.Data,
            QuilometragemVeiculo = command.QuilometragemVeiculo,
            Inspecoes = command.Inspecoes.Select(x => (Inspecao)x).ToList()
        };
    }

    public class InspecaoDto
    {
        public string? Observacao { get; set; }
        public int StatusId { get; set; }
        public int ItemId { get; set; }

        public static implicit operator Inspecao(InspecaoDto dto)
        {
            return new Inspecao
            {
                ItemId = dto.ItemId,
                Observacao = dto.Observacao,
                StatusId = dto.StatusId
            };
        }
    }
}

public class CriarVistoriaCommandHandler(AppDbContext context) : IRequestHandler<CriarVistoriaCommand, Result>
{
    public async Task<Result> Handler(CriarVistoriaCommand request, CancellationToken cancellationToken)
    {
        foreach (var inspecao in request.Inspecoes)
        {
            var existeItemDaInspecao = await context.Itens.AnyAsync(i => i.Id == inspecao.ItemId, cancellationToken);
            if (!existeItemDaInspecao)
                return new ErrorResult(["Um dos itens da inspeção não existe."], HttpStatusCode.BadRequest);

            var existeStatusDaInspecao =
                await context.StatusInspecao.AnyAsync(st => st.Id == inspecao.StatusId, cancellationToken);
            if (!existeStatusDaInspecao)
                return new ErrorResult(["Um dos status da inspeção não existe."], HttpStatusCode.BadRequest);
        }

        var esseVeiculoExiste = await context.Veiculos.AnyAsync(x => x.Id == request.VeiculoId, cancellationToken);
        if (!esseVeiculoExiste) return new ErrorResult(["Esse veículo não existe."], HttpStatusCode.BadRequest);

        Vistoria vistoria = request;

        await context.AddAsync(vistoria, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new SuccessResult("Vistoria criada com sucesso.", HttpStatusCode.Created, vistoria.Id);
    }
}