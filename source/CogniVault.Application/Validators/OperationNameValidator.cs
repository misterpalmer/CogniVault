using CogniVault.Application.ValueObjects;

using FluentValidation;

namespace CogniVault.Application.Validators;

public class OperationNameValidator : AbstractValidator<OperationName>
{
    public OperationNameValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Operation name cannot be empty.")
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("Operation name can only contain alphanumeric characters.");
    }

    public bool IsValid(OperationName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(OperationName value)
    {
        return !IsValid(value);
    }
}
