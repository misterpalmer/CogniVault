using CogniVault.Platform.Core.Abstractions.Persistence;

using Microsoft.EntityFrameworkCore;

namespace CogniVault.Platform.Identity.EFCoreProvider;

public class IdentityDbResolver : IDbResolver
{
    private readonly IdentityContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public IdentityDbResolver(IdentityContext context) => _context = context;

    public IRepositoryAsync<TEntity> GetContext<TEntity>() where TEntity : class
    {
        return GetRepository<TEntity>();
    }

    public IRepositoryAsync<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryAsync<TEntity>(_context);
        return (IRepositoryAsync<TEntity>) _repositories[type];
    }
}