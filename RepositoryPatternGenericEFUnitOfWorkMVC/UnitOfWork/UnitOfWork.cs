using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace RepositoryPatternGenericEFUnitOfWorkMVC.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext : DbContext, new()
    {
        private bool _disposed;
        private string _errorMsg = string.Empty;
        private DbContextTransaction _tran;
        public UnitOfWork()
        {
            Context = new TContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public TContext Context { get; }
  
        public void CreateTransaction()
        {
            _tran = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _tran.Commit();
        }

        public void Rollback()
        {
            _tran.Rollback();
            _tran.Dispose();
        }

        public void Save()
        {
            try
            { 
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMsg = _errorMsg + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
                    }
                }
                throw new Exception(_errorMsg, dbEx);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();
            _disposed = true;
        }
    }
}