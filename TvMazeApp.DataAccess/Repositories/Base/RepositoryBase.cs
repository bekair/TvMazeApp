﻿using System.Linq.Expressions;
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

        protected RepositoryBase(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetIncludeByAsync(Expression<Func<TEntity, bool>>? filter = null, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> queryableEntity = _dbSet;
            if (filter != null)
                queryableEntity = queryableEntity.Where(filter);
            
            queryableEntity = includeExpressions.Aggregate(queryableEntity, (current, includeExpression) => current.Include(includeExpression));
            
            return await queryableEntity.FirstOrDefaultAsync() ?? throw new DataNotFoundException(string.Format(AppConstant.ErrorMessage.DbDataNotFound, typeof(TEntity).Name));
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
}