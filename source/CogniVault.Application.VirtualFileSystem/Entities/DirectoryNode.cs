using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Application.VirtualFileSystem.Converters;
using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Entities;

public class DirectoryNode : FileSystemNode, IDirectoryNode
{
    [JsonIgnore] public override IFileSystemNode Parent => base.Parent ?? DirectoryNode.Null;
    
    [JsonIgnore] private static readonly DirectoryNode _null;

    // Public accessor for the Null object
    [JsonIgnore] public static DirectoryNode Null => _null;
    
    [JsonConverter(typeof(DirectoryNameJsonConverter))] public virtual DirectoryName Name { get; private set; }
    
    [JsonIgnore] public IList<IFileSystemNode> Children { get; private set; } = new List<IFileSystemNode>();
    

    // Static constructor to initialize the Null object
    static DirectoryNode()
    {
        _null = new DirectoryNode(DirectoryName.Null, null); // Explicitly pass null as parent
        _null.Id = Guid.Empty;
    }

    public DirectoryNode(DirectoryName name, DirectoryNode parent) : base(parent)
    {
        Id = Guid.NewGuid();
        Name = name;
        Parent = parent;
    }

    public DirectoryNode(DirectoryName name) : this(name, DirectoryNode.Null)
    {
        
    }

    public DirectoryNode(DirectoryName name, IList<IFileSystemNode> children, DirectoryNode parent) : this(name, parent)
    {
        Children = children;
    }

    public void AddChild(IFileSystemNode child)
    {
        if (child == null)
            throw new ArgumentNullException(nameof(child));

        if (child is DirectoryNode directoryNode)
        {
            directoryNode.Parent = this;
            Children.Add(directoryNode);
        }
        else if (child is FileNode fileNode)
        {
            fileNode.Parent = this;
            Children.Add(fileNode);
        }
        else if (child is SymbolicLinkNode symbolicLinkNode)
        {
            symbolicLinkNode.Parent = this;
            Children.Add(symbolicLinkNode);
        }
        else
        {
            throw new ArgumentException("Invalid child type.");
        }
    }
}

