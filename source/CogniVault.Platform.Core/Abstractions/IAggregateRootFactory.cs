namespace CogniVault.Platform.Core.Abstractions;

public interface IAggregateRootFactory<T> where T : IAggregateRoot
{
    Task<T> CreateAsync();
}
