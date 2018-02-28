using BaseObject.CommonObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace BaseObject.DataAccessObject
{
    public abstract class DataAccessObjectBase
    {
        #region Constant

        private const string FOR_UPDATE_PARAMETER_TRAILER = "_ForUpdate";

        #endregion Constant

        #region Variable

        protected SqlCommand m_sqlCmd;
        protected SqlDataAdapter m_sqlDa;
        protected StringBuilder m_strBuilder;

        //XML位置
        protected string m_strXMLFile;

        protected TransactionProxy m_tranProxy;

        #endregion Variable

        #region Property

        public SqlConnection SqlConnection
        {
            get { return this.m_tranProxy.SqlConnection; }
        }

        #endregion Property

        #region Constructor

        public DataAccessObjectBase()
        {
        }

        public DataAccessObjectBase(ref TransactionProxy tranProxy)
        {
            this.m_tranProxy = tranProxy;
            this.m_sqlCmd = new SqlCommand();
            this.m_sqlCmd.CommandTimeout = 600;
            this.m_strBuilder = new StringBuilder();
        }

        public DataAccessObjectBase(string strXMLFile)
        {
            this.m_strXMLFile = strXMLFile;
        }

        #endregion Constructor

        #region Function

        /// <summary>
        /// Data Access Object 執行 Fuction 前清除 SqlCommnad 與 StringBuilder 的內容
        /// </summary>
        protected void ClearSqlContext()
        {
            this.m_strBuilder.Clear();
            this.m_sqlCmd.CommandText = "";
            this.m_sqlCmd.Parameters.Clear();
        }

        /// <summary>
        /// 產生In 的SQL筆法
        /// </summary>
        /// <param name="strList">字串列表</param>
        /// <returns>In 的SQL筆法</returns>
        protected string CreateInListSQL(List<string> strList)
        {
            string _strResult = String.Empty;

            foreach (string _str in strList)
            {
                if (_strResult == String.Empty)
                {
                    _strResult = "N'" + _str + "'";
                }
                else
                {
                    _strResult = _strResult + "," + "N'" + _str + "'";
                }
            }

            if (String.IsNullOrWhiteSpace(_strResult) == true)
            {
                _strResult = "''";
            }

            return _strResult;
        }

        /// <summary>
        /// 產生In 的SQL筆法
        /// </summary>
        /// <param name="strList">字串列表，必須均數字</param>
        /// <returns> In 的SQL筆法</returns>
        protected string CreateInListSQLToInt(List<string> strList)
        {
            string _strResult = String.Empty;

            foreach (string _str in strList)
            {
                if (_strResult == String.Empty)
                {
                    _strResult = _str;
                }
                else
                {
                    _strResult = _strResult + "," + _str;
                }
            }

            if (String.IsNullOrWhiteSpace(_strResult) == true)
            {
                _strResult = "''";
            }

            return _strResult;
        }

        /// <summary>
        /// 產生In 的SQL筆法
        /// </summary>
        /// <param name="intList">字串列表，必須均數字</param>
        /// <returns> In 的SQL筆法</returns>
        protected string CreateInListSQLToInt(List<int> intList)
        {
            string _strResult = String.Empty;

            foreach (int _int in intList)
            {
                if (_strResult == String.Empty)
                {
                    _strResult = _int.ToString();
                }
                else
                {
                    _strResult = _strResult + "," + _int;
                }
            }

            if (String.IsNullOrWhiteSpace(_strResult) == true)
            {
                _strResult = "''";
            }

            return _strResult;
        }

        protected string CreateInsertPropertySQL(DataTransferObject.DataTransferObjectBase dtoBase)
        {
            string _strSQL = "";
            string _strFrontSQL = "";
            string _strBehindSQL = "";
            foreach (PropertyInfo _property in dtoBase.GetUpdatePropertyList())
            {
                if (_property.Name != "RowStatus")
                {
                    if (_strFrontSQL == "")
                    {
                        _strFrontSQL = " [" + _property.Name + "]";
                    }
                    else if (_strFrontSQL != "")
                    {
                        _strFrontSQL = _strFrontSQL + " , [" + _property.Name + "]";
                    }

                    if (_strBehindSQL == "")
                    {
                        _strBehindSQL = " @" + _property.Name;
                    }
                    else if (_strBehindSQL != "")
                    {
                        _strBehindSQL = _strBehindSQL + ", @" + _property.Name;
                    }
                }
            }
            _strSQL = " ( " + _strFrontSQL + " ) VALUES ( " + _strBehindSQL + " ) ";
            return _strSQL;
        }

        protected string CreateInsertPropertySQL(DataTransferObject.DataTransferObjectBase dtoBase, string strOutputField)
        {
            string _strSQL = "";
            string _strFrontSQL = "";
            string _strBehindSQL = "";
            foreach (PropertyInfo _property in dtoBase.GetUpdatePropertyList())
            {
                if (_property.Name != "RowStatus")
                {
                    if (_strFrontSQL == "")
                    {
                        _strFrontSQL = " [" + _property.Name + "]";
                    }
                    else if (_strFrontSQL != "")
                    {
                        _strFrontSQL = _strFrontSQL + " , [" + _property.Name + "]";
                    }

                    if (_strBehindSQL == "")
                    {
                        _strBehindSQL = " @" + _property.Name;
                    }
                    else if (_strBehindSQL != "")
                    {
                        _strBehindSQL = _strBehindSQL + ", @" + _property.Name;
                    }
                }
            }
            _strSQL = " ( " + _strFrontSQL + " ) OUTPUT INSERTED." + strOutputField + " VALUES ( " + _strBehindSQL + " ) ";
            return _strSQL;
        }

        /// <summary>
        /// 產生DTO所有欄位的SQL字串，用於SELECT的筆法
        /// </summary>
        /// <param name="dtoBase">要使用SELECT筆法的DTO</param>
        /// <returns>SELECT欄位的SQL字串</returns>
        protected string CreateSelectPropertySQL(DataTransferObject.DataTransferObjectBase dtoBase)
        {
            return this.CreateSelectPropertySQL(dtoBase, String.Empty);
        }

        /// <summary>
        /// 產生DTO所有欄位的SQL字串，用於SELECT的筆法
        /// </summary>
        /// <param name="dtoBase">要使用SELECT筆法的DTO</param>
        /// <param name="strTableSymbol">欄位的代號</param>
        /// <returns>SELECT欄位的SQL字串</returns>
        protected string CreateSelectPropertySQL(DataTransferObject.DataTransferObjectBase dtoBase, string strTableSymbol)
        {
            string _strSQL = String.Empty;

            PropertyInfo[] _properties = dtoBase.GetType().GetProperties();

            foreach (PropertyInfo _property in _properties)
            {
                if (_property.Name != "RowStatus")
                {
                    if (String.IsNullOrWhiteSpace(_strSQL) == false)
                    {
                        _strSQL = _strSQL + " , ";
                    }

                    _strSQL = _strSQL + (strTableSymbol == String.Empty ? String.Empty : strTableSymbol + ".") + "[" + _property.Name + "]";
                }
            }

            return _strSQL;
        }

        protected string CreateUpdatePropertySQL(DataTransferObject.DataTransferObjectBase dtoBase)
        {
            string _strSQL = "";

            foreach (PropertyInfo _property in dtoBase.GetUpdatePropertyList())
            {
                if (_property.Name != "RowStatus")
                {
                    if (_strSQL != "")
                    {
                        _strSQL = _strSQL + " , ";
                    }

                    _strSQL = _strSQL + _property.Name + "=" + "@" + _property.Name + FOR_UPDATE_PARAMETER_TRAILER;
                }
            }

            foreach (PropertyInfo _property in dtoBase.GetAccumulatePropertyList())
            {
                if (_property.Name != "RowStatus")
                {
                    if (_strSQL != "")
                    {
                        _strSQL = _strSQL + " , ";
                    }

                    switch (_property.PropertyType.Name)
                    {
                        case "Int32":
                        case "Double":
                        case "Decimal":
                        case "String":
                            _strSQL = _strSQL + _property.Name + "=" + _property.Name + "+" + "@" + _property.Name + FOR_UPDATE_PARAMETER_TRAILER;
                            break;

                        default:
                            _strSQL = _strSQL + _property.Name + "=" + "@" + _property.Name + FOR_UPDATE_PARAMETER_TRAILER;
                            break;
                    }
                }
            }
            return _strSQL;
        }

        protected void FillSqlCommmandParameter(DataTransferObject.DataTransferObjectBase dtoBase, ref SqlCommand sqlCmd)
        {
            foreach (PropertyInfo _property in dtoBase.GetUpdatePropertyList())
            {
                if (_property.Name != "RowStatus")
                {
                    sqlCmd.Parameters.AddWithValue("@" + _property.Name, _property.GetValue(dtoBase, null));
                }
            }

            foreach (PropertyInfo _property in dtoBase.GetAccumulatePropertyList())
            {
                if (_property.Name != "RowStatus")
                {
                    sqlCmd.Parameters.AddWithValue("@" + _property.Name, _property.GetValue(dtoBase, null));
                }
            }
        }

        protected void FillSqlCommmandParameterForUpdate(DataTransferObject.DataTransferObjectBase dtoBase, ref SqlCommand sqlCmd)
        {
            foreach (PropertyInfo _property in dtoBase.GetUpdatePropertyList())
            {
                if (_property.Name != "RowStatus")
                {
                    sqlCmd.Parameters.AddWithValue("@" + _property.Name + FOR_UPDATE_PARAMETER_TRAILER, _property.GetValue(dtoBase, null));
                }
            }

            foreach (PropertyInfo _property in dtoBase.GetAccumulatePropertyList())
            {
                if (_property.Name != "RowStatus")
                {
                    sqlCmd.Parameters.AddWithValue("@" + _property.Name + FOR_UPDATE_PARAMETER_TRAILER, _property.GetValue(dtoBase, null));
                }
            }
        }

        #endregion Function
    }
}