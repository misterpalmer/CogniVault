using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CogniVault.Platform.Core.RestApi.Configuration;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CogniVault.Platform.Identity.Provider;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtSettings;

    public JwtOptions JwtSettings => _jwtSettings;

    public JwtProvider(IOptions<JwtOptions> jwtOptionsAccessor)
    {
        _jwtSettings = jwtOptionsAccessor.Value;
    }

    public async Task<string> GetJwtAsync(string value = "default")
    {
        try
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, value),
                new Claim(ClaimTypes.Role, "admin")
            }.ToList();

            // Select the issuer and audience you want to use.
            // For demonstration, we pick the first one, but you'd typically use some logic here.
            string issuer = JwtSettings.Issuers?.FirstOrDefault() ?? "default-issuer";
            string audience = JwtSettings.ValidAudiences?.FirstOrDefault() ?? "default-audience";

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(JwtSettings.ExpiryDurationInMinutes),
                signingCredentials: signingCredentials
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return await Task.FromResult(tokenAsString);
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"An error occurred while generating JWT: {ex.Message}");
            return null;
        }
    }
}