using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Extensions;
using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;


namespace CogniVault.Application.Entities.AccessControl;

/// <summary>
/// Base class for all users
/// </summary>
/// <seealso cref="CogniVault.Application.Interfaces.IUser" />
/// <seealso cref="CogniVault.Application.Interfaces.IUserAuthentication" />
/// <seealso cref="CogniVault.Application.Interfaces.IUserManagement" />
/// <seealso cref="CogniVault.Application.Interfaces.IUserTimeZone" />
/// <seealso cref="CogniVault.Application.Interfaces.IUserActivity" />
public class FileSystemUser : IFileSystemUser, IUserAuthentication, IUserManagement, IUserTimeZone, IUserActivity
{
    // Unique identifier of the user
    public Guid Id { get; set; }
     // Username of the user
    public Username Username { get; private set; }
     // Password of the user
    public Password Password { get; private set; }
     // Email address of the user (nullable)
    public Email? Email { get; private set; }
     // Quota assigned to the user
    public Quota Quota { get; private set; }
     // Timezone of the user
    public TimeZoneInfo TimeZone { get; private set; }
     // Last login time of the user converted to user's timezone
    private DateTimeOffset _lastLoginAt;
     // Last update time of the user converted to user's timezone
    private DateTimeOffset _updatedAt;
     // Creation time of the user converted to user's timezone
    private DateTimeOffset _createdAt;
    // Last login time of the user converted to user's timezone
    public DateTimeOffset LastLoginAt => _lastLoginAt.ToOffset(TimeZone.GetUtcOffset(_lastLoginAt));
    // Last update time of the user converted to user's timezone
    public DateTimeOffset UpdatedAt => _updatedAt.ToOffset(TimeZone.GetUtcOffset(_updatedAt));
    // Creation time of the user converted to user's timezone
    public DateTimeOffset CreatedAt => _createdAt.ToOffset(TimeZone.GetUtcOffset(_createdAt));

    public ResourceName Name => throw new NotImplementedException();

    // Time provider
    private readonly ITimeProvider _timeProvider;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseUser"/> class.
    /// </summary>
    /// <param name="timeProvider">The time provider.</param>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <param name="timeZoneInfo">The time zone information.</param>
    /// <param name="isSuperUser">if set to <c>true</c> [is super user].</param>
    /// <param name="quota">The quota.</param>
    /// <exception cref="ArgumentNullException">username</exception>
    /// <exception cref="ArgumentNullException">password</exception>
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
