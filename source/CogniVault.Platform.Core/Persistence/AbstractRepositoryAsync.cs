namespace CogniVault.Platform.Core.Persistence;

public class AbstractRepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : AbstractBase
{
    private readonly IDbResolver _dbResolver;

    public AbstractRepositoryAsync(IDbResolver dbResolver)
    {
        _dbResolver = dbResolver;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var context = _dbResolver.GetContext<TEntity>();
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var context = _dbResolver.GetContext<TEntity>();
        context.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var context = _dbResolver.GetContext<TEntity>();
        context.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var context = _dbResolver.GetContext<TEntity>();
        return await context.FindAsync<TEntity>(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var context = _dbResolver.GetContext<TEntity>();
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
    {
        var context = _dbResolver.GetContext<TEntity>();
        return await context.Set<TEntity>().Where(expression).ToListAsync();
    }
}