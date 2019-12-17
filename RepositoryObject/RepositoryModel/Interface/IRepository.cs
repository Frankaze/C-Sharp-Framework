using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryObject.RepositoryModel.Interface
{
    public interface IRepository<tbEntity> : IDisposable
           where tbEntity : class
    {
        void Create(tbEntity tb);

        void Delete(tbEntity tb);

        tbEntity Get(Expression<Func<tbEntity, bool>> predicate);

        tbEntity GetWithAsNoTracking(Expression<Func<tbEntity, bool>> predicate);

        IQueryable<tbEntity> GetAll();

        IQueryable<tbEntity> GetAll(Expression<Func<tbEntity, bool>> predicate);

        IQueryable<tbEntity> GetAllWithAsNoTracking();

        IQueryable<tbEntity> GetAllWithAsNoTracking(Expression<Func<tbEntity, bool>> predicate);

        void Update(tbEntity tb);
    }
}
