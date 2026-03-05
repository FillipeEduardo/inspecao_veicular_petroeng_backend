using System.Net;
using System.Text.Json.Serialization;

namespace InspecaoVeicularPetroeng.Domain.Results;

public record SuccessResult(string Mensagem, [property: JsonIgnore] HttpStatusCode StatusCode, object? Dados = null)
    : Result(StatusCode)
{
}