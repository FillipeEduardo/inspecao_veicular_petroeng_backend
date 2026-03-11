using System.Net;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Queries.Veiculo;

public class ObterTodosVeiculosQuery : IRequest<Result>
{
}

public class ObterTodosVeiculosQueryHandler(AppDbContext context) : IRequestHandler<ObterTodosVeiculosQuery, Result>
{
    public async Task<Result> Handler(ObterTodosVeiculosQuery request, CancellationToken cancellationToken)
    {
        var veiculos = await context
            .Veiculos
            .Select(v => new
            {
                v.Id, v.Placa, v.Ano, v.Modelo,
                UltimaVistoria = v.Vistorias!.OrderByDescending(vis => vis.Data).FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        return new SuccessResult("Consulta efetuada com sucesso.", HttpStatusCode.OK, veiculos);
    }
}