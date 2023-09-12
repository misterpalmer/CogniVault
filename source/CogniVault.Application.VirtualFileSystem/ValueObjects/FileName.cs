using CogniVault.Platform.Core.Entities;

using FluentValidation;

namespace CogniVault.Application.VirtualFileSystem.ValueObjects;

public class FileName : IValueObject<FileName>
{
    public string Value { get; }

    private FileName(string value)
    {
        Value = value;
    }

    public static FileName Null => new FileName(string.Empty);

    public static async Task<FileName> CreateAsync(string value, IValidator<FileName> validator)
    {
        var instance = new FileName(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public int CompareTo(FileName? other)
    {
        // return StringComparer.OrdinalIgnoreCase.Equals(_value, other?._value);
        return Value.CompareTo(other?.Value);
    }

    public FileName Copy()
    {
        return new FileName(Value);
    }

    public bool Equals(FileName? other)
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

    public static explicit operator FileName(string? v)
    {
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "The input string cannot be null.");
        }

        return new FileName(v);
    }
}