using CogniVault.Application.ValueObjects;
using FluentValidation;

namespace CogniVault.Application.Validators;

public class QuotaValidator : AbstractValidator<Quota>
{
    public QuotaValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quota cannot be negative.");
    }
}

// The QuotaValidator should validate a Quota object, not a long primitive type. This is because the validation should happen on your value object, not just any long value. This would also allow you to add more complex validation rules in the future that consider more aspects of the Quota object than just the _value. Here is how it should look:
// This way, the validation is clearly associated with the Quota value object.