namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Veiculo
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public int Ano { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public List<Vistoria>? Vistorias { get; set; }
}