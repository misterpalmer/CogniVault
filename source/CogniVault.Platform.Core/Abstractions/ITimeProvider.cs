namespace CogniVault.Platform.Core.Abstractions;

public interface ITimeProvider
{
    DateTimeOffset Now { get; }
    DateTimeOffset UtcNow { get; }
}