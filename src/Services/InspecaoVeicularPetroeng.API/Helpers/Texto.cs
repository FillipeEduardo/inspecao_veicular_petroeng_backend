namespace InspecaoVeicularPetroeng.API.Helpers;

public static class Texto
{
    public static string SomenteNumeros(this string texto)
    {
        return new string([..texto.Where(char.IsDigit)]);
    }
}