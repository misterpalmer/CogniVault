namespace CogniVault.Platform.Core.RestApi.Configuration;

public sealed class JwtOptions
{
    /// <summary>
    /// The amount of time in minutes that the token's "nbf" (Not Before) and "exp" (Expiration) claims will be considered with some leniency. 
    /// </summary>
    public double ClockSkew { get; set; }

    /// <summary>
    /// Allowed issuers of the JWT.
    /// </summary>
    public IEnumerable<string> Issuers { get; set; }

    /// <summary>
    /// The thumbprint of the certificate used to sign the JWT.
    /// </summary>
    public string CertificateThumbprint { get; set; }

    /// <summary>
    /// The secret key used to sign the JWT (usually for symmetric algorithms like HS256).
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    /// The algorithm to use for JWT signing. (e.g., HS256, RS256)
    /// </summary>
    public string Algorithm { get; set; } = "HS256";

    /// <summary>
    /// The audience that the JWT should be valid for.
    /// </summary>
    public IEnumerable<string> ValidAudiences { get; set; }

    /// <summary>
    /// The time in minutes that the token will be valid after its creation.
    /// </summary>
    public int ExpiryDurationInMinutes { get; set; } = 60;

    /// <summary>
    /// If set, tokens must be presented over HTTPS.
    /// </summary>
    public bool RequireHttpsMetadata { get; set; } = true;

    /// <summary>
    /// Specifies if incoming tokens should be saved to the current HttpContext.
    /// </summary>
    public bool SaveToken { get; set; } = true;

    /// <summary>
    /// The claim type that will be checked to determine the name of the user.
    /// </summary>
    public string NameClaimType { get; set; } = "name";
    
    /// <summary>
    /// The claim type that will be checked to determine the roles of the user.
    /// </summary>
    public string RoleClaimType { get; set; } = "role";
}
