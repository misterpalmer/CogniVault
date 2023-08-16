using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class GroupName : IValueObject<GroupName>
{
    private readonly string _value;

    public string Value => _value;

    public GroupName(string value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(GroupName? other)
    {
        return _value.CompareTo(other?._value);
    }

    public GroupName Copy()
    {
        return new GroupName(_value);
    }

    public bool Equals(GroupName? other)
    {
        return _value == other?._value;
    }

    public void Validate()
    {
        var validator = new GroupNameValidator();
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
        if (obj is GroupName other)
        {
            return Equals(other);
        }
        return false;
    }
}

