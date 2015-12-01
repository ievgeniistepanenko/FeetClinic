using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FeetClinic_DAL.Abstarct;

namespace FeetClinic_DAL.Conrete
{
    public class Repository<TContext,TEntity> : IRepository<TEntity>
        where TContext:DbContext where TEntity:class 
    {
        internal TContext Context;
        internal DbSet<TEntity> DbSet;

        public Repository(TContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }
        
        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.
                Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.Distinct().ToList();
            }
        }

        public TEntity GetOne(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            return GetAll(filter, null, includeProperties).SingleOrDefault();
        }

        public TEntity Create(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        public TEntity Delete(int id)
        {
            var entity = GetById(id);
            return Delete(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            AttachIfDetached(entity);
            return DbSet.Remove(entity);
        }
        public TEntity Update(TEntity entity)
        {
            AttachIfDetached(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetAll(filter).Count();
        }

        public bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            return Count(filter) > 0;
        }

        private TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }
        private void AttachIfDetached(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
        }
    }
}
