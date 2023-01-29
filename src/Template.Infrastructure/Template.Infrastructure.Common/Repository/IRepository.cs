using System.Linq.Expressions;

namespace Template.Infrastructure.Common.Repository;

public interface IRepository<TEntity> where TEntity : class, new()
{
    public IQueryable<TEntity> All();

    public Task<List<TEntity>> AllAsync(CancellationToken cancellationToken);

    public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> selector, CancellationToken cancellationToken);

    public Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> selector, CancellationToken cancellationToken);

    public void Remove(TEntity entity);

    public void RemoveRange(IEnumerable<TEntity> entities);

    public void Update(TEntity entity);

    public Task SaveChangesAsync(CancellationToken cancellationToken);

    public Task<int> CountAsync(CancellationToken cancellationToken);

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
}
