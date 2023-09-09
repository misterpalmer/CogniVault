using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Validators;

public class InterfaceNameValidator : AbstractValidator<InterfaceName>
{
    private readonly IUnitOfWork _unitOfWork;
    public InterfaceNameValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        RuleFor(x => x.Value)
            .NotNull()
            .WithMessage("Interface name cannot be null.")
            .MustAsync(BeUniqueName)
            .WithMessage("Interface name must be unique.")
            .Must(val => val.Length >= 3 && val.Length <= 24)
            .WithMessage("Interface name must be between 3 and 24 characters or can be empty.")
            .Matches(@"^[a-zA-Z0-9_-]+$")
            .WithMessage("Interface name can only contain alphanumeric characters, underscores, and spaces (not at the beginning or end), or be empty.");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _unitOfWork.QueryRepository<PlatformInterface, Guid>().ExistsAsync(q => q.Name.Value.Equals(name));
    }

    public bool IsValid(InterfaceName value)
    {
        return Validate(value).IsValid;
    }

    public bool IsNotValid(InterfaceName value)
    {
        return !IsValid(value);
    }
}