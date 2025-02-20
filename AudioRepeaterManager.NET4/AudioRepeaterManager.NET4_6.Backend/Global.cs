using System;
using System.IO;

namespace AudioRepeaterManager.NET4_6.Backend
{
  /// <summary>
  /// Global parameters
  /// </summary>
  public class Global
  {
    #region Parameters

    #region Application name

    public readonly static string ReferencedApplicationName = "Virtual Audio Cable";
    public readonly static string ReferencedFileExtension = ".vac";

    #endregion

    #region Executable path names

    /// <summary>
    /// The name of the executable.
    /// </summary>
    private readonly static string executableName = "audiorepeater.exe";

    /// <summary>
    /// Typically "C:\Program Files\Virtual Audio Cable\audiorepeater.exe".
    /// </summary>
    private static string executablePathNameForBitMatchedProcessAndSystem =
      $"{systemRootPathName}Program Files\\{firstParentPathNameForExecutable}";

    /// <summary>
    /// Typically "C:\Program Files (x86)\Virtual Audio Cable\audiorepeater.exe".
    /// </summary>
    private static string executablePathNameForBitUnmatchedProcessAndSystem =
      $"{systemRootPathName}Program Files (x86)\\{firstParentPathNameForExecutable}";

    /// <summary>
    /// Typically "Virtual Audio Cable\audiorepeater.exe".
    /// </summary>
    private readonly static string firstParentPathNameForExecutable =
      $"{ReferencedApplicationName}\\{executableName}";

    /// <summary>
    /// Typically "C:\".
    /// </summary>
    private static string systemRootPathName = Path.GetPathRoot
      (
        Environment.GetFolderPath
        (
          Environment.SpecialFolder.System
        )
      );

    private static bool doesProcessAndSystemBitMatch =
      Environment.Is64BitProcess == Environment.Is64BitOperatingSystem;

    /// <summary>
    /// The expected executable full path name.
    /// </summary>
    public static string ExpectedExecutableFullPathName
    {
      get
      {
        if (doesProcessAndSystemBitMatch)
        {
          return executablePathNameForBitMatchedProcessAndSystem;
        }

        return executablePathNameForBitUnmatchedProcessAndSystem;
      }
    }


    /// <summary>
    /// Does audio repeater executable exist.
    /// </summary>
    public static bool DoesExecutableExist
    {
      get
      {
        return File.Exists(ExpectedExecutableFullPathName);
      }
    }

    #endregion

    #region Limitations

    public static bool DoIgnoreSafeMaxRepeaterCount = false;

    /// <summary>
    /// Limit the maximum amount of endpoints (audio devices).
    /// Note: Windows NT 6.x and newer does not have a maximum amount of endpoints.
    /// </summary>
    public static uint MaxEndpointCount
    {
      get
      {
        if (Environment.OSVersion.Version.Major < 6)
        {
          return WindowsNT5MaxEndpointCount;
        }

        return AudioRepeaterManageraxVirtualEndpointCount;
      }
    }

    /// <summary>
    /// Limit the maximum amount of repeaters (pairs of audio devices).
    /// </summary>
    public static uint MaxRepeaterCount
    {
      get
      {
        if (DoIgnoreSafeMaxRepeaterCount)
        {
          return uint.MaxValue;
        }

        return SafeMaxRepeaterCount;
      }
    }

    /// <summary>
    /// Arbitrary maximum amount of repeaters.
    /// </summary>
    public readonly static uint SafeMaxRepeaterCount = 1024;

    /// <summary>
    /// Maximum amount of virtual endpoints (virtual audio cables) for VAC.
    /// </summary>
    public readonly static uint AudioRepeaterManageraxVirtualEndpointCount = 256;

    /// <summary>
    /// Maximum amount of endpoints (audio devices) for Windows 5.x and older.
    /// </summary>
    public readonly static uint WindowsNT5MaxEndpointCount = 32;

    #endregion

    #endregion
  }
}
