using InspecaoVeicularPetroeng.API.Commands.Auth;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InspecaoVeicularPetroeng.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("criar-usuario")]
    public async Task<IActionResult> CriarUsuario(CriarUsuarioCommand command)
    {
        var result = await mediator.Send(command);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await mediator.Send(command);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }
}