using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CogniVault.Platform.Identity.Provider;
using CogniVault.Platform.Identity.ValueObjects;
using Microsoft.IdentityModel.Tokens;
namespace CogniVault.Platform.Identity.Services;

public class TokenService
{
    private readonly IJwtProvider _jwtProvider;

    public TokenService(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public async Task<string> GenerateTokenAsync(Username username)
    {
        // Using the JwtProvider to generate the token
        return await _jwtProvider.GetJwtAsync(username.Value);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        // The JwtProvider instance doesn't have validation logic, so we'll need to keep the validation here
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtProvider.JwtSettings.SecretKey)),
            ValidateIssuer = true, 
            ValidateAudience = true, 
            ValidIssuers = _jwtProvider.JwtSettings.Issuers, // Supporting multiple issuers
            ValidAudiences = _jwtProvider.JwtSettings.ValidAudiences, // Supporting multiple audiences
            ClockSkew = TimeSpan.FromMinutes(_jwtProvider.JwtSettings.ClockSkew)
        };

        var handler = new JwtSecurityTokenHandler();
        try
        {
            var principal = handler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch
        {
            // Token validation failed.
            return null;
        }
    }
}
