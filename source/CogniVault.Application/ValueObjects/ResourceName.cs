using CogniVault.Application.Validators;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class ResourceName : IValueObject<ResourceName>
{
    private readonly string _value;

    public string Value => _value;

    public ResourceName(string value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(ResourceName? other)
    {
        return StringComparer.OrdinalIgnoreCase.Compare(_value, other?._value);
    }

    public ResourceName Copy()
    {
        return new ResourceName(_value);
    }

    public bool Equals(ResourceName? other)
    {
        return StringComparer.OrdinalIgnoreCase.Equals(_value, other?._value);
    }

    public void Validate()
    {
        var validator = new ResourceNameValidator();
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
        return StringComparer.OrdinalIgnoreCase.GetHashCode(_value);
    }

    public override bool Equals(object obj)
    {
        if (obj is ResourceName other)
        {
            return Equals(other);
        }
        return false;
    }
}
