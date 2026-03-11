using System.Net;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Commands.Veiculo;

public class CriarVeiculoCommand : IRequest<Result>
{
    public string Placa { get; set; } = string.Empty;
    public int Ano { get; set; }
    public string Modelo { get; set; } = string.Empty;

    public static implicit operator Domain.Entities.Veiculo(CriarVeiculoCommand command)
    {
        return new Domain.Entities.Veiculo
        {
            Placa = command.Placa,
            Ano = command.Ano,
            Modelo = command.Modelo.Trim()
        };
    }
}

public class CriarVeiculoCommandHandler(AppDbContext context) : IRequestHandler<CriarVeiculoCommand, Result>
{
    public async Task<Result> Handler(CriarVeiculoCommand request, CancellationToken cancellationToken)
    {
        var jaExisteEsseVeiculo = await context.Veiculos.AnyAsync(v => v.Placa == request.Placa, cancellationToken);
        if (jaExisteEsseVeiculo)
            return new ErrorResult(["Um veículo com essa placa ja existe."], HttpStatusCode.BadRequest);

        Domain.Entities.Veiculo novoVeiculo = request;

        await context.AddAsync(novoVeiculo, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new SuccessResult("Veículo criado com sucesso.", HttpStatusCode.OK);
    }
}