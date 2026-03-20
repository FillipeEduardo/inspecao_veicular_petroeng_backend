using System.Net;
using InspecaoVeicularPetroeng.API.Helpers;
using InspecaoVeicularPetroeng.Domain.Entities;
using InspecaoVeicularPetroeng.Domain.Enums;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Commands.AuthCommands;

public class CriarCondutorCommand : IRequest<Result>
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public int ContratoId { get; set; }

    public static implicit operator Usuario(CriarCondutorCommand command)
    {
        return new Usuario
        {
            Perfil = Perfil.Condutor,
            Email = command.Email.Trim(),
            NomeCompleto = command.NomeCompleto.Trim(),
            Senha = Hashing.HashPassword(command.Senha).Result,
            Telefone = command.Telefone.SomenteNumeros(),
            ContratoId = command.ContratoId
        };
    }
}

public class CriarCondutorCommandHandler(AppDbContext context) : IRequestHandler<CriarCondutorCommand, Result>
{
    public async Task<Result> Handler(CriarCondutorCommand request, CancellationToken cancellationToken)
    {
        if (context.Usuarios.Any(x => x.Email == request.Email.Trim()))
            return new ErrorResult(["Esse e-mail já possui cadastro."], HttpStatusCode.BadRequest);

        var esseContratoExiste = await context.Contratos.AnyAsync(x => x.Id == request.ContratoId, cancellationToken);
        if (!esseContratoExiste)
            return new ErrorResult(["O contrato informado não existe."], HttpStatusCode.BadRequest);

        Usuario usuario = request;
        await context.AddAsync(usuario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var token = Auth.GenerateToken(usuario);
        return new SuccessResult("Cadastro efetuado com sucesso.", HttpStatusCode.OK, token);
    }
}