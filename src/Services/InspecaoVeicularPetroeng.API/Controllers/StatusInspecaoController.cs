using InspecaoVeicularPetroeng.API.Queries;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspecaoVeicularPetroeng.API.Controllers;

[ApiController]
[Route("api/status-inspecao")]
[Authorize]
public class StatusInspecaoController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Todos()
    {
        var query = new ObterTodosStatusInspecaoQuery();
        var result = await mediator.Send(query);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }
}