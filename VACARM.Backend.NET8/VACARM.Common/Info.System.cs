#warning Differs from projects of earlier NET revisions (below Framework 4.5).

using System;
using System.Runtime.InteropServices;

namespace VACARM.Common
{
  public partial class Info
  {
    #region Parameters

    public readonly static bool DoesProcessAndSystemBitMatch =
      Environment.Is64BitProcess == Environment.Is64BitOperatingSystem;

    public static bool IsOSWindows
    {
      get
      {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
      }
    }

    public static string OSDescription
    {
      get
      {
        return RuntimeInformation.OSDescription;
      }
    }

    #endregion
  }
}