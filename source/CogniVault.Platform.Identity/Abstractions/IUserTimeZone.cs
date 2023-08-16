namespace CogniVault.Application.Abstractions.Resources.AccessControl.Users;

public interface IUserTimeZone
{
    TimeZoneInfo TimeZone { get; }
    void ChangeTimeZone(TimeZoneInfo newTimeZone);
}