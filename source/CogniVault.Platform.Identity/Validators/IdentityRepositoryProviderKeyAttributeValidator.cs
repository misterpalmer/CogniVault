using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Validators;

public class IdentityRepositoryProviderKeyAttributeValidator : AbstractValidator<IdentityRepositoryProviderKeyAttribute>
{
    public IdentityRepositoryProviderKeyAttributeValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("IdentityRepositoryProviderKeyAttribute must not be empty.");
    }
    
    public bool IsValid(IdentityRepositoryProviderKeyAttribute value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(IdentityRepositoryProviderKeyAttribute value)
    {
        return !IsValid(value);
    }
}