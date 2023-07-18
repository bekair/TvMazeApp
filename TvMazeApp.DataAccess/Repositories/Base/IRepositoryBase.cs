using System.Linq.Expressions;
using TvMazeApp.Entity.Base;

namespace TvMazeApp.DataAccess.Repositories.Base;

public interface IRepositoryBase<TEntity>
    where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task<TEntity> GetIncludeByAsync(Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[] includeExpressions);
    void Insert(TEntity entity);
    void Update(TEntity entity);
}