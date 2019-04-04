using BaseObject.CommonObject;
using System.Xml;

namespace BaseObject.ApplicationService
{
    public abstract class ApplicationServiceBase
    {
        #region Constant

        #endregion Constant

        #region Variable

        protected TransactionProxy m_tranProxy;

        #endregion Variable

        #region Property

        public TransactionProxy TransactionProxy
        {
            get { return this.m_tranProxy; }
            set { this.m_tranProxy = value; }
        }

        #endregion Property

        #region Constructor

        public ApplicationServiceBase(string strConnectionString)
        {
            this.m_tranProxy = new TransactionProxy(strConnectionString);
        }

        public ApplicationServiceBase(ref TransactionProxy tranProxy)
        {
            this.m_tranProxy = tranProxy;
        }

        public ApplicationServiceBase(string strDataSource, string strInitialCatalog, string strUserID, string strPassword)
        {
            string _strSQL = "Data Source=" + strDataSource + ";"
                            + "Initial Catalog=" + strInitialCatalog + ";"
                            + "User Id=" + strUserID + ";"
                            + "Password=" + strPassword + ";";
            this.m_tranProxy = new TransactionProxy(_strSQL);
        }

        protected ApplicationServiceBase()
        { }

        #endregion Constructor

        #region Function

        //protected string GetConnectionString(string strDatabase)
        //{
        //    string _strConnectionString = "";
        //    string _strDataSource = "";
        //    string _strInitialCatalog = "";
        //    string _strUserID = "";
        //    string _strPassword = "";

        //    XmlDocument _xmlDocService = new XmlDocument();
        //    _xmlDocService.Load("Service.xml");

        //    XmlNodeList _xmlNodes = _xmlDocService.SelectNodes("/Database/" + strDatabase);

        //    foreach (XmlNode _xmlNode in _xmlNodes)
        //    {
        //        switch (_xmlNode.Name)
        //        {
        //            case "Data_Source":
        //                _strDataSource = _xmlNode.Attributes["value"].ToString();
        //                break;

        //            case "InitialCatalog":
        //                _strInitialCatalog = _xmlNode.Attributes["value"].ToString();
        //                break;

        //            case "UserID":
        //                _strUserID = _xmlNode.Attributes["value"].ToString();
        //                break;

        //            case "Password":
        //                _strPassword = _xmlNode.Attributes["value"].ToString();
        //                break;
        //        }
        //    }

        //    _strConnectionString = "Data Source=" + _strDataSource + ";" +
        //                                   "Initial Catalog=" + _strInitialCatalog + ";" +
        //                                   "User Id=" + _strUserID + ";" +
        //                                   "Password=" + _strPassword + ";";

        //    return _strConnectionString;
        //}

        #endregion Function

        #region Class

        #endregion Class
    }
}