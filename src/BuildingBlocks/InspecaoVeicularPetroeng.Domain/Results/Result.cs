using System.Net;

namespace InspecaoVeicularPetroeng.Domain.Results;

public abstract record Result(HttpStatusCode HttpStatusCode)
{
}