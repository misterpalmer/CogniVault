namespace CogniVault.Application.VirtualFileSystem.Contracts;

public interface IUnitOfWork : IDisposable
{
    IQueryRepositoryAsync<T> QueryRepository<T>() where T : class;
    ICommandRepositoryAsync<T> CommandRepository<T>() where T : class;
    Task<int> CompleteAsync();
}
