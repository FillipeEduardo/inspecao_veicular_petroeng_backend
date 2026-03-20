namespace InspecaoVeicularPetroeng.Domain.Results;

public class ListResult<T>
{
    public object? Registros { get; set; }
    public double TotalDePaginas => Math.Ceiling((double)TotalDeRegistros / 10);
    public int PaginaAtual { get; set; }
    public int TotalDeRegistros { get; set; }
}