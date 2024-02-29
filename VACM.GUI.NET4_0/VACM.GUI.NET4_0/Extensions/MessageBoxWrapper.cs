﻿using System.Windows;
using VACM.GUI.NET4_0.ViewModels.Accessors;
using MessageBox = System.Windows.MessageBox;

namespace VACM.NET4.Extensions
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
        /// Show MessageBox which user must close.
        /// Predefined caption.
        /// </summary>
        /// <param name="messageBoxText">The message box text</param>
        public static void Show(string messageBoxText)
        {
            Show(messageBoxText, AssemblyInformationAccessor.AssemblyTitle);
        }

        /// <summary>
        /// Show MessageBox which user must close.
        /// Predefined caption.
        /// </summary>
        /// <param name="messageBoxText">The message box text</param>
        /// <param name="messageBoxCaption">The message box caption</param>
        public static void Show(string messageBoxText, string messageBoxCaption)
        {
            MessageBox.Show(messageBoxText, messageBoxCaption);
        }

        /// <summary>
        /// Show MessageBox with predefined caption "Error".
        /// </summary>
        /// <param name="messageBoxText">The message box text</param>
        public static void ShowError(string messageBoxText)
        {
            Show(messageBoxText, errorCaption);
        }

        /// <summary>
        /// Show MessageBox with predefined caption "Notice".
        /// </summary>
        /// <param name="messageBoxText">The message box text</param>
        public static void ShowNotice(string messageBoxText)
        {
            Show(messageBoxText, noticeCaption);
        }

        /// <summary>
        /// Show MessageBox with predefined caption "Warning".
        /// </summary>
        /// <param name="messageBoxText">The message box text</param>
        public static void ShowWarning(string messageBoxText)
        {
            Show(messageBoxText, warningCaption);
        }
    }
}