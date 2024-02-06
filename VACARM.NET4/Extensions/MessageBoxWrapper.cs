using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace VACARM.NET4.Extensions
{
    /// <summary>
    /// Wrapper for MessageBox library
    /// </summary>
    public class MessageBoxWrapper
    {
        private readonly static string errorCaption = "Error";
        private readonly static string noticeCaption = "Notice";
        private readonly static string warningCaption = "Warning";

        /// <summary>
        /// Return True if question is answered with Yes.
        /// False if No.
        /// </summary>
        /// <param name="messageBoxText">the message box text</param>
        /// <returns>True/False</returns>
        public static bool ShowYesNoAndReturnTrueFalse
            (string messageBoxText)
        {
            return MessageBox.Show
                (messageBoxText, Common.ApplicationName, MessageBoxButton.YesNo)
                is MessageBoxResult.Yes;
        }

        /// <summary>
        /// Return True if question is answered with Yes.
        /// False if No.
        /// </summary>
        /// <param name="messageBoxText">the message box text</param>
        /// <param name="messageBoxCaption">the message box caption</param>
        /// <returns></returns>
        public static bool ShowYesNoAndReturnTrueFalse
            (string messageBoxText, string messageBoxCaption)
        {
            return MessageBox.Show
                (messageBoxText, messageBoxCaption, MessageBoxButton.YesNo)
                is MessageBoxResult.Yes;
        }

        /// <summary>
        /// Show MessageBox which user must close.
        /// Predefined caption.
        /// </summary>
        /// <param name="messageBoxText">the message box text</param>
        public static void Show(string messageBoxText)
        {
            Show(messageBoxText, Common.ApplicationName); ;
        }

        /// <summary>
        /// Show MessageBox which user must close.
        /// Predefined caption.
        /// </summary>
        /// <param name="messageBoxText">the message box text</param>
        /// <param name="messageBoxCaption">the message box caption</param>
        public static void Show(string messageBoxText, string messageBoxCaption)
        {
            MessageBox.Show(messageBoxText, messageBoxCaption);
        }

        /// <summary>
        /// Show MessageBox with predefined caption "Error".
        /// </summary>
        /// <param name="messageBoxText">the message box text</param>
        public static void ShowError(string messageBoxText)
        {
            Show(messageBoxText, errorCaption);
        }

        /// <summary>
        /// Show MessageBox with predefined caption "Notice".
        /// </summary>
        /// <param name="messageBoxText">the message box text</param>
        public static void ShowNotice(string messageBoxText)
        {
            Show(messageBoxText, noticeCaption);
        }

        /// <summary>
        /// Show MessageBox with predefined caption "Warning".
        /// </summary>
        /// <param name="messageBoxText">the message box text</param>
        public static void ShowWarning(string messageBoxText)
        {
            Show(messageBoxText, warningCaption);
        }
    }
}
