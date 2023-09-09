using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Platform.Identity.ValueObjects;

public class Username : IValueObject<Username>
{
    public string Value { get; private set; }

    private Username(string value)
    {
        Value = value;
    }

    public int CompareTo(Username? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public Username Copy()
    {
        return new Username(Value);
    }

    public bool Equals(Username? other)
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

    public static async Task<Username> CreateAsync(string value, IValidator<Username> validator)
    {
        var instance = new Username(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public static Username Null => new Username(string.Empty);

    public static explicit operator Username(string? v)
    {
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "The input string cannot be null.");
        }

        return new Username(v);
    }

    public override bool Equals(object obj)
    {
        if (obj is Username other)
        {
            return Equals(other);
        }
        return false;
    }
}

