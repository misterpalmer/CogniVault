namespace CogniVault.Application.Interfaces;

public interface IResourceManager
{
    IDirectory Root { get; }
    IResourceOperation DirectoryOperations { get; }
    IResourceOperation FileOperations { get; }
    void SetPermissions(IResource resource, IPermission permission);
    IPermission GetPermissions(IResource resource);
}