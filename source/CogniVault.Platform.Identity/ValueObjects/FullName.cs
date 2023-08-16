using CogniVault.Platform.Core.Entities;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Platform.Identity.ValueObjects;

public class FullName : IValueObject<FullName>
{
    protected readonly IValidator<FullName> _validator;

    public string Value { get; }

    public FullName(IValidator<FullName> validator, string value)
    {
        _validator = validator;
        Value = value;
        Validate();
    }

    public int CompareTo(FullName? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public FullName Copy()
    {
        return new FullName(_validator, Value);
    }

    public bool Equals(FullName? other)
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

    public void Validate()
    {
        ValidationResult results = _validator.Validate(this);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
    }
}