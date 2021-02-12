using System.ComponentModel;
using System.Reflection;

namespace UtilityObject.Extension
{
    public static class EnumExtension
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
        /// 取得ComponentModel的描述
        /// </summary>
        public static string GetDescription<T>(this T source)
        {
            FieldInfo _fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] _attribute = (DescriptionAttribute[])_fi.GetCustomAttributes(
             typeof(DescriptionAttribute), false);

            if (_attribute.Length > 0)
            {
                return _attribute[0].Description;
            }
            else
            {
                return source.ToString();
            }
        }

        #endregion Function

        #region Class

        #endregion Class
    }
}