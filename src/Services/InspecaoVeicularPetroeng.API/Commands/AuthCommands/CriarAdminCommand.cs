using System.Net;
using InspecaoVeicularPetroeng.API.Helpers;
using InspecaoVeicularPetroeng.Domain.Entities;
using InspecaoVeicularPetroeng.Domain.Enums;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;

namespace InspecaoVeicularPetroeng.API.Commands.AuthCommands;

public class CriarAdminCommand : IRequest<Result>
{
    public string KeyAdminTemp { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    public static implicit operator Usuario(CriarAdminCommand command)
    {
        return new Usuario
        {
            Email = command.Email.Trim(),
            NomeCompleto = command.NomeCompleto.Trim(),
            Perfil = Perfil.Admin,
            Senha = Hashing.HashPassword(command.Senha).Result,
            Telefone = command.Telefone.SomenteNumeros()
        };
    }
}

public class CriarAdminCommandHandler(AppDbContext context) : IRequestHandler<CriarAdminCommand, Result>
{
    public async Task<Result> Handler(CriarAdminCommand request, CancellationToken cancellationToken)
    {
        if (Environment.GetEnvironmentVariable("KEY_ADMIN_TEMP") != request.KeyAdminTemp)
            return new ErrorResult(["Não foi possivel criar o usuário."], HttpStatusCode.BadRequest);

        Usuario novoUsuario = request;

        if (context.Usuarios.Any(x => x.Email == novoUsuario.Email))
            return new ErrorResult(["Esse e-mail já possui cadastro."], HttpStatusCode.BadRequest);

        await context.AddAsync(novoUsuario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var token = Auth.GenerateToken(novoUsuario);
        return new SuccessResult("Usuario criado com sucesso.", HttpStatusCode.OK, token);
    }
}