using System.Linq.Expressions;

using CogniVault.Platform.Core.Entities;

namespace CogniVault.Platform.Core.Abstractions.Persistence;

public interface IQueryRepositoryAsync<TEntity, TId> where TEntity : DomainEntityBase
{
    Task<IAsyncEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> spec);

    Task<IAsyncEnumerable<TResult>> GetAllAsync<TResult>(ISpecification<TEntity> spec) where TResult : class;

    Task<TEntity> GetByIdAsync(ISpecification<TEntity> spec);

    Task<TResult> GetByIdAsync<TResult>(ISpecification<TEntity> spec) where TResult : class, new();

    Task<TEntity> GetFirstOrDefaultAsync(ISpecification<TEntity> spec);

    Task<TResult> GetFirstOrDefaultAsync<TResult>(ISpecification<TEntity> spec) where TResult : class;

    Task<IAsyncEnumerable<TEntity>> GetPagedListAsync(ISpecification<TEntity> spec);

    Task<IAsyncEnumerable<TResult>> GetPagedListAsync<TResult>(ISpecification<TEntity> spec) where TResult : class, new();

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, T>>? selector = null);

    Task<T> MinAsync<T>(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, T>>? selector = null);

    Task<decimal> AverageAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, decimal>>? selector = null);

    Task<decimal> SumAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, decimal>>? selector = null);
}
