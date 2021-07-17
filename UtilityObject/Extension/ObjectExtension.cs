using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UtilityObject.Extension
{
    public static class ObjectExtension
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
        /// 將 Object 轉換為屬性的KeyValuePair List, null值不轉
        /// </summary>
        /// <param name="obj">欲轉換之Object</param>
        /// <returns>屬性的KeyValuePair List</returns>
        public static List<KeyValuePair<string, string>> ObjectPropertyToKeyValuePairList(this object obj)
        {
            List<KeyValuePair<string, string>> _kvpList = new List<KeyValuePair<string, string>>();

            Type _type = obj.GetType();
            foreach (var p in _type.GetProperties())
            {
                string _strName = p.Name;
                object _strValue = p.GetValue(obj, null);
                if (_strValue != null)
                {
                    _kvpList.Add(new KeyValuePair<string, string>(_strName, _strValue.ToString()));
                }
            }

            return _kvpList;
        }

        /// <summary>
        /// Dictionary 轉為 Object 帶入屬性
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dic">IDictionary 物件</param>
        /// <returns>泛型物件</returns>
        public static T DictionaryToObjectProperty<T>(IDictionary<string, string> dic) where T : new()
        {
            var _obj = new T();
            PropertyInfo[] _piArray = _obj.GetType().GetProperties();

            foreach (PropertyInfo _pi in _piArray)
            {
                try
                {
                    if (dic.Any(x => x.Key.Equals(_pi.Name, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        KeyValuePair<string, string> _kvp = dic.First(x => x.Key.Equals(_pi.Name, StringComparison.InvariantCultureIgnoreCase));

                        Type _typeProperty = _obj.GetType().GetProperty(_pi.Name).PropertyType;

                        Type _typeObject = Nullable.GetUnderlyingType(_typeProperty) ?? _typeProperty;

                        object _objForSetValue = Convert.ChangeType(_kvp.Value, _typeObject);

                        _obj.GetType().GetProperty(_pi.Name).SetValue(_obj, _objForSetValue, null);
                    }
                }
                catch { }
            }
            return _obj;
        }

        #endregion Function

        #region Class

        #endregion Class
    }
}