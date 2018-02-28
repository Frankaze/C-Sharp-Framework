using System.Data;
using System.Data.SqlClient;

namespace BaseObject.CommonObject
{
    public class TransactionProxy
    {
        #region Constant

        #endregion Constant

        #region Variable

        public SqlConnection m_sqlConn;
        public SqlTransaction m_sqlTran;
        public string m_strDatabase;
        private bool m_bolIsBeginTranscation = false;
        private bool m_IsSubTransaction;
        private bool m_IsTransactionStart = false;
        private string m_strConnectionString;

        private TransactionProxy m_tranParentProxy;

        #endregion Variable

        #region Property

        public string ConnectionString
        {
            get
            {
                if (this.m_IsSubTransaction == false)
                {
                    return this.m_strConnectionString;
                }
                else
                {
                    return this.m_tranParentProxy.ConnectionString;
                }
            }
        }

        public string DataBase
        {
            get
            {
                if (this.m_IsSubTransaction == false)
                {
                    return this.m_strDatabase;
                }
                else
                {
                    return this.m_tranParentProxy.DataBase;
                }
            }
            set
            {
                if (this.m_IsSubTransaction == false)
                {
                    this.m_strDatabase = value;
                }
                else
                {
                    this.m_tranParentProxy.DataBase = value;
                }
            }
        }

        public bool IsBeginTranscation
        {
            get
            {
                if (this.m_IsSubTransaction == false)
                {
                    return this.m_bolIsBeginTranscation;
                }
                else
                {
                    return this.m_tranParentProxy.IsBeginTranscation;
                }
            }
        }

        public SqlConnection SqlConnection
        {
            get
            {
                if (this.m_IsSubTransaction == false)
                {
                    return this.m_sqlConn;
                }
                else
                {
                    return this.m_tranParentProxy.SqlConnection;
                }
            }
            set
            {
                if (this.m_IsSubTransaction == false)
                {
                    this.m_sqlConn = value;
                }
                else
                {
                    this.m_tranParentProxy.SqlConnection = value;
                }
            }
        }

        public SqlTransaction SqlTransaction
        {
            get
            {
                if (this.m_IsSubTransaction == false)
                {
                    return this.m_sqlTran;
                }
                else
                {
                    return this.m_tranParentProxy.SqlTransaction;
                }
            }
            set
            {
                if (this.m_IsSubTransaction == false)
                {
                    this.m_sqlTran = value;
                }
                else
                {
                    this.m_tranParentProxy.SqlTransaction = value;
                }
            }
        }

        #endregion Property

        #region Constructor

        public TransactionProxy(string strConnectionString)
        {
            this.m_strConnectionString = strConnectionString;
            this.m_sqlConn = new SqlConnection(strConnectionString);
            this.m_strDatabase = this.m_sqlConn.Database;
            this.m_bolIsBeginTranscation = false;
        }

        public TransactionProxy(ref TransactionProxy tranParentProxy)
        {
            this.m_tranParentProxy = tranParentProxy;
            this.m_IsSubTransaction = true;
        }

        private TransactionProxy()
        {
        }

        //public TransactionProxy(ref SqlConnection sqlConn, ref SqlTransaction sqlTran, bool bolIsBeginTranscation, bool bolIsSubTransaction)
        //{
        //    this.m_sqlConn = sqlConn;
        //    this.m_strDatabase = this.m_sqlConn.Database;

        //    this.m_sqlTran = sqlTran;
        //    this.m_bolIsBeginTranscation = bolIsBeginTranscation;
        //    this.m_IsSubTransaction = bolIsSubTransaction;
        //}

        #endregion Constructor

        #region Function

        public void BeginTransaction()
        {
            if (this.m_IsSubTransaction == false)
            {
                this.m_bolIsBeginTranscation = true;
            }
            else
            {
                if (this.m_tranParentProxy.IsBeginTranscation == false)
                {
                    this.m_bolIsBeginTranscation = true;
                }

                this.m_tranParentProxy.BeginTransaction();
            }
        }

        public void CloseConnection()
        {
            if (this.m_IsSubTransaction == false && this.m_bolIsBeginTranscation == false)
            {
                this.m_sqlConn.Close();
            }

            if (this.m_IsSubTransaction == true && this.m_tranParentProxy.IsBeginTranscation == false)
            {
                this.m_tranParentProxy.SqlConnection.Close();
            }
        }

        public void CommitTransaction()
        {
            if (this.SqlConnection.State == ConnectionState.Closed) { return; }

            if (this.m_IsSubTransaction == false && this.m_bolIsBeginTranscation == true)
            {
                this.m_sqlTran.Commit();
                this.m_sqlConn.Close();
                this.m_bolIsBeginTranscation = false;
            }

            if (this.m_IsSubTransaction == true && this.m_tranParentProxy.IsBeginTranscation == true)
            {
                if (this.m_IsTransactionStart == true)
                {
                    this.m_tranParentProxy.CommitTransaction();
                    this.m_IsTransactionStart = false;
                }
            }
        }

        public TransactionProxy CreateSubTranscationProxy()
        {
            TransactionProxy _tranParentProxy = this;
            return new TransactionProxy(ref _tranParentProxy);
        }

        public void OpenConnection(ref SqlCommand sqlCmd)
        {
            if (this.m_IsSubTransaction == false)
            {
                if (this.SqlConnection.State == ConnectionState.Closed)
                {
                    this.m_sqlConn.Open();

                    if (this.m_bolIsBeginTranscation == true)
                    {
                        this.m_sqlTran = this.m_sqlConn.BeginTransaction();
                        sqlCmd.Transaction = this.m_sqlTran;
                    }
                }
                else //已連線狀態
                {
                    if (this.m_bolIsBeginTranscation == true)
                    {
                        sqlCmd.Transaction = this.m_sqlTran;
                    }
                }
            }
            else
            {
                if (this.m_tranParentProxy.SqlConnection.State == ConnectionState.Closed)
                {
                    this.m_tranParentProxy.SqlConnection.Open();

                    if (this.m_tranParentProxy.IsBeginTranscation == true)
                    {
                        this.m_tranParentProxy.SqlTransaction = this.m_tranParentProxy.SqlConnection.BeginTransaction();
                        sqlCmd.Transaction = this.m_tranParentProxy.SqlTransaction;
                    }
                }
                else
                {
                    if (this.m_tranParentProxy.IsBeginTranscation == true)
                    {
                        sqlCmd.Transaction = this.m_tranParentProxy.SqlTransaction;
                    }
                }
            }
        }

        public void RollbackTransaction()
        {
            if (this.m_bolIsBeginTranscation == true && this.m_IsSubTransaction == false)
            {
                this.m_sqlTran.Rollback();
                this.m_sqlConn.Close();
                this.m_bolIsBeginTranscation = false;
            }
        }

        #endregion Function

        #region Class

        #endregion Class
    }
}