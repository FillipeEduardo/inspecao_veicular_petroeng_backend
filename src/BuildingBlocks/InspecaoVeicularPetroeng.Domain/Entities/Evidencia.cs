namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Evidencia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Foto>? Fotos { get; set; }
}