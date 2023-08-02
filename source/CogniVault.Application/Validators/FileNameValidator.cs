using CogniVault.Application.ValueObjects;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.Validators;

public class FileNameValidator : AbstractValidator<FileName>
{
    public FileNameValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("File name must not be empty.")
            .MaximumLength(255).WithMessage("File name must not exceed 255 characters.")
            .Must(name => name.All(c => !Path.GetInvalidFileNameChars().Contains(c)))
            .WithMessage("File name contains invalid characters");
    }
    
    public bool IsValid(FileName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(FileName value)
    {
        return !IsValid(value);
    }
}