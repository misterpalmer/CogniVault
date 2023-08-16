using CogniVault.Application.ValueObjects;
using FluentValidation;

namespace CogniVault.Application.Validators;

public class DirectoryNameValidator : AbstractValidator<DirectoryName>
{
    public DirectoryNameValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Directory name cannot be empty")
            .Must(name => name.All(c => !Path.GetInvalidFileNameChars().Contains(c)))
            .WithMessage("Directory name contains invalid characters")
            .Length(1, 255).WithMessage("Directory name must be between 1 and 255 characters long.")
            .Matches(@"^[a-zA-Z0-9][a-zA-Z0-9-]*[a-zA-Z0-9]$")
            .WithMessage("Directory name must start and end with a letter or number and can only contain letters, numbers, and hyphens.");
    }
    
    public bool IsValid(DirectoryName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(DirectoryName value)
    {
        return !IsValid(value);
    }
}


