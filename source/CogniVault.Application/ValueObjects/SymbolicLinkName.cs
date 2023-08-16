using CogniVault.Application.Validators;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class SymbolicLinkName : IValueObject<SymbolicLinkName>
{
    private readonly string _value;

    public string Value => _value;

    public SymbolicLinkName(string value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(SymbolicLinkName? other)
    {
        return _value.CompareTo(other?._value);
    }

    public SymbolicLinkName Copy()
    {
        return new SymbolicLinkName(_value);
    }

    public bool Equals(SymbolicLinkName? other)
    {
        return _value == other?._value;
    }

    public void Validate()
    {
        var validator = new SymbolicLinkNameValidator();
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
        if (obj is SymbolicLinkName other)
        {
            return Equals(other);
        }
        return false;
    }
}
