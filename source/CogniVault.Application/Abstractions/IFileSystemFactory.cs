namespace CogniVault.Application.Abstractions;

public interface IFileSystemFactory
{
    T CreateFileSystem<T>() where T : IFileSystem;
    Task<IFileSystem> GetCurrentFileSystemAsync();
}