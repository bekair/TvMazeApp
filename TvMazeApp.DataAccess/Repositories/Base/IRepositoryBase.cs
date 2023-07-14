using System.Linq.Expressions;
using TvMazeApp.Entity.Base;

namespace TvMazeApp.DataAccess.Repositories.Base;

public interface IRepositoryBase<TEntity>
    where TEntity : class, IEntity
{
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null);
    TEntity GetIncludeBy(int id, params Expression<Func<TEntity, object>>[] includeExpressions);
    TEntity GetById(int id);
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}