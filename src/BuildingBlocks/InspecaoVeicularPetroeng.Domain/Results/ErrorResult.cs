using System.Net;
using System.Text.Json.Serialization;

namespace InspecaoVeicularPetroeng.Domain.Results;

public record ErrorResult(List<string> Erros, [property: JsonIgnore] HttpStatusCode StatusCode) : Result(StatusCode)
{
}