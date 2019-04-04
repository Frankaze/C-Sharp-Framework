using System;
using System.Windows.Forms;

namespace WinFormObject.UserControl
{
    public partial class UserControlBase : System.Windows.Forms.UserControl
    {
        #region Constant

        #endregion Constant

        #region Variable

        protected bool m_bolInitialization = true;

        #endregion Variable

        #region Property

        #endregion Property

        #region Constructor

        public UserControlBase()
        {
            this.InitializeComponent();
        }

        #endregion Constructor

        #region Function

        public virtual void SetInitialization(bool bolInitialization)
        {
            this.m_bolInitialization = bolInitialization;
        }

        /// <summary>
        /// 限制TextBox只能輸入數字與英文字，使用事件：KeyPress
        /// </summary>
        /// <param name="sender">KeyPress事件的sender</param>
        /// <param name="e">KeyPress事件</param>
        protected void PressNumericAndLetterOnTextBox(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)48 || e.KeyChar == (Char)49 ||
                e.KeyChar == (Char)50 || e.KeyChar == (Char)51 ||
                e.KeyChar == (Char)52 || e.KeyChar == (Char)53 ||
                e.KeyChar == (Char)54 || e.KeyChar == (Char)55 ||
                e.KeyChar == (Char)56 || e.KeyChar == (Char)57 ||
                e.KeyChar == (Char)13 || e.KeyChar == (Char)8 || e.KeyChar == (Char)32 ||
                (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 限制TextBox只能輸入數字，使用事件：KeyPress
        /// </summary>
        /// <param name="sender">KeyPress事件的sender</param>
        /// <param name="e">KeyPress事件</param>
        protected void PressNumericOnlyOnTextBox(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)48 || e.KeyChar == (Char)49 ||
                e.KeyChar == (Char)50 || e.KeyChar == (Char)51 ||
                e.KeyChar == (Char)52 || e.KeyChar == (Char)53 ||
                e.KeyChar == (Char)54 || e.KeyChar == (Char)55 ||
                e.KeyChar == (Char)56 || e.KeyChar == (Char)57 ||
                e.KeyChar == (Char)13 || e.KeyChar == (Char)8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 當TextBox的內容達到最大字元時，自動送出Tab按鈕，使用事件：TextChanged
        /// </summary>
        /// <param name="txt">TextBox</param>
        protected void SendTabForMaxLengthOnTextBox(TextBox txt)
        {
            if (txt.Text.Length == txt.MaxLength)
            {
                SendKeys.Send("{TAB}");
            }
        }

        #endregion Function

        #region Class

        #endregion Class
    }
}