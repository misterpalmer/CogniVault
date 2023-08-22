using CogniVault.Application.Abstractions.Resources.AccessControl;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Extensions;
using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;
using CogniVault.Platform.Core.Abstractions;

namespace CogniVault.Application.Entities.AccessControl;


public class FileSystemUser : IFileSystemUser, IUserAuthentication, IUserManagement, IUserTimeZone, IUserActivity
{
    // Unique identifier of the user
    public Guid Id { get; set; }
    Username Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
     // Username of the user
    public Username Username { get; private set; }
    public Password Password { get; private set; }
    public Email? Email { get; private set; }
    public Quota Quota { get; private set; }
    public TimeZoneInfo TimeZone { get; private set; }
    private DateTimeOffset _lastLoginAt;
    private DateTimeOffset _updatedAt;
    private DateTimeOffset _createdAt;
    public DateTimeOffset LastLoginAt => _lastLoginAt.ToOffset(TimeZone.GetUtcOffset(_lastLoginAt));
    public DateTimeOffset UpdatedAt => _updatedAt.ToOffset(TimeZone.GetUtcOffset(_updatedAt));
    public DateTimeOffset CreatedAt => _createdAt.ToOffset(TimeZone.GetUtcOffset(_createdAt));

    Username IFileSystemUser.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    ResourceName IAccessControlEntity.Name => throw new NotImplementedException();

    private readonly ITimeProvider _timeProvider;
    

    public FileSystemUser(ITimeProvider timeProvider, Username username, Password password, TimeZoneInfo timeZoneInfo, Quota quota)
    {
        _timeProvider = timeProvider;
        Username = username;
        Password = password;
        TimeZone = timeZoneInfo ?? TimeZoneInfo.Utc;
        Quota = quota;
        Id = Guid.NewGuid();
        _lastLoginAt = _createdAt = _updatedAt = _timeProvider.UtcNow;
    }
     /// <summary>
    /// Modifies the last login date and time.
    /// </summary>
    public void ModifyLastLoginAt(DateTimeOffset lastLoginAt)
    {
        _lastLoginAt = lastLoginAt;
    }
     /// <summary>
    /// Modifies the updated date and time.
    /// </summary>
    /// <param name="updatedAt">The updated date and time.</param>
    /// <exception cref="ArgumentNullException">updatedAt</exception>
    public void ModifyUpdatedAt(DateTimeOffset updatedAt)
    {
        _updatedAt = updatedAt;
    }
     /// <summary>
    /// Changes the username.
    /// </summary>
    /// <param name="newUsername">The new username.</param>
    /// <exception cref="ArgumentNullException">newUsername</exception>
    public void ChangeUsername(Username newUsername)
    {
        Username = newUsername;
        ModifyUpdatedAt(_timeProvider.UtcNow);
    }
     /// <summary>
    /// Changes the password.
    /// </summary>
    /// <param name="newPassword">The new password.</param>
    /// <exception cref="ArgumentNullException">newPassword</exception>
    public void ChangePassword(Password newPassword)
    {
        Password = newPassword;
        ModifyUpdatedAt(_timeProvider.UtcNow);
    }
     /// <summary>
    /// Changes the email.
    /// </summary>
    /// <param name="newEmail">The new email.</param>
    /// <exception cref="ArgumentNullException">newEmail</exception>
    public void ChangeEmail(Email newEmail)
    {
        Email = newEmail;
        ModifyUpdatedAt(_timeProvider.UtcNow);
    }
     /// <summary>
    /// Changes the quota.
    /// </summary>
    /// <param name="newQuota">The new quota.</param>
    /// <exception cref="ArgumentOutOfRangeException">newQuota</exception>
    public void ChangeQuota(Quota newQuota)
    {
        Quota = newQuota;
        ModifyUpdatedAt(_timeProvider.UtcNow);
    }
     /// <summary>
    /// Changes the time zone.
    /// </summary>
    /// <param name="newTimeZone">The new time zone.</param>
    /// <exception cref="ArgumentNullException">newTimeZone</exception>
    public void ChangeTimeZone(TimeZoneInfo newTimeZone)
    {
        TimeZone = newTimeZone;
        ModifyUpdatedAt(_timeProvider.UtcNow);
    }
}
