namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Vistoria
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public double QuilometragemVeiculo { get; set; }
    public int VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; } = null!;
    public List<Foto>? Fotos { get; set; }
    public List<Inspecao>? Inspecoes { get; set; }
}