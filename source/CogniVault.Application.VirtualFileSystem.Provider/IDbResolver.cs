using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;

namespace CogniVault.Application.VirtualFileSystem.Provider;

public interface IDbResolver
{
    IQueryRepositoryAsync<T> QueryRepository<T>() where T : FileSystemNode;
    ICommandRepositoryAsync<T> CommandRepository<T>() where T : FileSystemNode;
}
