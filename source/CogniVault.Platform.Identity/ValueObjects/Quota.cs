using CogniVault.Application.Validators;
using CogniVault.Platform.Core.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class Quota : IValueObject<Quota>
{
    private readonly long _value;

    public long Value => _value;

    public Quota(long value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(Quota? other)
    {
        return _value.CompareTo(other?._value);
    }

    public Quota Copy()
    {
        return new Quota(_value);
    }

    public bool Equals(Quota? other)
    {
        return _value == other?._value;
    }

    public void Validate()
    {
        var validator = new QuotaValidator();
        ValidationResult results = validator.Validate(this);
            
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
    }

    public override string ToString()
    {
        return _value.ToString();
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is Quota other)
        {
            return Equals(other);
        }
        return false;
    }
}

