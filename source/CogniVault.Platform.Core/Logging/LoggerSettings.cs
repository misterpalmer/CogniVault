namespace CogniVault.Platform.Core.Logging;

public class LoggerSettings
{
    public string ApplicationName { get; set; } = string.Empty;
    public string MinimumLogLevel { get; set; } = "Information";
    public bool EnableConsoleLogging { get; set; } = false;
    public bool EnableFileLogging { get; set; } = false;
}