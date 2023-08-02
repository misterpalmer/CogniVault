using CogniVault.Application.Validators;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class PermissionName : IValueObject<PermissionName>
{
    private readonly string _value;

    public string Value => _value;

    public PermissionName(string value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(PermissionName? other)
    {
        return _value.CompareTo(other?._value);
    }

    public PermissionName Copy()
    {
        return new PermissionName(_value);
    }

    public bool Equals(PermissionName? other)
    {
        return _value == other?._value;
    }

    public void Validate()
    {
        var validator = new PermissionNameValidator();
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
        if (obj is PermissionName other)
        {
            return Equals(other);
        }
        return false;
    }
}
