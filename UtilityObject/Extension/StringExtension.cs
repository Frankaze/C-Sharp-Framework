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
        /// 物件轉字串
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string ToStringOrEmpty(this Object strInput)
        {
            return strInput == null ? string.Empty : strInput.ToString();
        }

        #endregion Function

        #region Class

        #endregion Class
    }
}