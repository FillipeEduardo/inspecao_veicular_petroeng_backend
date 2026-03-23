using System.Security.Claims;

namespace InspecaoVeicularPetroeng.API.Services;

public interface ICurrentUserService
{
    string? Email { get; }
    long UsuarioId { get; }
    int? ContratoId { get; }
}

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? Email => httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

    public long UsuarioId =>
        long.TryParse(httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            out var usuarioId)
            ? usuarioId
            : 0;

    public int? ContratoId =>
        int.TryParse(httpContextAccessor.HttpContext?.User.FindFirst("contrato_id")?.Value, out var contratoId)
            ? contratoId
            : null;
}