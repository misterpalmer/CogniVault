using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Entities;

public class SymbolicLinkNode : FileSystemNode, ISymbolicLinkNode
{
    public override IFileSystemNode Parent { get; set; }

    public SymbolicLinkName Name { get; set; }

    public SymbolicLinkNode(SymbolicLinkName name, IFileSystemNode parent) : base(parent)
    {
        Id = Guid.NewGuid();
        Name = name;
        Parent = parent;
    }
}