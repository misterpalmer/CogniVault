using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Entities;

public class PlatformUser : DomainEntityBase
{
    public Username Username { get; private set; } = null!;
    public EncryptedPassword Password { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Quota Quota { get; private set; } = null!;
    public TimeZoneInfo TimeZone { get; private set; } = null!;
    public DateTimeOffset LastLoginAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public PlatformUser(Username username, 
                        EncryptedPassword password, 
                        Email email, 
                        Quota quota, 
                        TimeZoneInfo timeZone,
                        DateTimeOffset createdAt)
    {
        Id = Guid.NewGuid();
        SetUsername(username);
        SetPassword(password);
        SetEmail(email);
        SetQuota(quota);
        SetTimeZone(timeZone);
        CreatedAt = createdAt;
        UpdatedAt = createdAt; // initial creation time
        LastLoginAt = DateTimeOffset.MinValue; // Or another default value if preferable
    }

    private PlatformUser(Username username)
    {
        SetUsername(username);
        SetPassword(EncryptedPassword.Null);
        SetEmail(Email.Null);
        SetQuota(Quota.Null);
        SetTimeZone(TimeZoneInfo.Utc);
        CreatedAt = DateTimeOffset.MinValue;
        UpdatedAt = DateTimeOffset.MinValue;
        LastLoginAt = DateTimeOffset.MinValue;
    }

    public PlatformUser UpdateUsername(Username newUsername)
    {
        SetUsername(newUsername);
        MarkUpdated();
        return this;
    }

    public PlatformUser UpdatePassword(EncryptedPassword newPassword)
    {
        SetPassword(newPassword);
        MarkUpdated();
        return this;
    }

    public PlatformUser UpdateEmail(Email newEmail)
    {
        SetEmail(newEmail);
        MarkUpdated();
        return this;
    }

    public PlatformUser UpdateQuota(Quota newQuota)
    {
        SetQuota(newQuota);
        MarkUpdated();
        return this;
    }

    public PlatformUser UpdateTimeZone(TimeZoneInfo newTimeZone)
    {
        SetTimeZone(newTimeZone);
        MarkUpdated();
        return this;
    }

    public PlatformUser UpdateLastLogin(DateTimeOffset loginTime)
    {
        LastLoginAt = loginTime;
        MarkUpdated();
        return this;
    }

    private void SetUsername(Username username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }

    private void SetPassword(EncryptedPassword password)
    {
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }

    private void SetEmail(Email email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    private void SetQuota(Quota quota)
    {
        Quota = quota ?? throw new ArgumentNullException(nameof(quota));
    }

    private void SetTimeZone(TimeZoneInfo timeZone)
    {
        TimeZone = timeZone ?? throw new ArgumentNullException(nameof(timeZone));
    }

    private void MarkUpdated()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public bool IsAuthenticated => throw new NotImplementedException();

    // Null Object Pattern implementation:
    public static PlatformUser Null => new PlatformUser(Username.Null)
    {
        Id = Guid.Empty
    };
}

