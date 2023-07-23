namespace CogniVault.Application.Extensions;

public static class DateTimeExtensions
{
    // <summary>
    /// Converts the specified <see cref="DateTime"/> value to the user's local time based on the provided time zone.
    /// If no time zone is provided, it defaults to "Pacific Standard Time".
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> value to convert.</param>
    /// <param name="timeZone">The <see cref="TimeZoneInfo"/> representing the user's time zone.</param>
    /// <returns>The converted <see cref="DateTime"/> value in the user's local time.</returns>
    public static DateTime ConvertToUserTime(this DateTime dateTime, TimeZoneInfo timeZone)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);
    }

    public static DateTime ConvertToUtc(this DateTime dateTime, TimeZoneInfo timeZone)
    {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
            return dateTime;
        }

        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);
    }
}