using CogniVault.Application.Abstractions;
using CogniVault.Application.Abstractions.Providers;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Abstractions.Resources.FileSystem.Directories;
using CogniVault.Application.Options;

namespace CogniVault.Application;

public class FileSystem : IFileSystem
{
    public IFileSystemNode Root => throw new NotImplementedException();

    public IFileSystemSecurityProvider FileSystemSecurityProvider => throw new NotImplementedException();

    public IAccessControlSecurityProvider AccessControlSecurityProvider => throw new NotImplementedException();

    public IFileSystemNodeFactory FileSystemNodeFactory => throw new NotImplementedException();

    public Task DeleteNodeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IFileSystemNode>> FindByResourceNameAsync(string resourceName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IFileSystemNode>> FindByUserAsync(IFileSystemUser user)
    {
        throw new NotImplementedException();
    }

    public Task<IFileSystemNode> GetNodeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task LockResourceAsync(IResource resource)
    {
        throw new NotImplementedException();
    }

    public Task MoveNodeAsync(Guid id, IFileSystemNode newParent)
    {
        throw new NotImplementedException();
    }

    public Task<T> ReadAsync<T>(IResource resource)
    {
        throw new NotImplementedException();
    }

    public Task UnlockResourceAsync(IResource resource)
    {
        throw new NotImplementedException();
    }

    public Task WriteAsync<T>(IResource resource, T data)
    {
        throw new NotImplementedException();
    }
}

// public class FileSystem : IFileSystem
// {
//     private bool _disposedValue;
//     private IDirectory _rootDirectory;
//     private FileSystemOptions _options;
//     private IResourceSecurityManager _resourceSecurityManager;
//     private IResourceServiceManager _resourceServiceManager;
//     private static readonly Lazy<FileSystem> _instance = new Lazy<FileSystem>(() => new FileSystem(/* dependencies */));

//     public static FileSystem Instance => _instance.Value;

//     private FileSystem(IDirectory rootDirectory,
//         FileSystemOptions options,
//         IResourceSecurityManager resourceSecurityManager,
//         IResourceServiceManager resourceServiceManager)
//     {
//         _rootDirectory = rootDirectory;
//         _options = options;
//         _resourceSecurityManager = resourceSecurityManager;
//         _resourceServiceManager = resourceServiceManager;
//     }

//     public bool IsDisposed => _disposedValue;

//     public IDirectory RootDirectory => _rootDirectory;

//     public FileSystemOptions Options => _options;

//     public IResourceSecurityManager ResourceSecurityManager => _resourceSecurityManager;

//     public IResourceServiceManager ResourceServiceManager => _resourceServiceManager;

//     public event EventHandler Closed;

//     public void Close()
//     {
//         // Implement logic to close the file system
//         _disposedValue = true;
//         Closed?.Invoke(this, EventArgs.Empty);
//     }

//     protected virtual void Dispose(bool disposing)
//     {
//         if (!_disposedValue)
//         {
//             if (disposing)
//             {
//                 // Dispose managed state (managed objects)
//                 _rootDirectory = null;
//                 _options = null;
//                 _resourceSecurityManager = null;
//                 _resourceServiceManager = null;
//             }

//             // Free unmanaged resources (unmanaged objects) and override finalizer
//             // Set large fields to null
//             _disposedValue = true;
//         }
//     }

//     public void Dispose()
//     {
//         // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
//         Dispose(disposing: true);
//         GC.SuppressFinalize(this);
//     }
// }
