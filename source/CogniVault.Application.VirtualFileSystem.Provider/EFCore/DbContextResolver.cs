using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;

namespace CogniVault.Application.VirtualFileSystem.Provider.EFCore;

public class DbContextResolver : IDbResolver
{
    public ICommandRepositoryAsync<T> CommandRepository<T>() where T : FileSystemNode
    {
        throw new NotImplementedException();
    }

    public IQueryRepositoryAsync<T> QueryRepository<T>() where T : FileSystemNode
    {
        throw new NotImplementedException();
    }
}