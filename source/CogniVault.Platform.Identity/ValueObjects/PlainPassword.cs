using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Validators;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Platform.Identity.ValueObjects;

public class PlainPassword : IValueObject<PlainPassword>
{
    public string Value { get; private set; }

    private PlainPassword(string encryptedValue)
    {
        Value = encryptedValue;
    }

    public int CompareTo(PlainPassword? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public PlainPassword Copy()
    {
        return new PlainPassword(Value);
    }

    public bool Equals(PlainPassword? other)
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

    public static async Task<PlainPassword> CreateAsync(string plainPassword, IValidator<PlainPassword> passwordValidator)
    {
        var instance = new PlainPassword(plainPassword);
        var validationResult = passwordValidator.Validate(instance);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return instance;
    }

    public static PlainPassword Null => new PlainPassword(string.Empty);  // Note: Please reconsider using null as the encryptor.
}
