namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Vistoria
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public StatusVistoria Status { get; set; } = null!;
    public Veiculo Veiculo { get; set; } = null!;
    public int QuilometragemVeiculo { get; set; }
}