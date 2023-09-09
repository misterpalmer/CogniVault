using CogniVault.Platform.Core.Entities;
using FluentValidation;

namespace CogniVault.Platform.Identity.ValueObjects;

public class TenantName : IValueObject<TenantName>
{
    public string Value { get; }

    private TenantName(string value)
    {
        Value = value;
    }

    public static TenantName Null => new TenantName(string.Empty);

    public static async Task<TenantName> CreateAsync(string value, IValidator<TenantName> validator)
    {
        var instance = new TenantName(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public int CompareTo(TenantName? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public TenantName Copy()
    {
        return new TenantName(Value);
    }

    public bool Equals(TenantName? other)
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

    public static explicit operator TenantName(string? v)
    {
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "The input string cannot be null.");
        }

        return new TenantName(v);
    }
}