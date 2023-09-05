namespace CogniVault.Platform.Core.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string value)
    {
        return string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }

    public static string FormatAs(this string source, params object[] paramObjects)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }

        if (paramObjects == null)
        {
            throw new ArgumentNullException("paramObjects");
        }

        return string.Format(source, paramObjects);
    }

    public static T ParseEnum<T>(this string source, bool? ignoreCase = null) where T : struct
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }

        if (!ignoreCase.HasValue)
        {
            return (T)Enum.Parse(typeof(T), source);
        }

        return (T)Enum.Parse(typeof(T), source, ignoreCase.Value);
    }
}