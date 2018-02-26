using System.DirectoryServices;

namespace UtilityObject.ActiveDirectory
{
    public static class ActiveDirectory
    {
        /// <summary>
        /// 連接Active Directory驗證帳號密碼
        /// </summary>
        /// <param name="strDomain">Active Directory的網域名稱或IP</param>
        /// <param name="strUserName">登入帳號</param>
        /// <param name="strPassword">登入密碼</param>
        /// <returns></returns>
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
    }
}