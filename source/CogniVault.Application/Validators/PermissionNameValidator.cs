using CogniVault.Application.ValueObjects;

using FluentValidation;

namespace CogniVault.Application.Validators;

public class PermissionNameValidator : AbstractValidator<PermissionName>
{
    public PermissionNameValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Permission name cannot be empty.")
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("Permission name can only contain alphanumeric characters.");
    }

    public bool IsValid(PermissionName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(PermissionName value)
    {
        return !IsValid(value);
    }
}
