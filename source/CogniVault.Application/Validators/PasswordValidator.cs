// using CogniVault.Application.ValueObjects;

// using FluentValidation;

// namespace CogniVault.Application.Validators;

// public class PasswordValidator : AbstractValidator<Password>
// {
//     public PasswordValidator()
//     {
//         RuleFor(x => x.Value)
//             .NotEmpty()
//             .MinimumLength(8)
//             .WithMessage("Password must be at least 8 characters.")
//             .Matches("[A-Z]")
//             .WithMessage("Password must contain at least one uppercase letter.")
//             .Matches("[a-z]")
//             .WithMessage("Password must contain at least one lowercase letter.")
//             .Matches("[0-9]")
//             .WithMessage("Password must contain at least one digit.")
//             .Matches("[!@#$%^&*()_+{}|\\:;'<>?,./\"]")
//             .WithMessage("Password must contain at least one special character.");
//     }

//     public bool IsValid(Password value)
//     {
//         return Validate(value).IsValid;
//     }

//     public bool IsNotValid(Password value)
//     {
//         return !IsValid(value);
//     }
// }