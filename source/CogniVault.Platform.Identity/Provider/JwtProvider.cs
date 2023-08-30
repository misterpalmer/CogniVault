using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CogniVault.Platform.Identity.Options;

using Microsoft.IdentityModel.Tokens;

namespace CogniVault.Platform.Identity.Provider;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtSettings;

    public JwtOptions JwtSettings => _jwtSettings;

    public async Task<string> GetJwtAsync(string value = "default")
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, value),
            new Claim(ClaimTypes.Role, "admin")
        }.ToList();

        var signingCredentials =new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: JwtSettings.Issuer,
            audience: JwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(JwtSettings.ExpiryMinutes),
            signingCredentials: signingCredentials
            );

        string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

        return await Task.FromResult(tokenAsString);
    }
}