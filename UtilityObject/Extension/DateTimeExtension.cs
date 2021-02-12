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

        /// <summary>
        /// 取得當週第一天 (星期一開始)
        /// </summary>
        /// <param name="date">時間</param>
        /// <returns>當週第一天</returns>
        public static DateTime FirstDayOfWeekFromMonday(this DateTime date)
        {
            int _intDayOfWeek = Convert.ToInt32(date.DayOfWeek);

            //如果遇到星期天 (7-1)
            int _intDayDiff = (-1) * (_intDayOfWeek == 0 ? (7 - 1) : (_intDayOfWeek - 1));

            return date.AddDays(_intDayDiff).Date;
        }

        /// <summary>
        ///  取得當週最後一天 (星期日結束)
        /// </summary>
        /// <param name="date">時間</param>
        /// <returns>當週最後一天</returns>
        public static DateTime LastDayOfWeekToSunday(this DateTime date)
        {
            int _intDayOfWeek = Convert.ToInt32(date.DayOfWeek);

            int _intDayDiff = 7 - (_intDayOfWeek == 0 ? 7 : _intDayOfWeek);

            return date.AddDays(_intDayDiff).Date;
        }
    }
}