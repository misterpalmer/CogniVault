using CogniVault.Application.ValueObjects;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Entities;

public class PlatformUser<T> : IPlatformUser<Guid>
{
    public Guid Id { get; set; } 
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email? Email { get; private set; }
    public Quota Quota { get; private set; }
    public TimeZoneInfo TimeZone { get; private set; }
    public DateTimeOffset LastLoginAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
}