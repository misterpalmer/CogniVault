using CogniVault.Application.Abstractions;
using CogniVault.Application.Constants;
using CogniVault.Application.Entities;
using CogniVault.Application.Interfaces;
using CogniVault.Application.Options;

namespace CogniVault.FileSystem.Provider.Memory;
public class MemoryFileSystem : IFileSystem
{
    public bool IsDisposed => throw new NotImplementedException();

    public IDirectory RootDirectory => throw new NotImplementedException();

    public FileSystemOptions Options => throw new NotImplementedException();

    public IResourceManager SecurityManager => throw new NotImplementedException();

    public event EventHandler Closed;

    public void CheckAccess(IResource node, FileSystemSecuredOperation operation)
    {
        throw new NotImplementedException();
    }

    public void Close()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ResourceType GetResourceType(string path)
    {
        throw new NotImplementedException();
    }

    public IService GetService(IResource node)
    {
        throw new NotImplementedException();
    }

    public bool HasAccess(IResource node, FileSystemSecuredOperation operation)
    {
        throw new NotImplementedException();
    }

    public IResource Resolve(IResourceAddress address, ResourceType nodeType)
    {
        throw new NotImplementedException();
    }

    public bool ResourceExists(string path, ResourceType nodeType)
    {
        throw new NotImplementedException();
    }
}
