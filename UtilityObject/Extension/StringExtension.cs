using System;

namespace UtilityObject.Extension
{
    public static class StringExtension
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
        /// 首字調整為大寫，其餘保持原貌
        /// </summary>
        /// <param name="strInput">輸入字串</param>
        /// <returns>首字調整為大寫的輸入字串</returns>
        public static string FormatFirstCharToUpper(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput))
            {
                return strInput;
            }

            return strInput.Substring(0, 1).ToUpper() + (strInput.Length > 1 ? strInput.Substring(1) : string.Empty);
        }

        /// <summary>
        /// 首字大寫，其餘小寫
        /// </summary>
        /// <param name="strInput">輸入字串</param>
        /// <returns>首字大寫，其餘小寫的輸入字串</returns>
        public static string FormatOnlyFirstCharToUpper(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput))
            {
                return strInput;
            }

            return strInput.Substring(0, 1).ToUpper() + (strInput.Length > 1 ? strInput.Substring(1).ToLower() : string.Empty);
        }

        /// <summary>
        /// 取得遇到第一個Char前的字串
        /// </summary>
        /// <param name="strInput">輸入字串</param>
        /// <param name="charIndexOf">第一個Char</param>
        /// <returns>第一個Char前的字串</returns>
        public static string FirstIndexOfString(this string strInput, char charIndexOf)
        {
            int _intIndex = strInput.IndexOf(charIndexOf);

            if (_intIndex == -1)
            {
                return strInput;
            }
            else
            {
                return strInput.Substring(0, _intIndex);
            }
        }

        /// <summary>
        /// 物件轉字串
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string ToStringOrEmpty(this Object strInput)
        {
            return strInput == null ? string.Empty : strInput.ToString();
        }

        /// <summary>
        /// 將 TimeStamp 轉換回時間
        /// </summary>
        /// <param name="strInput">TimeStamp</param>
        /// <returns>時間， Null 表示字串格式不正確</returns>
        public static DateTime? UnixTimeStampToDate(this string strInput)
        {
            double _douTimeStamp;

            if (double.TryParse(strInput, out _douTimeStamp) == true)
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return dtStart.AddSeconds(_douTimeStamp);
            }
            else
            {
                return null;
            }
        }

        #endregion Function

        #region Class

        #endregion Class
    }
}