using HelpDeskPro.Criterias;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelpDeskPro.Data.Repositories
{
    public abstract class RepositoryBase<TEntity>(HelpDeskProContext context) where TEntity : class
    {
        protected readonly HelpDeskProContext _context = context;
        protected readonly DbSet<TEntity> _set = context.Set<TEntity>();

        /// <summary>
        /// Aplica todos los criterios sobre un IQueryable.
        /// </summary>
        protected IQueryable<TEntity> ApplyCriteria(
            IQueryable<TEntity> query,
            IEnumerable<CriteriaBase<TEntity>>? criteria)
        {
            if (criteria is null) return query;

            foreach (var c in criteria)
            {
                if (c is null) continue;

                Expression<Func<TEntity, bool>> predicate = c.BuildExpression();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
            }

            return query;
        }

        /// <summary>
        /// Aplica paginación y devuelve (items, total).
        /// </summary>
        protected async Task<PagedResult<TEntity>> ToPagedResultAsync(
            IQueryable<TEntity> query,
            int pageNumber,
            int pageSize
        )
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 20;

            int total = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TEntity>
            {
                Items = items,
                TotalCount = total
            };
        }
    }
}
