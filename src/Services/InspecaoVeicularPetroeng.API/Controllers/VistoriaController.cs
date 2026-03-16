using InspecaoVeicularPetroeng.API.Commands.Vistoria;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InspecaoVeicularPetroeng.API.Controllers;

[ApiController]
[Route("api/vistoria")]
public class VistoriaController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar(CriarVistoriaCommand command)
    {
        var result = await mediator.Send(command);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }
}