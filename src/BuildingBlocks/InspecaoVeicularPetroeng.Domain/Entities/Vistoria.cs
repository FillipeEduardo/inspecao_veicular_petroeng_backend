namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Vistoria
{
    public long Id { get; set; }
    public DateTime Data { get; set; }
    public int QuilometragemVeiculo { get; set; }
    public string? Observacao { get; set; }
    public int StatusId { get; set; }
    public StatusVistoria Status { get; set; } = null!;
    public int VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; } = null!;
    public List<Foto>? Fotos { get; set; }
    public List<Inspecao>? Inspecoes { get; set; }
}