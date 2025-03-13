#warning Differs from projects of later NET revisions (Framework 4.5 and above).

using System;
using System.Runtime.InteropServices;

namespace VACARM.Common
{
  public partial class Info
  {
    #region Parameters

    public static bool IsOSWindows
    {
      get
      {
        var osPlatformId = Environment.OSVersion
          .Platform;

        return osPlatformId == PlatformID.WinCE
          || osPlatformId == PlatformID.Win32NT
          || osPlatformId == PlatformID.Win32S
          || osPlatformId == PlatformID.Win32Windows;
      }
    }

    public static string OSDescription
    {
      get
      {
        return Environment.OSVersion
          .VersionString;
      }
    }

    #endregion
  }
}