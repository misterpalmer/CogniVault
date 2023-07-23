namespace CogniVault.Application.Interfaces;

public interface ITimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}