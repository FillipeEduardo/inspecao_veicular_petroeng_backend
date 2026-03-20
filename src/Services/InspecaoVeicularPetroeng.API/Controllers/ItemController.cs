using InspecaoVeicularPetroeng.API.Queries.ItemQueries;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspecaoVeicularPetroeng.API.Controllers;

[ApiController]
[Route("api/item")]
[Authorize]
public class ItemController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Todos()
    {
        var query = new ObterTodosItensQuery();
        var result = await mediator.Send(query);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }
}