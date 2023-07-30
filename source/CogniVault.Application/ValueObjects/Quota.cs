namespace CogniVault.Application.ValueObjects;

public struct Quota
{
    private readonly long _value;

    public Quota(long value)
    {
        if(value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Quota cannot be negative.");

        _value = value;
    }

    public static implicit operator long(Quota quota) => quota._value;
}
