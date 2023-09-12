using System.Linq.Expressions;

namespace CogniVault.Application.VirtualFileSystem.Contracts;

public interface IQueryRepositoryAsync<T> where T : class
{
    Task<IAsyncEnumerable<T>> GetAllAsync(ISpecification<T> spec);

    Task<IAsyncEnumerable<TResult>> GetAllAsync<TResult>(ISpecification<T> spec) where TResult : class;

    Task<T> GetByIdAsync(ISpecification<T> spec);

    Task<TResult> GetByIdAsync<TResult>(ISpecification<T> spec) where TResult : class, new();

    Task<T> GetFirstOrDefaultAsync(ISpecification<T> spec);

    Task<TResult> GetFirstOrDefaultAsync<TResult>(ISpecification<T> spec) where TResult : class;

    Task<IAsyncEnumerable<T>> GetPagedListAsync(ISpecification<T> spec);

    Task<IAsyncEnumerable<TResult>> GetPagedListAsync<TResult>(ISpecification<T> spec) where TResult : class, new();

    Task<bool> ExistsAsync(Expression<Func<T, bool>>? selector = null);
}