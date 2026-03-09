namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Inspecao
{
    public long Id { get; set; }
    public long VistoriaId { get; set; }
    public Vistoria Vistoria { get; set; } = null!;
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
}