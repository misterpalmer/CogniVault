using System.ComponentModel.DataAnnotations;

namespace CogniVault.Application.ValueObjects;

public class Email : IEquatable<Email>
{
    private static readonly EmailAddressAttribute emailValidator = new EmailAddressAttribute();

    public string Value { get; }

    public Email(string value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException("Invalid email address", nameof(value));
        }

        Value = value;
    }

    private static bool IsValid(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && emailValidator.IsValid(value);
    }

    public static implicit operator string(Email email) => email.Value;

    public static explicit operator Email(string s) => new Email(s);

    public bool Equals(Email other)
    {
        if (other == null)
        {
            return false;
        }

        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is Email email)
        {
            return Equals(email);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}

