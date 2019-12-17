using RepositoryObject.RepositoryModel.Interface;
using RepositoryObject.UnitOfWorkModel.Interface;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
namespace RepositoryObject.RepositoryModel
{
    public class RepositoryBase<tbEntity> : IRepository<tbEntity>
        where tbEntity : class
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

        private DbSet<tbEntity> DbSet { get; set; }

        #endregion Property

        #region Constructor

        public RepositoryBase(ref IUnitOfWork iuow)
        {
            this.IUnitOfWork = iuow;
            this.DbContext = iuow.DbContext;
            this.DbSet = this.DbContext.Set<tbEntity>();
        }

        #endregion Constructor

        #region Function

        public void Create(tbEntity tb)
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

        public void Delete(tbEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("tbEntity");
            }
            else
            {
                this.DbContext.Entry(instance).State = EntityState.Deleted;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public tbEntity Get(Expression<Func<tbEntity, bool>> predicate)
        {
            return this.DbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public IQueryable<tbEntity> GetAll()
        {
            return this.DbSet.AsQueryable().AsNoTracking();
        }

        public IQueryable<tbEntity> GetAll(Expression<Func<tbEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate).AsNoTracking();
        }

        public IQueryable<tbEntity> GetAllWithTracking()
        {
            return DbSet;
        }

        public IQueryable<tbEntity> GetAllWithTracking(Expression<Func<tbEntity, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }

        public tbEntity GetWithTracking(Expression<Func<tbEntity, bool>> predicate)
        {
            return this.DbSet.FirstOrDefault(predicate);
        }

        public void Update(tbEntity tb)
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
