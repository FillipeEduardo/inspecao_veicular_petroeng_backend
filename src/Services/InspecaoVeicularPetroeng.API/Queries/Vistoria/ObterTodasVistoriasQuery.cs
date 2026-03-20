using System.Net;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Queries.Vistoria;

public class ObterTodasVistoriasQuery : IRequest<Result>
{
    public int Pagina { get; set; }
}

public class ObterTodasVistoriasQueryHandler(AppDbContext context) : IRequestHandler<ObterTodasVistoriasQuery, Result>
{
    public async Task<Result> Handler(ObterTodasVistoriasQuery request, CancellationToken cancellationToken)
    {
        var query = context
            .Vistorias
            .Select(x => new
            {
                x.Id, x.Data, x.QuilometragemVeiculo,
                Veiculo = new { x.Veiculo.Id, x.Veiculo.Ano, x.Veiculo.Modelo, x.Veiculo.Placa }
            });

        var totalDeRegistro = await query.CountAsync(cancellationToken);

        var registros = await query
            .OrderByDescending(x => x.Id)
            .Skip((request.Pagina - 1) * 10)
            .Take(10)
            .ToListAsync(cancellationToken);

        var listResult = new ListResult<object>
        {
            PaginaAtual = request.Pagina,
            TotalDeRegistros = totalDeRegistro,
            Registros = registros
        };

        return new SuccessResult("Consulta efetuada com sucesso.", HttpStatusCode.OK, listResult);
    }
}