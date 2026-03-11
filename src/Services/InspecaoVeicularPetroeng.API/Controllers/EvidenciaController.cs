using InspecaoVeicularPetroeng.API.Queries.Evidencia;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InspecaoVeicularPetroeng.API.Controllers;

[ApiController]
[Route("api/evidencia")]
public class EvidenciaController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Todos()
    {
        var query = new ObterTodasEvidenciasQuery();
        var result = await mediator.Send(query);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }
}