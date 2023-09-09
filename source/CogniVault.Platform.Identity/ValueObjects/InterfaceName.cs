using CogniVault.Platform.Core.Entities;

using FluentValidation;

namespace CogniVault.Platform.Identity.ValueObjects;

public class InterfaceName : IValueObject<InterfaceName>
{
    public string Value { get; }

    private InterfaceName(string value)
    {
        Value = value;
    }

    public static InterfaceName Null => new InterfaceName(string.Empty);

    public static async Task<InterfaceName> CreateAsync(string value, IValidator<InterfaceName> validator)
    {
        var instance = new InterfaceName(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public int CompareTo(InterfaceName? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public InterfaceName Copy()
    {
        return new InterfaceName(Value);
    }

    public bool Equals(InterfaceName? other)
    {
        return Value == other?.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }

    public static explicit operator InterfaceName(string? v)
    {
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "The input string cannot be null.");
        }

        return new InterfaceName(v);
    }
}