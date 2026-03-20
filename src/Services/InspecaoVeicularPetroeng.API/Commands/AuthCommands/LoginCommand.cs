using System.Net;
using InspecaoVeicularPetroeng.API.Helpers;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Commands.AuthCommands;

public class LoginCommand : IRequest<Result>
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}

public class LoginCommandHandler(AppDbContext context) : IRequestHandler<LoginCommand, Result>
{
    private const string MensagemError = "Credenciais inválidas.";

    public async Task<Result> Handler(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (usuario is null || !await Hashing.VerifyPassword(request.Senha, usuario.Senha))
            return new ErrorResult([MensagemError], HttpStatusCode.BadRequest);

        var token = Auth.GenerateToken(usuario);
        return new SuccessResult("Token gerado com sucesso.", HttpStatusCode.OK, token);
    }
}