using AutoMapper;
using RepositoryObject.UnitOfWorkModel.Interface;
using RepositoryObject.UnitOfWorkModel;

namespace RepositoryObject.ApplicationService
{
    public abstract class ApplicationServiceBase
    {
        #region Constant

        #endregion Constant

        #region Variable

        protected IMapper m_imapper;
        protected IUnitOfWork m_iuow;
        protected Profile m_pof;

        #endregion Variable

        #region Property

        #endregion Property

        #region Constructor

        public ApplicationServiceBase(Profile pof)
        {
            this.m_pof = pof;
            this.CreateMap();
        }

        public ApplicationServiceBase(string strConnectionString)
        {
            this.m_iuow = new UnitOfWork(new DbContextFactory(strConnectionString));
            this.CreateMap();
        }

        public ApplicationServiceBase(string strConnectionString, Profile pof)
        {
            this.m_pof = pof;
            this.m_iuow = new UnitOfWork(new DbContextFactory(strConnectionString));
            this.CreateMap();
        }

        public ApplicationServiceBase(IUnitOfWork iuow)
        {
            this.m_iuow = iuow;
            this.CreateMap();
        }

        public ApplicationServiceBase(ref IUnitOfWork iuow, Profile pof)
        {
            this.m_pof = pof;
            this.m_iuow = iuow;
            this.CreateMap();
        }

        private ApplicationServiceBase()
        {
            this.CreateMap();
        }

        #endregion Constructor

        #region Function

        protected virtual void CreateMap()
        {
            var _config = new MapperConfiguration(_cfg =>
            {
                _cfg.AddProfile(this.m_pof);
            });

            //_config.AssertConfigurationIsValid();//驗證mapper

            this.m_imapper = _config.CreateMapper();
        }

        #endregion Function
    }

    public abstract class ProfileBase : Profile
    {
    }
}