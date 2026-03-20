using InspecaoVeicularPetroeng.Domain.Enums;

namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Usuario
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public Perfil Perfil { get; set; }
    public int? ContratoId { get; set; }
    public Contrato? Contrato { get; set; }
}