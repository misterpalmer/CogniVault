namespace CogniVault.FileSystem.Provider.Memory;

public class MemoryFileSystemProvider : IFileSystemProvider
{
    private readonly ConcurrentDictionary<Guid, IFileSystem> _fileSystems = new();

    public Task<IFileSystem> GetFileSystemAsync(Guid id)
    {
        return Task.FromResult(_fileSystems[id]);
    }

    public Task<IFileSystem> CreateFileSystemAsync()
    {
        var fileSystem = new MemoryFileSystem();
        _fileSystems.TryAdd(fileSystem.Id, fileSystem);
        return Task.FromResult<IFileSystem>(fileSystem);
    }
}