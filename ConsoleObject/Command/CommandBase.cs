using System;
//using UtilityObject.Email;

namespace ConsoleObject.Command
{
    public abstract class CommandBase
    {
        #region Constant

        private const int COMMAND_TYPE_TITLE_LENGTH = 18; //指令標題切齊長度
        private const string SPLIT_MARK = "  "; //訊息切割字元

        #endregion Constant

        #region Variable

        #endregion Variable

        #region Property

        #endregion Property

        #region Constructor

        #endregion Constructor

        #region Function

        #region Error Email

        //protected virtual void SendNotifcationMailWhenCommandFail(string strApplicatonCode, List<string> strEmailAddressList, CommandBase cmd, Exception ex)
        //{
        //    NotificationEmail _email = new NotificationEmail(strApplicatonCode);

        //    _email.SendMail(strEmailAddressList, strApplicatonCode + "排程失敗 - " + cmd.GetType().Name,
        //        "執行失敗時間：" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "<br />" +
        //        "執行錯誤原因：" + ex.Message.ToString());
        //}

        //protected abstract void SendNotifcationMailWhenCommandFail(CommandBase cmd, Exception ex);

        #endregion Error Email

        #region Command Message

        /// <summary>
        /// 寫出「指令完成」訊息
        /// </summary>
        /// <param name="cmd">指令基礎物件</param>
        protected virtual void WriteCommandCompleteMessage(CommandBase cmd)
        {
            this.WriteCommandMessage("[Command Complete]", cmd.GetType().Name);
            Console.WriteLine();
        }

        /// <summary>
        /// 寫出「指令失敗」訊息
        /// </summary>
        /// <param name="cmd">指令基礎物件</param>
        protected virtual void WriteCommandFailMessage(CommandBase cmd)
        {
            this.WriteCommandMessage("[Command Fail]", cmd.GetType().Name);
            Console.WriteLine();
        }

        /// <summary>
        /// 寫出「指令進行」訊息
        /// </summary>
        /// <param name="strCommandMessage">指令基礎物件</param>
        protected virtual void WriteCommandProcessMessage(string strCommandMessage)
        {
            this.WriteCommandMessage("[Command Process]", strCommandMessage);
        }

        /// <summary>
        /// 寫出「指令開始」訊息
        /// </summary>
        /// <param name="cmd">指令基礎物件</param>
        protected virtual void WriteCommandStartMessage(CommandBase cmd)
        {
            this.WriteCommandMessage("[Command Start] ", cmd.GetType().Name);
        }

        /// <summary>
        /// 命令提示字元寫出指令的訊息
        /// </summary>
        /// <param name="strCommandTypeTitle">指令類型的標題</param>
        /// <param name="strCommandMessage">指令訊息</param>
        private void WriteCommandMessage(string strCommandTypeTitle, string strCommandMessage)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + SPLIT_MARK + strCommandTypeTitle.PadRight(COMMAND_TYPE_TITLE_LENGTH) + SPLIT_MARK + strCommandMessage);
        }

        #endregion Command Message

        #endregion Function
    }
}