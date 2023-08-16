using CogniVault.Application.ValueObjects;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Platform.Identity.Validators;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Email must not be empty.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
    
    public bool IsValid(Email value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(Email value)
    {
        return !IsValid(value);
    }
}