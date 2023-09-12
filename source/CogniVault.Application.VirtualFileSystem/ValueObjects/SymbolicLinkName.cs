using CogniVault.Platform.Core.Entities;

using FluentValidation;

namespace CogniVault.Application.VirtualFileSystem.ValueObjects;

public class SymbolicLinkName : IValueObject<SymbolicLinkName>
{
    public string Value { get; }

    private SymbolicLinkName(string value)
    {
        Value = value;
    }

    public static SymbolicLinkName Null => new SymbolicLinkName(string.Empty);

    public static async Task<SymbolicLinkName> CreateAsync(string value, IValidator<SymbolicLinkName> validator)
    {
        var instance = new SymbolicLinkName(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public int CompareTo(SymbolicLinkName? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public SymbolicLinkName Copy()
    {
        return new SymbolicLinkName(Value);
    }

    public bool Equals(SymbolicLinkName? other)
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

    public static explicit operator SymbolicLinkName(string? v)
    {
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "The input string cannot be null.");
        }

        return new SymbolicLinkName(v);
    }
}