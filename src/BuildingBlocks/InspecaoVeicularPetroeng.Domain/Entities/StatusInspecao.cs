namespace InspecaoVeicularPetroeng.Domain.Entities;

public class StatusInspecao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Inspecao>? Inspecoes { get; set; }
}