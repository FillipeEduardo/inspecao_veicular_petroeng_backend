using System.Net;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Queries.ItemQueries;

public class ObterTodosItensQuery : IRequest<Result>
{
}

public class ObterTodosItensQueryHandler(AppDbContext context) : IRequestHandler<ObterTodosItensQuery, Result>
{
    public async Task<Result> Handler(ObterTodosItensQuery request, CancellationToken cancellationToken)
    {
        var itens = await context.Itens.Select(i => new { i.Id, i.Nome }).ToListAsync(cancellationToken);

        return new SuccessResult("Consulta efetuada com sucesso.", HttpStatusCode.OK, itens);
    }
}