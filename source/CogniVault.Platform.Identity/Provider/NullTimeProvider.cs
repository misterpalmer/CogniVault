using CogniVault.Platform.Core.Abstractions;

namespace CogniVault.Platform.Identity.Provider;

public class NullTimeProvider : ITimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}