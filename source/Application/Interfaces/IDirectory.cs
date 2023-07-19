namespace CogniVault.Application.Interfaces;

public interface IDirectory
{
    string Name { get; }
    IDirectory ParentDirectory { get; }
    IEnumerable<IDirectory> Subdirectories { get; }
    IEnumerable<IFile> Files { get; }
    IEnumerable<IPermission> Permissions { get; }

    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }

    void AddSubdirectory(IDirectory directory);
    void RemoveSubdirectory(IDirectory directory);
    void AddFile(IFile file);
    void RemoveFile(IFile file);
    void AddPermission(IPermission permission);
    void RemovePermission(IPermission permission);
    bool HasPermission(IPermission permission);
}
