using CogniVault.Application.Abstractions;
using CogniVault.Application.Events;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities;

public class File : FileSystemResource, IFile
{
    public IResourceContent Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public long Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}