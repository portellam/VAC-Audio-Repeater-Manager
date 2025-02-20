using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AudioRepeaterManager.NET4_6.GUI.Extensions
{
  public sealed class DialogCenteringService : IDisposable
  {
    #region Parameters

    private IntPtr hHook;
    private readonly IWin32Window iWin32Window;
    private readonly Win32Native.HookProc hookProc;

    #endregion

    #region Logic

    DialogCenteringService()
    {
      Dispose(false);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="iWin32Window">The Win32 window</param>
    /// <exception cref="ArgumentNullException"></exception>
    public DialogCenteringService(IWin32Window iWin32Window)
    {
      if (iWin32Window is null)
      {
        throw new ArgumentNullException(nameof(iWin32Window));
      }

      this.iWin32Window = iWin32Window;
      hookProc = GetDialogHookProcedure;

      hHook = Win32Native.SetWindowsHookEx
        (
          Win32Native.WH_CALLWNDPROCRET,
          hookProc,
          IntPtr.Zero,
          Win32Native.GetCurrentThreadId()
        );
    }

    /// <summary>
    /// Do center the window.
    /// </summary>
    /// <param name="hChildWindow">The child window hook</param>
    /// <returns>True/False</returns>
    private bool DoCenterWindow(IntPtr hChildWindow)
    {
      var recParent = GetWindowRect(iWin32Window.Handle);

      if (recParent is null)
      {
        return false;
      }

      return DoCenterWindow
        (
          hChildWindow,
          recParent.Value
        );
    }

    /// <summary>
    /// Get the dialog hook procedure from the Win32 API.
    /// </summary>
    /// <param name="nCode">The code to determine how to process the message</param>
    /// <param name="wParam">The keyboard message identifier</param>
    /// <param name="lParam">The keyboard hook struct pointer</param>
    /// <returns>The dialog hook procedure</returns>
    private IntPtr GetDialogHookProcedure
    (
      int nCode,
      IntPtr wParam,
      IntPtr lParam
    )
    {
      if (nCode < 0)
      {
        return Win32Native.CallNextHookEx
        (
          IntPtr.Zero,
          nCode,
          wParam,
          lParam
        );
      }

      var msg = (Win32Native.CWPRETSTRUCT)Marshal
        .PtrToStructure
        (
          lParam,
          typeof(Win32Native.CWPRETSTRUCT)
        );

      if (msg.message != (int)Win32Native.CbtHookAction.HCBT_ACTIVATE)
      {
        return Win32Native.CallNextHookEx
        (
          IntPtr.Zero,
          nCode,
          wParam,
          lParam
        );
      }

      try
      {
        DoCenterWindow(msg.hwnd);
      }
      finally
      {
        Win32Native.UnhookWindowsHookEx(hHook);
        hHook = IntPtr.Zero;
      }

      return Win32Native
        .CallNextHookEx
        (
          IntPtr.Zero,
          nCode,
          wParam,
          lParam
        );
    }

    /// <summary>
    /// Dispose of managed resources.
    /// </summary>
    /// <param name="doDispose">Do dispose</param>
    private void Dispose(bool doDispose)
    {
      if (doDispose)
      {
        //NOTE: Add dispose calls of managed resources here.
      }

      if (hHook != IntPtr.Zero)
      {
        Win32Native.UnhookWindowsHookEx(hHook);
        hHook = IntPtr.Zero;
      }
    }

    /// <summary>
    /// Do center the window from the Win32 API.
    /// </summary>
    /// <param name="hChildWindow">The child window hook</param>
    /// <param name="rectangleParent">The rectangle parent</param>
    /// <returns>True/False</returns>
    private static bool DoCenterWindow
    (
      IntPtr hChildWindow,
      Rectangle rectangleParent
    )
    {
      var rectangleChild = GetWindowRect(hChildWindow);

      if (rectangleChild is null)
      {
        return false;
      }

      var centeredPoint = GetCenteredPoint
        (
          rectangleParent,
          rectangleChild.Value
        );

      return Win32Native.SetWindowPos
        (
          hChildWindow,
          IntPtr.Zero,
          centeredPoint.X,
          centeredPoint.Y, -1, -1,
          Win32Native.SetWindowPosFlags.SWP_ASYNCWINDOWPOS
            | Win32Native.SetWindowPosFlags.SWP_NOSIZE
            | Win32Native.SetWindowPosFlags.SWP_NOACTIVATE
            | Win32Native.SetWindowPosFlags.SWP_NOOWNERZORDER
            | Win32Native.SetWindowPosFlags.SWP_NOZORDER
        );
    }

    /// <summary>
    /// Get the centered point.
    /// </summary>
    /// <param name="rectangleParent">The rectangle parent</param>
    /// <param name="rectangleChild">The rectangle child</param>
    /// <returns>The centered point</returns>
    private static Point GetCenteredPoint
    (
      Rectangle rectangleParent,
      Rectangle rectangleChild
    )
    {
      var ptParentCenter = new Point
      {
        X = rectangleParent.X + (rectangleParent.Width / 2),
        Y = rectangleParent.Y + (rectangleParent.Height / 2)
      };

      var ptStart = new Point
      {
        X = ptParentCenter.X - (rectangleChild.Width / 2),
        Y = ptParentCenter.Y - (rectangleChild.Height / 2)
      };

      var recCentered = new Rectangle(ptStart.X, ptStart.Y, rectangleChild.Width,
          rectangleChild.Height);                                                 // Get centered rectangle.

      var workingArea = Screen
        .FromRectangle(rectangleParent)
        .WorkingArea;                                                             // Find the working area of the parent.

      if (workingArea.X > recCentered.X)                                          // Make sure child window isn't spanning across multiple screens.
      {
        recCentered = new Rectangle
          (
            workingArea.X,
            recCentered.Y,
            recCentered.Width,
            recCentered.Height
          );
      }

      if (workingArea.Y > recCentered.Y)
      {
        recCentered = new Rectangle
          (
            recCentered.X,
            workingArea.Y,
            recCentered.Width,
            recCentered.Height
          );
      }

      if (workingArea.Right < recCentered.Right)
      {
        recCentered = new Rectangle
          (
            workingArea.Right - recCentered.Width,
            recCentered.Y,
            recCentered.Width,
            recCentered.Height
          );
      }

      if (workingArea.Bottom < recCentered.Bottom)
      {
        recCentered = new Rectangle
          (
            recCentered.X,
            workingArea.Bottom - recCentered.Height,
            recCentered.Width,
            recCentered.Height
          );
      }

      return new Point
        (
          recCentered.X,
          recCentered.Y
        );
    }

    /// <summary>
    /// Get the window rectangle from the Win32 API.
    /// </summary>
    /// <param name="hWindow">The window hook</param>
    /// <returns>The rectangle</returns>
    private static Rectangle? GetWindowRect(IntPtr hWindow)
    {
      var rect = new Win32Native.RECT();

      if
      (
        ! Win32Native
          .GetWindowRect
          (
            hWindow,
            ref rect
          )
      )
      {
        return null;
      }

      return new Rectangle
        (
          rect.left,
          rect.top,
          rect.right - rect.left,
          rect.bottom - rect.top
        );
    }

    /// <summary>
    /// Dispose of managed resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion

    #region Native/Unsafe

    // TODO: make as internal and leave outside class?
    /// <summary>
    /// Win32 API
    /// </summary>
    private static class Win32Native
    {
      #region Parameters

      private const string kernel32DllString = "kernel32.dll";
      private const string user32DllString = "user32.dll";

      public const int WH_CALLWNDPROCRET = 12;

      #endregion

      #region Structs

      public enum CbtHookAction
      {
        HCBT_MOVESIZE,
        HCBT_MINMAX,
        HCBT_QS,
        HCBT_CREATEWND,
        HCBT_DESTROYWND,
        HCBT_ACTIVATE,
        HCBT_CLICKSKIPPED,
        HCBT_KEYSKIPPED,
        HCBT_SYSCOMMAND,
        HCBT_SETFOCUS
      }

      [Flags]
      public enum SetWindowPosFlags : uint
      {
        SWP_ASYNCWINDOWPOS = 0x4000U,
        SWP_DEFERERASE = 0x2000U,
        SWP_DRAWFRAME = 0x0020U,
        SWP_FRAMECHANGED = 0x0020U,
        SWP_HIDEWINDOW = 0x0080U,
        SWP_NOACTIVATE = 0x0010U,
        SWP_NOCOPYBITS = 0x0100U,
        SWP_NOMOVE = 0x0002U,
        SWP_NOOWNERZORDER = 0x0200U,
        SWP_NOREDRAW = 0x0008U,
        SWP_NOREPOSITION = 0x0200U,
        SWP_NOSENDCHANGING = 0x0400U,
        SWP_NOSIZE = 0x0001U,
        SWP_NOZORDER = 0x0004U,
        SWP_SHOWWINDOW = 0x0040U
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct CWPRETSTRUCT
      {
        public IntPtr lResult;
        public IntPtr lParam;
        public IntPtr wParam;
        public uint message;
        public IntPtr hwnd;
      };

      [StructLayout(LayoutKind.Sequential)]
      public struct RECT
      {
        public int left;
        public int top;
        public int right;
        public int bottom;
      }

      #endregion

      #region Delegates

      public delegate IntPtr HookProc
      (
        int nCode,
        IntPtr wParam,
        IntPtr lParam
      );

      [DllImport(user32DllString)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool GetWindowRect
      (
        IntPtr hWnd,
        ref RECT lpRect
      );

      [DllImport(user32DllString)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetWindowPos
      (
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int x,
        int y,
        int cx,
        int cy,
        SetWindowPosFlags uFlags
      );

      [DllImport(kernel32DllString)]
      public static extern int GetCurrentThreadId();

      [DllImport(user32DllString)]
      public static extern int UnhookWindowsHookEx(IntPtr idHook);

      [DllImport(user32DllString)]
      public static extern IntPtr SetWindowsHookEx
      (
        int idHook,
        HookProc lpfn,
        IntPtr hInstance,
        int threadId
      );

      [DllImport(user32DllString)]
      public static extern IntPtr CallNextHookEx
      (
        IntPtr idHook,
        int nCode,
        IntPtr wParam,
        IntPtr lParam
      );

      #endregion
    }

    #endregion
  }
}