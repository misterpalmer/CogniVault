using CogniVault.Application.ValueObjects;

using FluentValidation;

namespace CogniVault.Application.Validators;

public class ResourceNameValidator : AbstractValidator<ResourceName>
{
    public ResourceNameValidator()
    {
        RuleFor(r => r.Value)
            .NotEmpty().WithMessage("{PropertyName} should not be empty.")
            .NotNull().WithMessage("{PropertyName} should not be null.")
            .Length(1, 100).WithMessage("{PropertyName} length must be between 1 and 100 characters."); 
    }
}
