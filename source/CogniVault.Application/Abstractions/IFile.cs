using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Abstractions;

public interface IFile
{
    IResourceContent Content { get; set; }
    long Size { get; set; }
}
