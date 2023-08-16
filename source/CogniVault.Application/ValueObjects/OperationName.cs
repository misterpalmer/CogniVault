using CogniVault.Application.Validators;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class OperationName : IValueObject<OperationName>
{
    private readonly string _value;

    public string Value => _value;

    public OperationName(string value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(OperationName? other)
    {
        return _value.CompareTo(other?._value);
    }

    public OperationName Copy()
    {
        return new OperationName(_value);
    }

    public bool Equals(OperationName? other)
    {
        return _value == other?._value;
    }

    public void Validate()
    {
        var validator = new OperationNameValidator();
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
        if (obj is OperationName other)
        {
            return Equals(other);
        }
        return false;
    }
}
