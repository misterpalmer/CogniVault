using CogniVault.Application.Interfaces;

namespace CogniVault.Application.ValueObjects;

public class Username : IEquatable<Username>
{
    private readonly IUsernameValidator validator;
    public string Value { get; }

    public Username(string value, IUsernameValidator validator)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));

        if (string.IsNullOrWhiteSpace(value) || !this.validator.Validate(value))
        {
            throw new ArgumentException("Invalid username", nameof(value));
        }
        
        Value = value;
    }

    public bool Equals(Username other)
    {
        if (other == null)
        {
            return false;
        }

        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is Username username)
        {
            return Value.Equals(username.Value);
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
