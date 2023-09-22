using System.Text.Json.Serialization;

using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Application.VirtualFileSystem.Converters;
using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Entities;

public class FileNode : FileSystemNode, IFileNode
{
    public override IFileSystemNode Parent { get; set; }
    public FileNode File { get; set; }
    [JsonConverter(typeof(FileNameJsonConverter))] public virtual FileName Name { get; private set; }

    public FileNode(FileNode file, IFileSystemNode parent) : base(parent)
    {
        Id = Guid.NewGuid();
        File = file;
        Parent = parent;
    }

    public FileNode(FileName name, DirectoryNode parent) : base(parent)
    {
        Id = Guid.NewGuid();
        Name = name;
        Parent = parent;
    }
}