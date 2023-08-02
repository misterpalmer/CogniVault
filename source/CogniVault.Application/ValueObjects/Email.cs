using CogniVault.Application.Validators;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class Email : IValueObject<Email>
{
    private readonly string _value;

    public string Value => _value;

    public Email(string value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(Email? other)
    {
        return _value.CompareTo(other?._value);
    }

    public Email Copy()
    {
        return new Email(_value);
    }

    public bool Equals(Email? other)
    {
        return _value == other?._value;
    }

    public void Validate()
    {
        var validator = new EmailValidator();
        ValidationResult results = validator.Validate(this);
        
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
    }

    public override string ToString()
    {
        return _value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
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