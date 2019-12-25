using RepositoryObject.RepositoryModel.Interface;
using RepositoryObject.UnitOfWorkModel.Interface;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
namespace RepositoryObject.RepositoryModel
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        #region Constant

        #endregion Constant

        #region Variable

        #endregion Variable

        #region Property

        public DbContext DbContext
        {
            get;
            set;
        }

        protected IUnitOfWork IUnitOfWork { get; set; }

        private DbSet<TEntity> DbSet { get; set; }

        #endregion Property

        #region Constructor

        public RepositoryBase(ref IUnitOfWork iuow)
        {
            this.IUnitOfWork = iuow;
            this.DbContext = iuow.DbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        #endregion Constructor

        #region Function

        public void Create(TEntity tb)
        {
            if (tb == null)
            {
                throw new ArgumentNullException("tbEntity");
            }
            else
            {
                this.DbSet.Add(tb);
            }
        }

        public void Delete(TEntity tb)
        {
            if (tb == null)
            {
                throw new ArgumentNullException("tbEntity");
            }
            else
            {
                this.DbContext.Entry(tb).State = EntityState.Deleted;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.DbSet.AsQueryable().AsNoTracking();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate).AsNoTracking();
        }

        public IQueryable<TEntity> GetAllWithTracking()
        {
            return DbSet;
        }

        public IQueryable<TEntity> GetAllWithTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }

        public TEntity GetWithTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DbSet.FirstOrDefault(predicate);
        }

        public void Update(TEntity tb)
        {
            if (tb == null)
            {
                throw new ArgumentNullException("tbEntity");
            }
            else
            {
                this.DbContext.Entry(tb).State = EntityState.Modified;
            }
        }

        protected virtual void Dispose(bool bolDisposing)
        {
            if (bolDisposing)
            {
                if (this.DbContext != null)
                {
                    this.DbContext.Dispose();
                    this.DbContext = null;
                }
            }
        }

        #endregion Function
    }
}
