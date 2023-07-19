namespace CogniVault.Application.Interfaces;

public interface IUserTimeZone
{
    TimeZoneInfo TimeZone { get; }
    void ChangeTimeZone(TimeZoneInfo newTimeZone);
}