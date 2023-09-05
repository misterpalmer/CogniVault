using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Validators;

public class PlainPasswordValidator : AbstractValidator<PlainPassword>
{
    public PlainPasswordValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one digit.")
            .Matches("[!@#$%^&*()_+{}|\\:;'<>?,./\"]")
            .WithMessage("Password must contain at least one special character.");
    }

    public bool IsValid(PlainPassword value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(PlainPassword value)
    {
        return !IsValid(value);
    }
}
