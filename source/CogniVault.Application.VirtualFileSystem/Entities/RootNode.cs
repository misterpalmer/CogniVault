using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Entities;

public class RootNode : DirectoryNode, IRootNode
{
    private static readonly Lazy<RootNode> lazy = new Lazy<RootNode>(() => new RootNode());

    public static RootNode Instance => lazy.Value;
    

    private RootNode() : base(DirectoryName.RootNode)
    {
        
    }
}