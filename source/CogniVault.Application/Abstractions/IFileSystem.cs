using CogniVault.Application.Constants;
using CogniVault.Application.Entities;
using CogniVault.Application.Interfaces;
using CogniVault.Application.Options;

namespace CogniVault.Application.Abstractions;

public interface IFileSystem : IResourceResolver, IDisposable
{
    bool IsDisposed { get; }
    event EventHandler Closed;
    IDirectory RootDirectory { get; }
    IService GetService(IResource node);
    ResourceType GetResourceType(string path);
    bool ResourceExists(string path, ResourceType nodeType);
    FileSystemOptions Options { get; }
    IResource Resolve(IResourceAddress address, ResourceType nodeType);
    IResourceManager SecurityManager { get; }
    bool HasAccess(IResource node, FileSystemSecuredOperation operation);
    void CheckAccess(IResource node, FileSystemSecuredOperation operation);
    void Close();
}

