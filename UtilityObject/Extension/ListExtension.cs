using System;
using System.Collections.Generic;
using System.Linq;

namespace UtilityObject.Extension
{
    public static class ListExtension
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

        public static IList<T> Clone<T>(this IList<T> list) where T : ICloneable
        {
            return list.Select(i => (T)i.Clone()).ToList();
        }

        #endregion Function

        #region Class

        #endregion Class
    }
}