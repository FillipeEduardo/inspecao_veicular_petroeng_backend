using System.Net;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Queries.StatusInspecaoQueries;

public class ObterTodosStatusInspecaoQuery : IRequest<Result>
{
}

public class ObterTodosStatusInspecaoQueryHandler(AppDbContext context)
    : IRequestHandler<ObterTodosStatusInspecaoQuery, Result>
{
    public async Task<Result> Handler(ObterTodosStatusInspecaoQuery request, CancellationToken cancellationToken)
    {
        var listaStatusInspecao =
            await context.StatusInspecao.Select(si => new { si.Id, si.Nome }).ToListAsync(cancellationToken);

        return new SuccessResult("Consulta efetuada com sucesso.", HttpStatusCode.OK, listaStatusInspecao);
    }
}