using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;

namespace CogniVault.Application.ValueObjects;

public class Password : IEquatable<Password>
{
    private readonly IValidator<string> validator;
    private readonly IPasswordEncryptor encryptor;
    public string Value { get; }

    public Password(string value, IValidator<string> validator, IPasswordEncryptor encryptor)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this.encryptor = encryptor ?? throw new ArgumentNullException(nameof(encryptor));

        if (string.IsNullOrWhiteSpace(value) || !this.validator.IsValid(value))
        {
            throw new ArgumentException("Invalid password", nameof(value));
        }

        Value = this.encryptor.Encrypt(value);
    }

    public bool Verify(string passwordToVerify)
    {
        return encryptor.Verify(passwordToVerify, Value);
    }

    public bool Equals(Password other)
    {
        if (other == null)
        {
            return false;
        }

        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is Password password)
        {
            return Value.Equals(password.Value);
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
