namespace CogniVault.Platform.Core.RestApi.Configuration;

public class HstsConfigurer
{
    /// <summary>
    /// Specifies the time, in seconds, that the browser should remember that this site is only to be accessed using HTTPS.
    /// </summary>
    public int MaxAge { get; set; }

    /// <summary>
    /// If set to true, this rule applies to all of the site's subdomains as well.
    /// </summary>
    public bool IncludeSubDomains { get; set; }

    /// <summary>
    /// Signals the user agent to treat the HSTS host as a Known HSTS Host.
    /// </summary>
    public bool Preload { get; set; }

    /// <summary>
    /// Determines whether HSTS should be enabled or not. Useful for turning HSTS on/off without removing other settings.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// For multi-domain applications, specify which domains the HSTS policy should be applied to.
    /// </summary>
    public List<string> Domains { get; set; }

    /// <summary>
    /// Allows you to exclude specific subdomains from HSTS even if IncludeSubDomains is set to true.
    /// </summary>
    public List<string> ExcludedSubDomains { get; set; }
}