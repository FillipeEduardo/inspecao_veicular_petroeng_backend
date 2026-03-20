using System.Net;
using InspecaoVeicularPetroeng.API.Helpers;
using InspecaoVeicularPetroeng.Domain.Entities;
using InspecaoVeicularPetroeng.Domain.Enums;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;

namespace InspecaoVeicularPetroeng.API.Commands.AuthCommands;

public class CriarUsuarioCommand : IRequest<Result>
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    public static implicit operator Usuario(CriarUsuarioCommand command)
    {
        return new Usuario
        {
            Perfil = Perfil.Condutor,
            Email = command.Email.Trim(),
            NomeCompleto = command.NomeCompleto.Trim(),
            Senha = Hashing.HashPassword(command.Senha).Result,
            Telefone = command.Telefone.SomenteNumeros()
        };
    }
}

public class CriarUsuarioCommandHandler(AppDbContext context) : IRequestHandler<CriarUsuarioCommand, Result>
{
    public async Task<Result> Handler(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (context.Usuarios.Any(x => x.Email == request.Email.Trim()))
            return new ErrorResult(["Esse e-mail já possui cadastro."], HttpStatusCode.BadRequest);

        Usuario usuario = request;
        await context.AddAsync(usuario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var token = Helpers.Auth.GenerateToken(usuario);
        return new SuccessResult("Cadastro efetuado com sucesso.", HttpStatusCode.OK, token);
    }
}