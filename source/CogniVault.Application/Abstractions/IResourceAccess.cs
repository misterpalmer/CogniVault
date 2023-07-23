using CogniVault.Application.Constants;

namespace CogniVault.Application.Abstractions;

public interface IResourceAccess
{
    void CheckAccess(FileSystemSecuredOperation operation);
    void CreateGlobalLock(string name);
}