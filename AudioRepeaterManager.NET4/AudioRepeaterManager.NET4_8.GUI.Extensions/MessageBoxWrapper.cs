using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace AudioRepeaterManager.NET4_8.GUI.Extensions
{
  /// <summary>
  /// Wrapper for MessageBox library
  /// </summary>
  public class MessageBoxWrapper
  {
    #region Parameters

    private readonly static string errorCaption = "Error";
    private readonly static string noticeCaption = "Notice";
    private readonly static string warningCaption = "Warning";
    public static string AssemblyTitle { get; set; }

    public MessageBoxWrapper(string assemblyTitle)
    {
      AssemblyTitle = assemblyTitle;
    }

    #endregion

    #region Logic

    /// <summary>
    /// Show MessageBox with predefined caption which user must close with "Yes" or
    /// "No". Return True if question is answered with Yes. False if No.
    /// </summary>
    /// <param name="messageBoxText">The message box text</param>
    /// <returns>True/False</returns>
    public static bool ShowYesNoAndReturnTrueFalse(string messageBoxText)
    {
      return ShowYesNoAndReturnTrueFalse
      (
        messageBoxText,
        AssemblyTitle
      );
    }



    /// <summary>
    /// Show centered MessageBox with predefined caption which user must close with
    /// "Yes" or "No". Return True if question is answered with "Yes".
    /// False if "No".
    /// </summary>
    /// <param name="parentObject">The parent object</param>
    /// <param name="messageBoxText">The message box text</param>
    /// <returns>True/False</returns>
    public static bool ShowYesNoAndReturnTrueFalse
    (
      object parentObject,
      string messageBoxText
    )
    {
      return ShowYesNoAndReturnTrueFalse
        (
          parentObject,
          messageBoxText,
          AssemblyTitle
        );
    }

    /// <summary>
    /// Show MessageBox with user-defined caption which user must close with "Yes"
    /// or "No". Return True if question is answered with "Yes". False if "No".
    /// </summary>
    /// <param name="messageBoxText">The message box text</param>
    /// <param name="messageBoxCaption">The message box caption</param>
    /// <returns>True/False</returns>
    public static bool ShowYesNoAndReturnTrueFalse
    (
      string messageBoxText,
      string messageBoxCaption
    )
    {
      return MessageBox
        .Show
        (
          messageBoxText,
          messageBoxCaption,
          MessageBoxButton.YesNo
        ) is MessageBoxResult.Yes;
    }

    /// <summary>
    /// Show centered MessageBox with user-defined caption which user must close
    /// with "Yes" or "No". Return True if question is answered with "Yes".
    /// False if "No".
    /// </summary>
    /// <param name="parentObject">The parent object</param>
    /// <param name="messageBoxText">The message box text</param>
    /// <param name="messageBoxCaption">The message box caption</param>
    /// <returns>True/False</returns>
    public static bool ShowYesNoAndReturnTrueFalse
    (
      object parentObject,
      string messageBoxText,
      string messageBoxCaption
    )
    {
      if (parentObject is null)
      {
        throw new System.ArgumentNullException(nameof(parentObject));
      }

      if (! (parentObject is IWin32Window))
      {
        throw new System.NotSupportedException(nameof(parentObject));
      }

      using (new DialogCenteringService(parentObject as IWin32Window))
      {
        return MessageBox
          .Show
          (
            messageBoxText,
            messageBoxCaption,
            MessageBoxButton.YesNo
          ) is MessageBoxResult.Yes;
      }
    }

    /// <summary>
    /// Show MessageBox with predefined caption which user must close.
    /// </summary>
    /// <param name="messageBoxText">The message box text</param>
    public static void Show(string messageBoxText)
    {
      Show(messageBoxText, AssemblyTitle);
    }

    /// <summary>
    /// Show centered MessageBox with predefined caption which user must close.
    /// </summary>
    /// <param name="parentObject">The parent object</param>
    /// <param name="messageBoxText">The message box text</param>
    public static void Show
    (
      object parentObject,
      string messageBoxText
    )
    {
      if (parentObject is null)
      {
        throw new System.ArgumentNullException(nameof(parentObject));
      }

      if (! (parentObject is IWin32Window))
      {
        throw new System.NotSupportedException(nameof(parentObject));
      }

      using (new DialogCenteringService(parentObject as IWin32Window))
      {
        Show
          (
            messageBoxText,
            AssemblyTitle
          );
      }
    }

    /// <summary>
    /// Show MessageBox with user-defined caption which user must close.
    /// </summary>
    /// <param name="messageBoxText">The message box text</param>
    /// <param name="messageBoxCaption">The message box caption</param>
    public static void Show
    (
      string messageBoxText,
      string messageBoxCaption
    )
    {
      MessageBox.Show
        (
          messageBoxText,
          messageBoxCaption
        );
    }

    /// <summary>
    /// Show centered MessageBox with user-defined caption which user must close.
    /// </summary>
    /// <param name="parentObject">The parent object</param>
    /// <param name="messageBoxText">The message box text</param>
    /// <param name="messageBoxCaption">The message box caption</param>
    public static void Show
    (
      object parentObject,
      string messageBoxText,
      string messageBoxCaption
    )
    {
      if (parentObject is null)
      {
        throw new System.ArgumentNullException(nameof(parentObject));
      }

      if (! (parentObject is IWin32Window))
      {
        throw new System.NotSupportedException(nameof(parentObject));
      }

      using (new DialogCenteringService(parentObject as IWin32Window))
      {
        Show
        (
          messageBoxText,
          messageBoxCaption
        );
      }
    }

    /// <summary>
    /// Show MessageBox with predefined caption "Error" which user must
    /// close.
    /// </summary>
    /// <param name="messageBoxText">The message box text</param>
    public static void ShowError(string messageBoxText)
    {
      Show
        (
          messageBoxText,
          errorCaption
        );
    }

    /// <summary>
    /// Show centered MessageBox with predefined caption "Error" which user must
    /// close.
    /// </summary>
    /// <param name="parentObject">The parent object</param>
    /// <param name="messageBoxText">The message box text</param>
    public static void ShowError
    (
      object parentObject,
      string messageBoxText
    )
    {
      Show
        (
          parentObject,
          messageBoxText,
          errorCaption
        );
    }

    /// <summary>
    /// Show MessageBox with predefined caption "Notice" which user must
    /// close.
    /// </summary>
    /// <param name="messageBoxText">The message box text</param>
    public static void ShowNotice(string messageBoxText)
    {
      Show
        (
          messageBoxText,
          noticeCaption
        );
    }

    /// <summary>
    /// Show centered MessageBox with predefined caption "Notice" which user must
    /// close.
    /// </summary>
    /// <param name="parentObject">The parent object</param>
    /// <param name="messageBoxText">The message box text</param>
    public static void ShowNotice
    (
      object parentObject,
      string messageBoxText
    )
    {
      Show
        (
          parentObject,
          messageBoxText,
          noticeCaption
        );
    }

    /// <summary>
    /// Show MessageBox with predefined caption "Warning" which user must
    /// close.
    /// </summary>
    /// <param name="messageBoxText">The message box text</param>
    public static void ShowWarning(string messageBoxText)
    {
      Show
        (
          messageBoxText,
          warningCaption
        );
    }

    /// <summary>
    /// Show centered MessageBox with predefined caption "Warning" which user must
    /// close.
    /// </summary>
    /// <param name="parentObject">The parent object</param>
    /// <param name="messageBoxText">The message box text</param>
    public static void ShowWarning
    (
      object parentObject,
      string messageBoxText
    )
    {
      Show(parentObject, messageBoxText, warningCaption);
    }

    #endregion
  }
}