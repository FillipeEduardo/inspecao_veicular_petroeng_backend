namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Foto
{
    public Guid Id { get; set; }
    public string Extensao { get; set; } = string.Empty;
    public int EvidenciaId { get; set; }
    public Evidencia Evidencia { get; set; } = null!;
    public long VistoriaId { get; set; }
    public Vistoria Vistoria { get; set; } = null!;
}