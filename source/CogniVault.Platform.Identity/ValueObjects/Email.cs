using CogniVault.Platform.Core.Entities;
using FluentValidation;


namespace CogniVault.Platform.Identity.ValueObjects;

public class Email : IValueObject<Email>
{
    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public int CompareTo(Email? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public Email Copy()
    {
        return new Email(Value);
    }

    public bool Equals(Email? other)
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

    public static async Task<Email> CreateAsync(string value, IValidator<Email> validator)
    {
        var instance = new Email(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public static Email Null => new Email(string.Empty);

    public static explicit operator Email(string? v)
    {
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "The input string cannot be null.");
        }

        return new Email(v);
    }
}
