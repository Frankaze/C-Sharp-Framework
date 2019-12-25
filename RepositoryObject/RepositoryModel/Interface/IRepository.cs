using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryObject.RepositoryModel.Interface
{
    public interface IRepository<TEntity> : IDisposable
           where TEntity : class
    {
        void Create(TEntity tb);

        void Delete(TEntity tb);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAllWithTracking();

        IQueryable<TEntity> GetAllWithTracking(Expression<Func<TEntity, bool>> predicate);

        TEntity GetWithTracking(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity tb);
    }
}
