using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Validators;

public class InterfaceValidator : AbstractValidator<InterfaceName>
{
    public InterfaceValidator()
    {
        RuleFor(x => x.Value)
            .NotNull()
            .WithMessage("Organization name cannot be null.")
            .MustAsync(BeUniqueName)
            .WithMessage("Organization name must be unique.")
            .Must(val => val.Length >= 3 && val.Length <= 100)
            .WithMessage("Organization name must be between 3 and 100 characters or can be empty.")
            .Matches(@"^[a-zA-Z0-9_][a-zA-Z0-9_\s]*[a-zA-Z0-9_]$|^$")
            .WithMessage("Organization name can only contain alphanumeric characters, underscores, and spaces (not at the beginning or end), or be empty.");
    }
    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await Task.FromResult(true);
    }

    public bool IsValid(InterfaceName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(InterfaceName value)
    {
        return !IsValid(value);
    }
}
