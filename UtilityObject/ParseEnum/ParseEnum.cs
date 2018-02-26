using System;

namespace UtilityObject.ParseEnum
{
    public static class ParseEnum
    {
        /// <summary>
        /// 將字串轉換成Enum
        /// </summary>
        /// <typeparam name="TEnum">Enum類別</typeparam>
        /// <param name="str">字串</param>
        /// <returns>Enum的值</returns>
        public static TEnum ParseStringToEnum<TEnum>(string str)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), str, true);
        }

        /// <summary>
        /// 將字串轉換成Enum，如果傳入空白則回傳Null
        /// </summary>
        /// <typeparam name="TEnum">Enum類別</typeparam>
        /// <param name="str">字串</param>
        /// <returns>Enum的值 或 Null</returns>
        public static TEnum? ParseStringToEnumIncludeNull<TEnum>(string str) where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(str) == true)
            {
                return (TEnum?)null;
            }
            else
            {
                return (TEnum)Enum.Parse(typeof(TEnum), str, true);
            }
        }
    }
}