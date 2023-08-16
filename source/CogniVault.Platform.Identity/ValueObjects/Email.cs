using CogniVault.Application.Validators;
using CogniVault.Platform.Core.Entities;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class Email : IValueObject<Email>
{
    private readonly IValidator<Email> _validator;

    public string Value { get; }

    public Email(IValidator<Email> validator, string value)
    {
        _validator = validator;
        Value = value;
        Validate();
    }

    public int CompareTo(Email? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public Email Copy()
    {
        return new Email(_validator, Value);
    }

    public bool Equals(Email? other)
    {
        return Value == other?.Value;
    }

    public void Validate()
    {
        ValidationResult results = _validator.Validate(this);
        
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
    }

    public override string ToString()
    {
        return Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is Email other)
        {
            return Equals(other);
        }
        return false;
    }

    public static bool operator ==(Email left, Email right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Email left, Email right)
    {
        return !(left == right);
    }
}