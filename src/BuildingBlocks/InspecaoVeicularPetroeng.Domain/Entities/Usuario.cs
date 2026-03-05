namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Usuario
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public int PerfilId { get; set; }
    public Perfil Perfil { get; set; } = null!;
}