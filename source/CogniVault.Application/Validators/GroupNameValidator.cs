using CogniVault.Application.ValueObjects;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.Validators;

public class GroupNameValidator : AbstractValidator<GroupName>
{
    public GroupNameValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Group name cannot be empty")
            .Must(x => x.All(char.IsLetterOrDigit))
            .WithMessage("Group name can only contain letters and digits")
            .Length(1, 50)
            .WithMessage("Group name should be between 1 and 50 characters long");
    }

    public bool IsValid(GroupName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(GroupName value)
    {
        return !IsValid(value);
    }
}