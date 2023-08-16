using CogniVault.Application.Abstractions.Resources.AccessControl;


namespace CogniVault.Application.Abstractions.Resources.FileSystem;

public interface IFileSystemResource : IAccessControlResource, IFileSystemAddress
{
    IFileSystemNode Parent { get; }
}