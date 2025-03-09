using System;

namespace VACARM.Common
{
  public partial class Info
  {
    #region Parameters

    /// <summary>
    /// Safe maximum amount of virtual endpoints (virtual audio cables) for VAC.
    /// </summary>
    public readonly static uint SafeMaxVirtualEndpointCount = 10;

    /// <summary>
    /// Unsafe maximum amount of virtual endpoints (virtual audio cables) for VAC.
    /// </summary>
    public readonly static uint UnsafeMaxVirtualEndpointCount = 100;

    /// <summary>
    /// Maximum amount of endpoints (audio devices) for Windows 5.x and older.
    /// </summary>
    public readonly static uint WindowsNT5MaxEndpointCount = 32;

    public static bool AutoStartRepeaters { get; set; } = false;
    public static bool IgnoreMaxLegacyEndpointCount { get; set; } = false;
    public static bool IgnoreSafeMaxRepeaterCount { get; set; } = false;
    public static bool UseMultimediaExtensions { get; set; } = false;

    /// <summary>
    /// Limit the maximum amount of virtual endpoints (virtual audio cables).
    /// Note: Windows NT 6.x and newer does not have a maximum amount of actual 
    /// endpoints.
    /// </summary>
    public static uint MaxVirtualEndpointCount
    {
      get
      {
        if
        (
          Environment.OSVersion.Version.Major < 6
          || !IgnoreMaxLegacyEndpointCount
        )
        {
          return WindowsNT5MaxEndpointCount;
        }

        return SafeMaxVirtualEndpointCount;
      }
    }

    /// <summary>
    /// Limit the maximum amount of repeaters (pairs of audio devices).
    /// </summary>
    public static uint MaxRepeaterCount
    {
      get
      {
        if (IgnoreSafeMaxRepeaterCount)
        {
          return uint.MaxValue;
        }

        return SafeMaxRepeaterCount;
      }
    }

    /// <summary>
    /// Arbitrary maximum amount of repeaters.
    /// </summary>
    public static uint SafeMaxRepeaterCount { get; } =
      UnsafeMaxVirtualEndpointCount / 2;

    /// <summary>
    /// Arbitrary maximum amount of repeaters.
    /// </summary>
    public static uint UnsafeMaxRepeaterCount
    {
      get
      {
        return UnsafeMaxVirtualEndpointCount * 2;
      }
    }

    #endregion
  }
}
