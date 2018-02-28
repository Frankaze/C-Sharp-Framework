using BaseObject.CommonObject;

namespace BaseObject.DataAccessObject
{
    public class PagingDataSourceBase : DataAccessObjectBase
    {
        public PagingDataSourceBase(ref TransactionProxy tranProxy) : base(ref tranProxy)
        {
        }
    }
}