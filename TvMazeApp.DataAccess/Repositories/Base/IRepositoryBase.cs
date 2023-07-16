using System.Linq.Expressions;
using TvMazeApp.Entity.Base;

namespace TvMazeApp.DataAccess.Repositories.Base;

public interface IRepositoryBase<TEntity>
    where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task<TEntity> GetIncludeByAsync(int id, params Expression<Func<TEntity, object>>[] includeExpressions);
    Task<TEntity> GetByIdAsync(int id);
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}