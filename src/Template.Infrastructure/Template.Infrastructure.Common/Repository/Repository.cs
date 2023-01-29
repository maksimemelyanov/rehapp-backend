using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Template.Infrastructure.Common.Interfaces;

namespace Template.Infrastructure.Common.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IIdentified, new()
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public abstract Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    public abstract Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> selector, CancellationToken cancellationToken);

    public virtual IQueryable<TEntity> All()
    {
        return _context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> AllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().AnyAsync(selector, cancellationToken);
    }

    public virtual void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().CountAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().Where(filter).CountAsync(cancellationToken);
    }
}
