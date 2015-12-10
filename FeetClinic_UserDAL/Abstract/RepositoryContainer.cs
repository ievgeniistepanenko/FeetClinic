using System;
using System.Data.Entity;

namespace FeetClinic_UserDAL.Abstract
{
    
    public abstract class RepositoryContainer<TContext>: IDisposable where TContext:DbContext
    {
        protected TContext Context;

        public virtual void Save()
        {
                Context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
