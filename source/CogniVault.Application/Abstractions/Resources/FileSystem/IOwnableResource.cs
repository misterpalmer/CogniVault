using CogniVault.Application.Abstractions.Resources.AccessControl.Users;

namespace CogniVault.Application.Abstractions.Resources.FileSystem;

public interface IOwnableResource
{
    IFileSystemUser Owner { get; set; }
    void SetOwner(IFileSystemUser newOwner);
    bool IsHidden { get; set; }
}