using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Entities;

public class TimeProvider : ITimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}