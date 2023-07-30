using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions.Resources.AccessControl;

public interface IAccessControlEntity
{
    Guid Id { get; }
    ResourceName Name { get; }
}
