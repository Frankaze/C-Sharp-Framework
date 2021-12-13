using System;

namespace UtilityObject.Extension
{
    public static class DecimalExtension
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
        /// 計算進位的數值
        /// </summary>
        /// <param name="decInput">輸入金額</param>
        /// <param name="enumMathMethodCarry">進位方式Enum</param>
        /// <returns>進位後的數值</returns>
        public static decimal Carry(this decimal decInput, DecimalExtension.MathCarryMethodEnum enumMathMethodCarry)
        {
            switch (enumMathMethodCarry)
            {
                case MathCarryMethodEnum.RoundToOnePlace:
                    return Math.Round(decInput, 1);

                case MathCarryMethodEnum.RoundToTwoPlace:
                    return Math.Round(decInput, 2);

                case MathCarryMethodEnum.RoundToThreePlace:
                    return Math.Round(decInput, 3);

                case MathCarryMethodEnum.RoundToFourPlace:
                    return Math.Round(decInput, 4);

                case MathCarryMethodEnum.Ceiling:
                    return Math.Ceiling(decInput);

                case MathCarryMethodEnum.Floor:
                    return Math.Floor(decInput);

                default:
                case MathCarryMethodEnum.None:
                    return decInput;
            }
        }

        /// <summary>
        /// Decimal轉換成字串時，移除小點數右方多餘的0
        /// </summary>
        /// <param name="decInput">輸入金額</param>
        /// <returns>移除小點數右方多餘的0字串</returns>
        public static string ToStringRemovePointZero(this decimal decInput)
        {
            string _strInput = decInput.ToString();

            string _strResult = _strInput;

            if (_strInput.Contains(".") == true)
            {
                for (int i = _strInput.Length - 1; i >= 0; i--)
                {
                    //遇到小數點就切掉整數的右側
                    if (_strInput[i] == '.')
                    {
                        _strResult = _strInput.Substring(0, i);
                        break;
                    }

                    if (_strInput[i] != '0')
                    {
                        _strResult = _strInput.Substring(0, i + 1);
                        break;
                    }
                }
            }
            return _strResult;
        }

        #endregion Function

        #region Class

        #endregion Class

        #region Enum

        public enum MathCarryMethodEnum
        {
            RoundToOnePlace, //四捨五入到小數第1位
            RoundToTwoPlace, //四捨五入到小數第2位
            RoundToThreePlace, //四捨五入到小數第3位
            RoundToFourPlace, //四捨五入到小數第4位
            Ceiling, //無條件進位
            Floor, //無條件捨去
            None //無選項
        }

        #endregion Enum
    }
}