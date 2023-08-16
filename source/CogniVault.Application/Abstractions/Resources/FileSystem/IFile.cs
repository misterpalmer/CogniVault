using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Abstractions.Resources.FileSystem.Files;

public interface IFile : IFileSystemResource
{
    IResourceContent Content { get; set; }
    long Size { get; set; }
}
