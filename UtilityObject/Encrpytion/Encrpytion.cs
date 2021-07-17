using System;
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

        #region ASE256

        /// <summary>
        /// 使用ASE256加密
        /// </summary>
        /// <param name="enumCase">大小寫</param>
        /// <param name="enumCipherMode">區塊加密模式，常用為 CBC</param>
        /// <param name="enumPaddingMode">指定填補類型，常用為 PKCS7</param>
        /// <param name="strPlaintext">明文</param>
        /// <param name="strKey">Key</param>
        /// <param name="strIV">IV</param>
        /// <returns>密文</returns>
        public static string EncryptAES256(CaseEnum enumCase, CipherMode enumCipherMode, PaddingMode enumPaddingMode, string strPlaintext, string strKey, string strIV)
        {
            StringBuilder _sb = new StringBuilder();

            if (!string.IsNullOrEmpty(strPlaintext))
            {
                byte[] _bytePlaintext = Encoding.UTF8.GetBytes(strPlaintext);
                byte[] _byteKey = Encoding.UTF8.GetBytes(strKey);
                byte[] _byteIV = Encoding.UTF8.GetBytes(strIV);

                byte[] _byteEncryptValue;
                using (var _aes = Aes.Create())
                {
                    _aes.Mode = enumCipherMode;
                    _aes.Padding = enumPaddingMode;
                    _aes.Key = _byteKey;
                    _aes.IV = _byteIV;

                    using (var _encryptor = _aes.CreateEncryptor())
                    {
                        _byteEncryptValue = _encryptor.TransformFinalBlock(_bytePlaintext, 0, _bytePlaintext.Length);
                    }
                }

                if (_byteEncryptValue != null)
                {
                    switch (enumCase)
                    {
                        case CaseEnum.Lowercase:
                            _sb.Append(BitConverter.ToString(_byteEncryptValue)?.Replace("-", string.Empty)?.ToLower());
                            break;

                        case CaseEnum.Uppercase:
                            _sb.Append(BitConverter.ToString(_byteEncryptValue)?.Replace("-", string.Empty)?.ToUpper());
                            break;
                    }
                }
            }
            return _sb.ToString();
        }

        /// <summary>
        /// 使用ASE256解密
        /// </summary>
        /// <param name="enumCase">大小寫</param>
        /// <param name="enumCipherMode">區塊加密模式，常用為 CBC</param>
        /// <param name="enumPaddingMode">指定填補類型，常用為 PKCS7</param>
        /// <param name="strCiphertext">密文</param>
        /// <param name="strKey">Key</param>
        /// <param name="strIV">IV</param>
        /// <returns>名文</returns>
        public static string DecryptAES256(CaseEnum enumCase, CipherMode enumCipherMode, PaddingMode enumPaddingMode, string strCiphertext, string strKey, string strIV)
        {
            StringBuilder _sb = new StringBuilder();

            if (!string.IsNullOrEmpty(strCiphertext))
            {
                byte[] _byteCiphertext = Encoding.UTF8.GetBytes(strCiphertext);
                byte[] _byteKey = Encoding.UTF8.GetBytes(strKey);
                byte[] _byteIV = Encoding.UTF8.GetBytes(strIV);

                byte[] _byteDecryptValue;
                using (var _aes = Aes.Create())
                {
                    _aes.Mode = enumCipherMode;
                    _aes.Padding = enumPaddingMode;
                    _aes.Key = _byteKey;
                    _aes.IV = _byteIV;

                    using (var _decryptor = _aes.CreateDecryptor())
                    {
                        _byteDecryptValue = _decryptor.TransformFinalBlock(_byteCiphertext, 0, _byteCiphertext.Length);
                    }
                }

                if (_byteDecryptValue != null)
                {
                    switch (enumCase)
                    {
                        case CaseEnum.Lowercase:
                            _sb.Append(BitConverter.ToString(_byteDecryptValue)?.Replace("-", string.Empty)?.ToLower());
                            break;

                        case CaseEnum.Uppercase:
                            _sb.Append(BitConverter.ToString(_byteDecryptValue)?.Replace("-", string.Empty)?.ToUpper());
                            break;
                    }
                }
            }
            return _sb.ToString();
        }

        #endregion ASE256

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