using System.DirectoryServices;

namespace UtilityObject.ActiveDirectory
{
    public static class ActiveDirectory
    {
        #region Constant

        #endregion Constant

        #region Variable

        #endregion Variable

        #region Property

        #endregion Property

        #region Constructor

        #endregion Constructor

        #region Function

        /// <summary>
        /// 連接Active Directory驗證帳號密碼
        /// </summary>
        /// <param name="strDomain">Active Directory的網域名稱或IP</param>
        /// <param name="strUserName">登入帳號</param>
        /// <param name="strPassword">登入密碼</param>
        /// <returns>是否授權成功</returns>
        public static bool IsAuthenticated(string strDomain, string strUserName, string strPassword)
        {
            DirectoryEntry _objDirectoryEntry = new DirectoryEntry("LDAP://" + strDomain, strUserName, strPassword);

            DirectorySearcher _objDirectorySearcher = new DirectorySearcher(_objDirectoryEntry);
            _objDirectorySearcher.Filter = "(sAMAccountName=" + strUserName + ")";
            try
            {
                SearchResult _objSearchResult = _objDirectorySearcher.FindOne();

                if (_objSearchResult != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion Function

        #region Class

        #endregion Class
    }
}