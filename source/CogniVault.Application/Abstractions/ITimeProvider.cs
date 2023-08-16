namespace CogniVault.Application.Interfaces;

public interface ITimeProvider
{
    DateTimeOffset Now { get; }
    DateTimeOffset UtcNow { get; }
}