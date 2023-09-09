using CogniVault.Platform.Core.Entities;
using FluentValidation;


namespace CogniVault.Platform.Identity.ValueObjects;

public class Quota : IValueObject<Quota>
{
    public int Value { get; private set; } // Assuming Quota is an integer

    private Quota(int value)
    {
        Value = value;
    }

    public int CompareTo(Quota? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public Quota Copy()
    {
        return new Quota(Value);
    }

    public bool Equals(Quota? other)
    {
        return Value == other?.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static async Task<Quota> CreateAsync(int value, IValidator<Quota> validator)
    {
        var instance = new Quota(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public static Quota Null => new Quota(0);  // Assuming 0 means no quota

    public static explicit operator Quota(int v)
    {
        // Note: Since int is a non-nullable value type, no need to check for null here
        return new Quota(v);
    }
}
