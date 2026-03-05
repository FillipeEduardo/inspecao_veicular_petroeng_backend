namespace InspecaoVeicularPetroeng.Domain.Results;

public class ListResult<T>
{
    public object? Registros { get; set; }
    public double TotalPaginas => Math.Ceiling((double)QuantidadeRegistros / 10);
    public int PaginaAtual { get; set; }
    public int QuantidadeRegistros { get; set; }
}