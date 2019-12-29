using System;

namespace BaseObject.CommonObject
{
    /// <summary>
    /// 帳號Exception
    /// </summary>
    public class AccountException
    {
        public class AccountExceptionBase : Exception
        {
            public AccountExceptionBase()
            {
            }

            public AccountExceptionBase(Guid guidAccountGUId, string strAccount)
            {
                this.AccountGUId = guidAccountGUId;
                this.Account = strAccount;
            }

            public string Account { get; set; }
            public Guid AccountGUId { get; set; }
        }

        /// <summary>
        /// 帳號重複建檔
        /// </summary>
        public class AccountIsDuplicatedException : AccountExceptionBase
        {
            public AccountIsDuplicatedException() : base()
            {
            }

            private AccountIsDuplicatedException(Guid guidAccountGUId, string strAccount) : base(guidAccountGUId, strAccount)
            {
            }
        }

        /// <summary>
        /// 帳號未啟用
        /// </summary>
        public class AccountIsInactiveException : AccountExceptionBase
        {
            public AccountIsInactiveException(Guid guidAccountGUId, string strAccount) : base(guidAccountGUId, strAccount)
            { }

            public AccountIsInactiveException() : base()
            {
            }
        }

        /// <summary>
        /// 帳號不存在(沒資料) 或 關閉(Close)
        /// </summary>
        public class AccountIsNotExistenceException : AccountExceptionBase
        {
            public AccountIsNotExistenceException() : base()
            {
            }

            private AccountIsNotExistenceException(Guid guidAccountGUId, string strAccount) : base(guidAccountGUId, strAccount)
            {
            }
        }

        /// <summary>
        /// AD驗證失敗
        /// </summary>
        public class FailToAuthenticateFromADException : AccountExceptionBase
        {
            public FailToAuthenticateFromADException(Guid guidAccountGUId, string strAccount) : base(guidAccountGUId, strAccount)
            { }

            public FailToAuthenticateFromADException() : base()
            {
            }
        }

        /// <summary>
        /// 密碼錯誤
        /// </summary>
        public class PasswordIsNotCorrectException : AccountExceptionBase
        {
            public PasswordIsNotCorrectException(Guid guidAccountGUId, string strAccount) : base(guidAccountGUId, strAccount)
            { }

            public PasswordIsNotCorrectException() : base()
            {
            }
        }
    }

    /// <summary>
    /// EmailException
    /// </summary>
    public class EmailException
    {
        /// <summary>
        /// Email不存在
        /// </summary>
        public class EmailAddressIsNotExistenceException : Exception { }
    }

    public class ExcelException
    {
        public class ExcelFormatIsIncorrectException : Exception { }

        public class ExcelOpenedException : Exception { }

        public class FailToSaveExcelException : Exception { }
    }

    /// <summary>
    /// 權限Exception
    /// </summary>
    public class PermissionsException
    {
        /// <summary>
        /// 權限不足
        /// </summary>
        public class InsufficientPermissionsException : Exception { }
    }

    public class TransactionProxyException
    {
        public class TransactionProxyIsNullException : Exception
        {
        }
    }
}