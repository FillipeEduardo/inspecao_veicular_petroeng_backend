namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Perfil
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Usuario> Usuarios { get; set; } = [];
}