using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Contracts;

public interface IFileNode
{
    public IFileSystemNode Parent { get; set; }
    
    public FileName Name { get; }
}