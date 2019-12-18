using RepositoryObject.UnitOfWorkModel.Interface;
using System;
using System.Data.Entity;

namespace RepositoryObject.UnitOfWorkModel
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constant

        #endregion Constant

        #region Variable

        private bool m_bolDisposed = false;
        private bool m_bolIsSubUnit = false;
        private DbContext m_dbContext;
        private IDbContextFactory m_idbContextFactory { get; set; }

        #endregion Variable

        #region Property

        public UnitOfWork(IDbContextFactory idbContextFactory)
        {
            this.m_idbContextFactory = idbContextFactory;
        }

        public UnitOfWork(ref DbContext dbContext, bool bolIsSubUnit)
        {
            this.m_dbContext = dbContext;
            this.m_bolIsSubUnit = bolIsSubUnit;
        }

        public DbContext DbContext
        {
            get
            {
                if (this.m_dbContext != null)
                {
                    return this.m_dbContext;
                }
                this.m_dbContext = this.m_idbContextFactory.GetDbContext();
                return this.m_dbContext;
            }
        }

        public bool IsSubUnit
        {
            get
            {
                return this.m_bolIsSubUnit;
            }
        }

        #endregion Property

        #region Constructor

        #endregion Constructor

        #region Function

        public IUnitOfWork CreateSubUnit()
        {
            if (this.m_dbContext == null)
            {
                this.m_dbContext = this.m_idbContextFactory.GetDbContext();
            }

            return new UnitOfWork(ref this.m_dbContext, true);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChange()
        {
            if (this.m_bolIsSubUnit == false)
            {
                return this.DbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        protected virtual void Dispose(bool bolDisposing)
        {
            if (!this.m_bolDisposed)
            {
                if (bolDisposing)
                {
                    this.DbContext.Dispose();
                    this.m_dbContext = null;
                }
            }
            this.m_bolDisposed = true;
        }

        #endregion Function
    }
}