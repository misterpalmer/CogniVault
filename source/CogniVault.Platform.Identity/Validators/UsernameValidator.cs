using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;


namespace CogniVault.Platform.Identity.Validators;

public class UsernameValidator : AbstractValidator<Username>
{
    public UsernameValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .Length(11, 64).WithMessage("Username must be between 11 and 64 characters.")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Username can only contain alphanumeric characters and underscores.");
    }

    public bool IsValid(Username value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(Username value)
    {
        return !IsValid(value);
    }
}