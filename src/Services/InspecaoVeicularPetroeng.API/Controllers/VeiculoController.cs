using InspecaoVeicularPetroeng.API.Commands.Veiculo;
using InspecaoVeicularPetroeng.API.Queries.Veiculo;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspecaoVeicularPetroeng.API.Controllers;

[ApiController]
[Route("api/veiculo")]
[Authorize]
public class VeiculoController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Todos()
    {
        var query = new ObterTodosVeiculosQuery();
        var result = await mediator.Send(query);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarVeiculoCommand command)
    {
        var result = await mediator.Send(command);
        return new ObjectResult(result)
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }
}