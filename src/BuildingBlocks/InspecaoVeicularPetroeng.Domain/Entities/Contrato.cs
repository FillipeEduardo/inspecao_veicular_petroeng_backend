namespace InspecaoVeicularPetroeng.Domain.Entities;

public class Contrato
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Usuario>? Usuarios { get; set; }
    public List<Veiculo>? Veiculos { get; set; }
}