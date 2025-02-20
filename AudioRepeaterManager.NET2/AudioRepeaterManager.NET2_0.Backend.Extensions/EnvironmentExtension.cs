using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AudioRepeaterManager.NET2_0.Backend.Extensions
{
  public class EnvironmentExtension
  {
    #region Parameters

    /// <summary>
    /// Is application currently running on the Windows 32-bit on Windows 64-bit
    /// (WoW64) subsystem.
    /// </summary>
    /// <returns>True/false if application is running in WoW64.</returns>
    private static bool IsWoW64Application()
    {
      if
      (
        (
          Environment.OSVersion.Version.Major == 5
          && Environment.OSVersion.Version.Minor >= 1
        ) || Environment.OSVersion.Version.Major >= 6)
      {
        using (Process p = Process.GetCurrentProcess())
        {
          bool returnValue;

          if (
            !IsWoW64Process
            (
              p.Handle,
              out returnValue
            )
          )
          {
            return false;
          }

          return returnValue;
        }
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Is process currently runnning on the Windows 32-bit on Windows 64-bit
    /// (WoW64) subsystem.
    /// </summary>
    /// <param name="hProcess">The process handle</param>
    /// <param name="wow64Process">True/false is a WoW64 process</param>
    /// <returns></returns>
    [DllImport
      (
        "kernel32.dll",
        SetLastError = true,
        CallingConvention = CallingConvention.Winapi
      )
    ]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWoW64Process
    (
      [In] IntPtr hProcess,
      [Out] out bool wow64Process
    );

    /// <summary>
    /// True/false is 64-bit operating system.
    /// </summary>
    public static bool Is64BitOperatingSystem
    {
      get
      {
        return Is64BitProcess
          || IsWoW64Application();
      }
    }

    /// <summary>
    /// True/false is 64-bit process.
    /// </summary>
    public static bool Is64BitProcess
    {
      get
      {
        return IntPtr.Size == 8;
      }
    }

    #endregion
  }
}
