using CogniVault.Platform.Core.Entities;

using FluentValidation;

namespace CogniVault.Platform.Identity.ValueObjects;

public class IdentityRepositoryProviderKeyAttribute : IValueObject<IdentityRepositoryProviderKeyAttribute>
{
    protected readonly IValidator<IdentityRepositoryProviderKeyAttribute> _validator;
    public string Value { get; }

    public IdentityRepositoryProviderKeyAttribute(IValidator<IdentityRepositoryProviderKeyAttribute> validator, string value)
    {
        _validator = validator;
        Value = value;
        Validate();
    }

    public int CompareTo(IdentityRepositoryProviderKeyAttribute? other)
    {
        throw new NotImplementedException();
    }

    public IdentityRepositoryProviderKeyAttribute Copy()
    {
        throw new NotImplementedException();
    }

    public bool Equals(IdentityRepositoryProviderKeyAttribute? other)
    {
        throw new NotImplementedException();
    }

    public void Validate()
    {
        throw new NotImplementedException();
    }
}