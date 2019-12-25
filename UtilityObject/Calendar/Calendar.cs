using System;
using System.Globalization;

namespace UtilityObject.Calendar
{
    public class Calendar
    {
        public DateTime TransferTaiwanDateIntoAD(DateTime dateTaiwanDate)
        {
            CultureInfo _culture = new CultureInfo("zh-TW");
            _culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return DateTime.Parse(dateTaiwanDate.ToShortDateString(), _culture);
        }
    }
}