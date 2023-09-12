using System.Collections.ObjectModel;

using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Contracts;

public interface IDirectoryNode
{
    public IFileSystemNode Parent { get; set; }
    
    public DirectoryName Name { get; }
    
    IList<IFileSystemNode> Children { get; }
    
    void AddChild(IFileSystemNode child);
}