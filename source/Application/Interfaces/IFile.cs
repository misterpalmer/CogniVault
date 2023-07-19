using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Interfaces;

public interface IFile
{
    Guid Id { get; }
    ResourceName Name { get; set; }
    IEnumerable<ResourceName> Path { get; set; }
    long SizeInBytes { get; set; }
    DateTime CreatedAt { get; }
    DateTime LastModifiedAt { get; }
    IUser Owner { get; set; }

    void Move(string newPath);
    void Rename(string newName);
    void Resize(long newSize);
    void SetOwner(IUser newOwner);
}
