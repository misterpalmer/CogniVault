using CogniVault.Platform.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CogniVault.Platform.Core.Persistence;
public static class SpecificationEvaluator<TEntity> where TEntity : class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
    {
        IQueryable<TEntity> query = inputQuery;

        if (specification.DisableTracking)
        {
            query = query.AsNoTracking();
        }

        if (specification.IgnoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }
        
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        foreach (var includeExpression in specification.Includes)
        {
            query = query.Include(includeExpression);
        }

        foreach (var includeString in specification.IncludeStrings)
        {
            query = query.Include(includeString);
        }

        if (specification.OrderBys != null && specification.OrderBys.Any())
        {
            // Apply ordering
            IOrderedQueryable<TEntity>? orderedQuery = null;
            foreach (var orderBy in specification.OrderBys)
            {
                if (orderedQuery == null)
                {
                    orderedQuery = orderBy.Descending 
                        ? query.OrderByDescending(orderBy.Expression) 
                        : query.OrderBy(orderBy.Expression);
                }
                else
                {
                    orderedQuery = orderBy.Descending 
                        ? orderedQuery.ThenByDescending(orderBy.Expression) 
                        : orderedQuery.ThenBy(orderBy.Expression);
                }
            }

            if (orderedQuery != null)
            {
                query = orderedQuery;
            }
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }
}

