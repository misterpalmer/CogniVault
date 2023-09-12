using CogniVault.Application.VirtualFileSystem.ValueObjects;

using FluentValidation;

namespace CogniVault.Application.VirtualFileSystem.Validators;

public class SymbolicLinkNameValidator : AbstractValidator<SymbolicLinkName>
{
    public SymbolicLinkNameValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Directory name cannot be empty")
            .Must(name => name.All(c => !Path.GetInvalidFileNameChars().Contains(c)))
            .WithMessage("Directory name contains invalid characters")
            .Length(1, 255).WithMessage("Directory name must be between 1 and 255 characters long.")
            .Matches(@"^[a-zA-Z0-9][a-zA-Z0-9-]*[a-zA-Z0-9]$")
            .WithMessage("Directory name must start and end with a letter or number and can only contain letters, numbers, and hyphens.");
    }
    
    public bool IsValid(SymbolicLinkName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(SymbolicLinkName value)
    {
        return !IsValid(value);
    }
}