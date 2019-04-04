using BaseObject.CommonObject;

namespace BaseObject.BusinessObject
{
    public abstract class BusinessObjectBase
    {
        #region Constant

        #endregion Constant

        #region Variable

        public TransactionProxy m_tranProxy;

        #endregion Variable

        #region Property

        #endregion Property

        #region Constructor

        public BusinessObjectBase()
        {
        }

        public BusinessObjectBase(ref TransactionProxy tranProxy)
        {
            this.m_tranProxy = tranProxy;
        }

        #endregion Constructor

        #region Function

        #endregion Function

        #region Class

        #endregion Class
    }
}