namespace VACARM.Common
{
  public partial class Info
  {
    #region Parameters

    /// <summary>
    /// Typically "C:\Program Files\Virtual Audio Cable\".
    /// </summary>
    private static string parentPathNameForBitMatchedProcessAndSystem
    {
      get
      {
        return $"{systemRootPathName}Program Files\\" +
          $"{ReferencedApplicationName}\\";
      }
    }

    /// <summary>
    /// Typically "C:\Program Files (x86)\Virtual Audio Cable\".
    /// </summary>
    private static string parentPathNameForBitUnmatchedProcessAndSystem
    {
      get
      {
        return $"{systemRootPathName}Program Files (x86)\\" +
          $"{ReferencedApplicationName}\\";
      }
    }

    /// <summary>
    /// Typically "C:\".
    /// </summary>
    private static string systemRootPathName
    {
      get
      {
        string? name = Path.GetPathRoot
        (
          Environment.GetFolderPath
          (
            Environment.SpecialFolder.System
          )
        );

        if (string.IsNullOrEmpty(name))
        {
          name = "C:\\";
        }

        return name;
      }
    }

    private static bool doesProcessAndSystemBitMatch
    {
      get
      {
        return Environment.Is64BitProcess == Environment.Is64BitOperatingSystem;
      }
    }

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
          return $"{parentPathNameForBitMatchedProcessAndSystem}" +
            $"{PreferredExecutableName}";
        }

        return $"{parentPathNameForBitUnmatchedProcessAndSystem}" +
          $"{PreferredExecutableName}";
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
  }
}