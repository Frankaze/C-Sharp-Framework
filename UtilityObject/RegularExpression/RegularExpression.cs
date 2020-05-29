using System.Text.RegularExpressions;

namespace UtilityObject.RegularExpression
{
    public static class RegularExpression
    {
        #region Constant

        private const string CELLPHONE_TW_REGULAR_EXPRESSION = @"^09[0-9]{8}";
        //private const string CURRENCY_NTD_REGULAR_EXPRESSION = "^(0|[1-9][0-9]*)$";
        private const string EMAIL_REGULAR_EXPRESSION = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //private const string POSITIVE_INTEGER_REGULAR_EXPRESSION = @"^[1-9][0-9]*$";

        #endregion Constant

        #region Variable

        private static Regex m_regex;

        #endregion Variable

        #region Property

        #endregion Property

        #region Constructor

        #endregion Constructor

        #region Function

        /// <summary>
        /// 使用現有正規化描述判別格式
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="enumRegularExpression"></param>
        /// <returns></returns>
        public static bool CheckRegularExpressionForUseEnumRegularExpression(string strInput, RegularExpressionEnum enumRegularExpression)
        {
            string _strRegularExpression = string.Empty;

            switch (enumRegularExpression)
            {
                case RegularExpressionEnum.Email:
                    _strRegularExpression = EMAIL_REGULAR_EXPRESSION;
                    break;

                case RegularExpressionEnum.CellPhone_TW:
                    _strRegularExpression = CELLPHONE_TW_REGULAR_EXPRESSION;
                    break;

                //case RegularExpressionEnum.Currency_NTD:
                //    _strRegularExpression = CURRENCY_NTD_REGULAR_EXPRESSION;
                //    break;

                //case RegularExpressionEnum.PositiveInteger:
                //    _strRegularExpression = POSITIVE_INTEGER_REGULAR_EXPRESSION;
                //    break;

                default:
                    _strRegularExpression = string.Empty;
                    break;
            }

            if (string.IsNullOrWhiteSpace(_strRegularExpression) == false)
            {
                m_regex = new Regex(_strRegularExpression);
                return m_regex.IsMatch(strInput);
            }
            else
            {
                return false;
            }
        }

        public static bool CheckString(string strInput, string strRegularExpression)
        {
            m_regex = new Regex(strRegularExpression, RegexOptions.IgnoreCase);
            return m_regex.IsMatch(strInput);
        }

        #endregion Function

        #region Class

        #endregion Class

        #region Enum

        public enum RegularExpressionEnum
        {
            Email,
            CellPhone_TW,
            //Currency_NTD,
            //PositiveInteger // 正整數
        }

        #endregion Enum
    }
}