using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities;

public class User : BaseUser
{
    public User(ITimeProvider timeProvider, Username username,
        Password password,
        TimeZoneInfo timeZoneInfo,
        long quota = 10) : base(timeProvider, username, password, timeZoneInfo, quota)
    {
        IsSuperUser = false;
    }
}