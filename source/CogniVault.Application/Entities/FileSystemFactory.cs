using CogniVault.Application.Abstractions;

namespace CogniVault.Application.Entities;

public class FileSystemFactory : IFileSystemFactory
{
    private readonly IServiceProvider _serviceProvider;

    public FileSystemFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T CreateFileSystem<T>() where T : IFileSystem
    {
        return (T)_serviceProvider.GetService(typeof(T));
    }

    public Task<IFileSystem> GetCurrentFileSystemAsync()
    {
        throw new NotImplementedException();
    }
}

