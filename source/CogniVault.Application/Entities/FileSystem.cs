using CogniVault.Application.Abstractions;
using CogniVault.Application.Constants;
using CogniVault.Application.Interfaces;
using CogniVault.Application.Options;

namespace CogniVault.Application.Entities;

public class FileSystem : IFileSystem
{
    // Static instance of the class, instantiated once
    private static readonly FileSystem instance = new FileSystem();
    public static FileSystem Instance => instance;

    // Private constructor to prevent instantiation
    private FileSystem()
    {
        // Initialization code here
    }

    private bool _disposedValue;

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

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~FileSystem()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}