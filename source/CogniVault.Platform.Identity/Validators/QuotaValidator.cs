using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;

namespace CogniVault.Platform.Identity.Validators;

public class QuotaValidator : AbstractValidator<Quota>
{
    public QuotaValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quota cannot be negative.");
    }
}
