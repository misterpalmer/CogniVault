using CogniVault.Application.Abstractions.Resources.AccessControl.Users;

namespace CogniVault.Application.Abstractions.Resources;

public interface IResourceActivityProperties
{
    IFileSystemUser CreatedBy { get; }
    DateTimeOffset CreatedAt { get; }
    IFileSystemUser LastModifiedBy { get; }
    DateTimeOffset LastModifiedAt { get; }
}