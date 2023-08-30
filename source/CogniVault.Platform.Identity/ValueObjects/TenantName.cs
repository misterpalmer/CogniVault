using CogniVault.Platform.Core.Entities;

using FluentValidation;

namespace CogniVault.Platform.Identity.ValueObjects;

public class TenantName : IValueObject<TenantName>
{
    private readonly IValidator<TenantName> _validator;

    public string Value { get; }

    public TenantName(IValidator<TenantName> validator, string value)
    {
        _validator = validator;
        Value = value;
        Validate();
    }

    public int CompareTo(TenantName? other)
    {
        throw new NotImplementedException();
    }

    public TenantName Copy()
    {
        throw new NotImplementedException();
    }

    public bool Equals(TenantName? other)
    {
        throw new NotImplementedException();
    }

    public void Validate()
    {
        throw new NotImplementedException();
    }
}