using System.Net;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Queries.EvidenciaQueries;

public class ObterTodasEvidenciasQuery : IRequest<Result>
{
}

public class ObterTodasEvidenciasQueryHandler(AppDbContext context) : IRequestHandler<ObterTodasEvidenciasQuery, Result>
{
    public async Task<Result> Handler(ObterTodasEvidenciasQuery request, CancellationToken cancellationToken)
    {
        var evidencias = await context.Evidencias.Select(e => new { e.Id, e.Nome }).ToListAsync(cancellationToken);

        return new SuccessResult("Consulta efetuada com sucesso.", HttpStatusCode.OK, evidencias);
    }
}