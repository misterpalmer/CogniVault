using CogniVault.Platform.Core.Entities;

using FluentValidation;

namespace CogniVault.Platform.Identity.ValueObjects;

public class InterfaceName : IValueObject<InterfaceName>
{
    protected readonly IValidator<InterfaceName> _validator;

    public string Value { get; }

    public InterfaceName(IValidator<InterfaceName> validator, string value)
    {
        _validator = validator;
        Value = value;
        Validate();
    }

    public int CompareTo(InterfaceName? other)
    {
        throw new NotImplementedException();
    }

    public InterfaceName Copy()
    {
        throw new NotImplementedException();
    }

    public bool Equals(InterfaceName? other)
    {
        throw new NotImplementedException();
    }

    public void Validate()
    {
        throw new NotImplementedException();
    }
}