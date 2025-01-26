using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Policy;

namespace AudioRepeaterManager.NET8_0.Domain
{
  /// <summary>
  /// Global parameters
  /// </summary>
  public class Global
  {
    #region Parameters

    #region Application info

    public static string ApplicationAbbreviatedName
    {
      get
      {
        return string.Format
        (
          "{0}ARM",
          ReferencedApplicationName
        );
      }
    }

    public static string ApplicationName
    {
      get
      {
        return string.Format
        (
           "{0} Audio Repeater Manager",
            ReferencedApplicationName
        );
      }
    }

    public static string ApplicationPartialAbbreviatedName
    {
      get
      {
        return string.Format
        (
           "{0} Audio Repeater Manager",
            ReferencedApplicationAbbreviatedName
        );
      }
    }

    public readonly static string ReferencedApplicationAbbreviatedName = "VAC";
    public readonly static string ReferencedApplicationName = "Virtual Audio Cable";
    public readonly static string ReferencedFileExtension = ".vac";

    public static string ScriptFileExtension
    {
      get
      {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
          return ".bat";
        }

        else if
        (
          RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
          || RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
          || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD)
        )
        {
          return ".sh";
        }

        else
        {
          throw new NotImplementedException
            (
              string.Format
              (
                "Failed to determine script file extension."
                + "Operating System {0} is not supported.",
               RuntimeInformation.OSDescription
              )
            );
        }
      }
    }

    public readonly static string XMLFileExtension = ".xml";

    #endregion

    #region Executable path names

    /// <summary>
    /// Typically "C:\Program Files\Virtual Audio Cable\".
    /// </summary>
    private static string parentPathNameForBitMatchedProcessAndSystem =
      $"{systemRootPathName}Program Files\\{ReferencedApplicationName}\\";

    /// <summary>
    /// Typically "C:\Program Files (x86)\Virtual Audio Cable\".
    /// </summary>
    private static string parentPathNameForBitUnmatchedProcessAndSystem =
      $"{systemRootPathName}Program Files (x86)\\{ReferencedApplicationName}\\";

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

    public static bool PreferLegacyExecutable = false;

    /// <summary>
    /// The name of the executable for Multi Media Extensions (MME) or legacy use-cases.
    /// </summary>
    public static string MMEExecutableName
    {
      get
      {
        return "audiorepeater.exe";
      }
    }

    /// <summary>
    /// The name of the executable for Kernel Streaming (KS).
    /// </summary>
    public static string KSExecutableName
    {
      get
      {
        return "audiorepeater_ks.exe";
      }
    }

    /// <summary>
    /// The preferred name of the executable.
    /// </summary>
    public static string PreferredExecutableName
    {
      get
      {
        if (PreferLegacyExecutable)
        {
          return MMEExecutableName;
        }

        return KSExecutableName;
      }
    }

    /// <summary>
    /// The expected executable full path name.
    /// </summary>
    public static string ExpectedExecutableFullPathName
    {
      get
      {
        if (doesProcessAndSystemBitMatch)
        {
          return $"{parentPathNameForBitMatchedProcessAndSystem}{PreferredExecutableName}";
        }

        return $"{parentPathNameForBitUnmatchedProcessAndSystem}{PreferredExecutableName}";
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
    public static bool DoIgnoreMaxLegacyEndpointCount = false;

    /// <summary>
    /// Limit the maximum amount of endpoints (audio devices).
    /// Note: Windows NT 6.x and newer does not have a maximum amount of endpoints.
    /// </summary>
    public static uint MaxEndpointCount
    {
      get
      {
        if
        (
          Environment.OSVersion.Version.Major < 6
          ! && DoIgnoreMaxLegacyEndpointCount
        )
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
