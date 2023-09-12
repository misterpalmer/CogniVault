using System.Text.Json.Serialization;

using CogniVault.Application.VirtualFileSystem.Contracts;

namespace CogniVault.Application.VirtualFileSystem.Abstractions;

public abstract class FileSystemNode : IFileSystemNode
{
    [JsonIgnore] private bool _isLocked;

    [JsonIgnore] public virtual IFileSystemNode Parent { get; set; }

    public Guid Id { get; set; }

    public FileSystemNode(IFileSystemNode parent)
    {
        Parent = parent;
    }

    public Task<bool> IsLockedAsync()
    {
        return Task.FromResult(_isLocked);
    }

    public Task LockAsync()
    {
        _isLocked = true;
        return Task.CompletedTask;
    }

    public Task<T> ReadAsync<T>()
    {
        throw new NotImplementedException();
    }

    public Task UnlockAsync()
    {
        _isLocked = false;
        return Task.CompletedTask;
    }

    public Task WriteAsync<T>(T data)
    {
        throw new NotImplementedException();
    }
}