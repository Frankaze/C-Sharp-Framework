using RepositoryObject.UnitOfWorkModel.Interface;
using System;
using System.Data.Entity;

namespace RepositoryObject.UnitOfWorkModel
{
    public class DbContextFactory : IDbContextFactory
    {
        #region Constant

        #endregion Constant

        #region Variable

        private DbContext m_dbContext;
        private string m_strConnectionString = string.Empty;

        #endregion Variable

        #region Property

        private DbContext DbContext
        {
            get
            {
                if (this.m_dbContext == null)
                {
                    Type _type = typeof(DbContext);
                    this.m_dbContext = (DbContext)Activator.CreateInstance(_type, this.m_strConnectionString);
                }
                return m_dbContext;
            }
        }

        public DbContext GetDbContext()
        {
            return this.DbContext;
        }

        #endregion Property

        #region Constructor

        public DbContextFactory(string strConnectionString)
        {
            this.m_strConnectionString = strConnectionString;
        }

        #endregion Constructor

        #region Function

        #endregion Function
    }
}