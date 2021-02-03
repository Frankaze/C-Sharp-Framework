using System;

namespace UtilityObject.Extension
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 取得台灣標準時間
        /// </summary>
        /// <param name="date">時間</param>
        /// <returns>台灣標準時間</returns>
        public static DateTime TaiwanStandardTime(this DateTime date)
        {
            return date.ToUniversalTime().AddHours(8);
        }
    }
}