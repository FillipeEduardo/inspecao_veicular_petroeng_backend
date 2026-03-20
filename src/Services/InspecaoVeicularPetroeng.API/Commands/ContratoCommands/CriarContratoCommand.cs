using System.Net;
using InspecaoVeicularPetroeng.Domain.Entities;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Commands.ContratoCommands;

public class CriarContratoCommand : IRequest<Result>
{
    public string Nome { get; set; } = string.Empty;

    public static implicit operator Contrato(CriarContratoCommand command)
    {
        return new Contrato
        {
            Nome = command.Nome.Trim()
        };
    }
}

public class CriarContratoCommandHandler(AppDbContext context) : IRequestHandler<CriarContratoCommand, Result>
{
    public async Task<Result> Handler(CriarContratoCommand request, CancellationToken cancellationToken)
    {
        Contrato novoContrato = request;
        var jaExisteContratoComEsseNome =
            await context.Contratos.AnyAsync(x => x.Nome == novoContrato.Nome, cancellationToken);
        if (jaExisteContratoComEsseNome)
            return new ErrorResult(["Ja Existe um contrato com esse nome."], HttpStatusCode.BadRequest);

        await context.AddAsync(novoContrato, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new SuccessResult("Contrato criado com sucesso.", HttpStatusCode.Created, novoContrato.Id);
    }
}