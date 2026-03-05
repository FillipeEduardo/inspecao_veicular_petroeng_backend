using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace InspecaoVeicularPetroeng.API.Helpers;

public static class Hashing
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 4;
    private const int MemorySize = 65536;
    private const int DegreeOfParallelism = 2;

    public static async Task<string> HashPassword(string password)
    {
        var salt = new byte[SaltSize];
        using var randon = RandomNumberGenerator.Create();
        randon.GetBytes(salt);

        using var argon = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            DegreeOfParallelism = DegreeOfParallelism,
            Iterations = Iterations,
            MemorySize = MemorySize,
            Salt = salt
        };

        var hashBytes = await argon.GetBytesAsync(HashSize);

        var hashWithSalt = salt.Concat(hashBytes).ToArray();

        return Convert.ToBase64String(hashWithSalt);
    }

    public static async Task<bool> VerifyPassword(string password, string storedHash)
    {
        if (string.IsNullOrEmpty(password)
            || !Base64.IsValid(storedHash))
            return false;

        var hashBytes = Convert.FromBase64String(storedHash);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        var hashLength = hashBytes.Length - SaltSize;
        var storedPasswordHash = new byte[hashLength];
        Array.Copy(hashBytes, SaltSize, storedPasswordHash, 0, hashLength);

        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            Iterations = Iterations,
            MemorySize = MemorySize,
            DegreeOfParallelism = DegreeOfParallelism
        };

        var computedHash = await argon2.GetBytesAsync(hashLength);

        return computedHash.SequenceEqual(storedPasswordHash);
    }
}