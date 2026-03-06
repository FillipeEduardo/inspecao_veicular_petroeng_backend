using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InspecaoVeicularPetroeng.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace InspecaoVeicularPetroeng.API.Helpers;

public static class Auth
{
    public static string GenerateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
        var credentials =
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Subject = GenerateClaims(usuario),
            Expires = DateTime.UtcNow.AddYears(1)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Usuario usuario)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
        ci.AddClaim(new Claim(ClaimTypes.Role, usuario.Perfil.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
        return ci;
    }
}