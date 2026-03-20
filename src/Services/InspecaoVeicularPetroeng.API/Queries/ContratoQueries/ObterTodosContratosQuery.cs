using System.Net;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Queries.ContratoQueries;

public class ObterTodosContratosQuery : IRequest<Result>
{
}

public class ObterTodosContratosQueryHandler(AppDbContext context) : IRequestHandler<ObterTodosContratosQuery, Result>
{
    public async Task<Result> Handler(ObterTodosContratosQuery request, CancellationToken cancellationToken)
    {
        var contratos = await context
            .Contratos
            .Select(x => new { x.Id, x.Nome })
            .ToListAsync(cancellationToken);

        return new SuccessResult("Consulta efetuada com sucesso.", HttpStatusCode.OK, contratos);
    }
}