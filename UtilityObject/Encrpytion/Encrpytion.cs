using System.Security.Cryptography;
using System.Text;

namespace UtilityObject.Encrpytion
{
    public static class Encrpytion
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
        /// 使用MD5加密
        /// </summary>
        /// <param name="enumCase">大小寫</param>
        /// <param name="strPlaintext">明文</param>
        /// <returns>密文</returns>
        public static string EncryptByMD5(CaseEnum enumCase, string strPlaintext)
        {
            MD5 _md5 = new MD5CryptoServiceProvider();

            byte[] _byteBufferArray = Encoding.Default.GetBytes(strPlaintext);
            byte[] _byteMD5Array = new MD5CryptoServiceProvider().ComputeHash(_byteBufferArray);

            StringBuilder _sb = new StringBuilder();
            for (int i = 0; i < _byteMD5Array.Length; i++)
            {
                switch (enumCase)
                {
                    case CaseEnum.Lowercase:
                        _sb.Append(_byteMD5Array[i].ToString("x2"));
                        break;

                    case CaseEnum.Uppercase:
                        _sb.Append(_byteMD5Array[i].ToString("X2"));
                        break;
                }
            }
            return _sb.ToString();
        }

        /// <summary>
        /// 使用SHA256加密
        /// </summary>
        /// <param name="enumCase">大小寫</param>
        /// <param name="strPlaintext">明文</param>
        /// <returns>密文</returns>
        public static string EncryptSHA256(CaseEnum enumCase, string strPlaintext)
        {
            SHA256 _sha256 = new SHA256CryptoServiceProvider();

            byte[] _byteBufferArray = Encoding.UTF8.GetBytes(strPlaintext);
            byte[] _byteSHAArray = SHA256Managed.Create().ComputeHash(_byteBufferArray);

            StringBuilder _sb = new StringBuilder();

            for (int i = 0; i < _byteSHAArray.Length; i++)
            {
                switch (enumCase)
                {
                    case CaseEnum.Lowercase:
                        _sb.Append(_byteSHAArray[i].ToString("x2"));
                        break;

                    case CaseEnum.Uppercase:
                        _sb.Append(_byteSHAArray[i].ToString("X2"));
                        break;
                }
            }

            return _sb.ToString();
        }

        #endregion Function

        #region Class

        #endregion Class

        #region Enum

        public enum CaseEnum
        {
            Uppercase,
            Lowercase,
        }

        #endregion Enum
    }
}