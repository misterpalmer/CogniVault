using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Attributes;

public class IdentityRepositoryProviderAttribute : Attribute
{
    public IdentityRepositoryProviderKeyAttribute Key { get; }

    public IdentityRepositoryProviderAttribute(string key)
    {
        Key = new IdentityRepositoryProviderKeyAttribute(new IdentityRepositoryProviderKeyAttributeValidator(), key);
    }
}