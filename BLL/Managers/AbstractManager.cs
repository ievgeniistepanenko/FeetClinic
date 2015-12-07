using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BE.Interfaces;
using DomainModel.BLL.Interfaces;
using FeetClinic_DAL.Abstarct;
using FeetClinic_DAL.Conrete;

namespace BLL.Managers
{
    public abstract class AbstractManager<TEntity>  where TEntity: class, IEntity 
    {
        protected DalFacade Facade { get; }
        protected IRepository<TEntity> Repository;


        protected AbstractManager()
        {
            Facade = new DalFacade();
        }

        protected abstract IRepository<TEntity> GetRepository() ;

        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetAll("");
        }
        public virtual IEnumerable<TEntity> GetAll(string properties)
        {
            return GetRepository().GetAll(null,null,properties);
        }

        public virtual TEntity GetOne(int id)
        {
            return GetOne(id,"");
        }
        public virtual TEntity GetOne(int id,string properties)
        {
            return GetRepository().GetOne(ad => ad.Id == id,properties);
        }

        public virtual TEntity Update(TEntity entity)
        {
            TEntity e = GetRepository().Update(entity);
            Save();
            return e;
        }
        public virtual TEntity Create(TEntity entity)
        {
            TEntity e = GetRepository().Create(entity);
            Save();
            return e;
        }
        public virtual TEntity Delete(TEntity entity)
        {
            return Delete(entity.Id);
        }
        public TEntity Delete(int id)
        {
            TEntity e = GetRepository().Delete(id);
            Save();
            return e;
        }

        public bool Any(Expression<Func<TEntity,bool>> filter)
        {
            return Repository.Any(filter);
        }

        public void Dispose()
        {
            Facade.Dispose();
        }

        private void Save()
        {
            Facade.Save();
        }
    }
}
