using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Entities;

public class TimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}