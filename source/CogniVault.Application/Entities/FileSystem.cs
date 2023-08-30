using CogniVault.Application.Abstractions;
using CogniVault.Application.Abstractions.Providers;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application;

public class FileSystem : IFileSystem
{
    private readonly IFileSystemNode _root;
    private readonly IFileSystemSecurityProvider _fileSystemSecurityProvider;
    private readonly IAccessControlSecurityProvider _accessControlSecurityProvider;
    private readonly IFileSystemNodeFactory _fileSystemNodeFactory;
    private readonly IDictionary<Guid, IFileSystemNode> _fileSystemNodes; 

    public FileSystem(IFileSystemNode root,
        IFileSystemSecurityProvider fileSystemSecurityProvider,
        IAccessControlSecurityProvider accessControlSecurityProvider,
        IFileSystemNodeFactory fileSystemNodeFactory)
    {
        _root = root;
        _fileSystemSecurityProvider = fileSystemSecurityProvider;
        _accessControlSecurityProvider = accessControlSecurityProvider;
        _fileSystemNodeFactory = fileSystemNodeFactory;
        _fileSystemNodes = new Dictionary<Guid, IFileSystemNode>();
    }

    public IFileSystemNode Root => _root;
    public IFileSystemSecurityProvider FileSystemSecurityProvider => _fileSystemSecurityProvider;
    public IAccessControlSecurityProvider AccessControlSecurityProvider => _accessControlSecurityProvider;
    public IFileSystemNodeFactory FileSystemNodeFactory => _fileSystemNodeFactory;

    public Task DeleteNodeAsync(Guid id)
    {
        _fileSystemNodes.Remove(id);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<IFileSystemNode>> FindByResourceNameAsync(IResourceName resourceName)
    {
        var result = _fileSystemNodes.Values.Where(node => node.Resource.Name == resourceName);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<IFileSystemNode>> FindByUserAsync(IFileSystemUser user)
    {
        var result = _fileSystemNodes.Values.Where(node => node.Resource.Owner == user);
        return Task.FromResult(result);
    }

    public Task<IFileSystemNode> GetNodeAsync(Guid id)
    {
        _fileSystemNodes.TryGetValue(id, out var node);
        return Task.FromResult(node);
    }

    public Task LockResourceAsync(IResource resource)
    {
        // For the sake of this example, we'll assume this operation always succeeds.
        // In a real-world scenario, you might have more complex logic here.
        return Task.CompletedTask;
    }

    public Task MoveNodeAsync(Guid id, IFileSystemNode newParent)
    {
        if (_fileSystemNodes.TryGetValue(id, out var node))
        {
            node.Parent = newParent;
        }

        return Task.CompletedTask;
    }

    public Task<T> ReadAsync<T>(IResource resource)
    {
        // This implementation would heavily depend on the structure of IResource
        // and what it means to "read" a resource.
        throw new NotImplementedException();
    }

    public Task UnlockResourceAsync(IResource resource)
    {
        // Similarly, this would depend on the nature of IResource and your application's rules for locking and unlocking.
        return Task.CompletedTask;
    }

    public Task WriteAsync<T>(IResource resource, T data)
    {
        // This implementation would heavily depend on the structure of IResource
        // and what it means to "write" data to a resource.
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
