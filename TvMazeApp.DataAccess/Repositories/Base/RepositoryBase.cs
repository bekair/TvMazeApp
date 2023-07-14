using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TvMazeApp.Core.Constants;
using TvMazeApp.Core.Exceptions;
using TvMazeApp.Entity.Base;

namespace TvMazeApp.DataAccess.Repositories.Base;

public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public virtual TEntity GetIncludeBy(int id, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var queryableEntity = _dbSet.Where(x => x.Id == id);
            queryableEntity = includeExpressions.Aggregate(queryableEntity, (current, includeExpression) => current.Include(includeExpression));
            
            return queryableEntity.FirstOrDefault() ?? throw new DataNotFoundException(string.Format(AppConstant.ErrorMessage.DbDataNotFound, typeof(TEntity).Name));
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id) ?? throw new DataNotFoundException(string.Format(AppConstant.ErrorMessage.DbDataNotFound, typeof(TEntity).Name));
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }
    }