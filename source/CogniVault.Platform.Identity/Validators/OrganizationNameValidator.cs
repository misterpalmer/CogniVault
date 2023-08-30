using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;

namespace CogniVault.Platform.Identity.Validators;

public class OrganizationNameValidator : AbstractValidator<OrganizationName>
{
    private readonly IUnitOfWork _unitOfWork;
    public OrganizationNameValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(x => x.Value)
            .NotNull()
            .WithMessage("Organization name cannot be null.")
            .MustAsync(BeUniqueName)
            .WithMessage("Organization name must be unique.")
            .Must(val => val.Length >= 3 && val.Length <= 100)
            .WithMessage("Organization name must be between 3 and 100 characters or can be empty.")
            .Matches(@"^[a-zA-Z0-9_][a-zA-Z0-9_\s]*[a-zA-Z0-9_]$|^$")
            .WithMessage("Organization name can only contain alphanumeric characters, underscores, and spaces (not at the beginning or end), or be empty.");

    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _unitOfWork.QueryRepository<PlatformOrganization, Guid>().ExistsAsync(q => q.Name.Value.Equals(name));
    }

    public bool IsValid(OrganizationName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(OrganizationName value)
    {
        return !IsValid(value);
    }
}