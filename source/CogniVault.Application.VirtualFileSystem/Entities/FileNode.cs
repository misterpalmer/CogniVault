using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;

namespace CogniVault.Application.VirtualFileSystem.Entities;

public class FileNode : FileSystemNode, IFileNode
{
    public override IFileSystemNode Parent { get; set; }
    public IFileSystemResource File { get; set; }

    public FileNode(IFileSystemResource file, IFileSystemNode parent) : base(parent)
    {
        Id = Guid.NewGuid();
        File = file;
        Parent = parent;
    }
}