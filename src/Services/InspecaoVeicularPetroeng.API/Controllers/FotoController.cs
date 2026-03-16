using InspecaoVeicularPetroeng.API.Commands.Foto;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InspecaoVeicularPetroeng.API.Controllers;

[ApiController]
[Route("api/foto")]
public class FotoController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar(IFormFile foto, [FromForm] long vistoriaId, [FromForm] int evidenciaId)
    {
        var command = new CriarFotoCommand
        {
            EvidenciaId = evidenciaId,
            Foto = foto,
            VistoriaId = vistoriaId
        };
        var result = await mediator.Send(command);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }
}