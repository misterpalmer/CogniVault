using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities;

/// <summary>
/// Represents a super user
/// </summary>
public class SuperUser : BaseUser, ISuperUser
{
    // Any additional properties exclusive to a SuperUser

    public SuperUser(ITimeProvider timeProvider, Username username, Password password, TimeZoneInfo timeZoneInfo, long quota = 10)
        : base(timeProvider, username, password, timeZoneInfo, quota)
    {
        IsSuperUser = true;
    }

    public void PerformSuperUserAction()
    {
        // Perform some super user action
    }
}
