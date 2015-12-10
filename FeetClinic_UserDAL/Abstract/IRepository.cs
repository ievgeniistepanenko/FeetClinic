using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FeetClinic_UserDAL.Abstract
{
    public interface IRepository<TEntity> where TEntity:class 
    {
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");
        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(int id);
        TEntity Delete(TEntity entity);
        int Count(Expression<Func<TEntity, bool>> filter = null);
        bool Any(Expression<Func<TEntity, bool>> filter = null);
    }
}
