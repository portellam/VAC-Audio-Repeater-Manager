namespace VACARM.Common
{
  public partial class Info
  {
    #region Parameters

    /// <summary>
    /// True/false does audio repeater executable exist.
    /// </summary>
    public bool DoesExecutableExist { get; } =
      File.Exists(ExpectedExecutablePathName);

    public readonly static bool DoesProcessAndSystemBitMatch =
      Environment.Is64BitProcess == Environment.Is64BitOperatingSystem;

    /// <summary>
    /// The name of the executable for Multi Media Extensions (MME) or legacy 
    /// use-cases.
    /// </summary>
    public readonly static string MMEExecutableName = "audiorepeater.exe";

    /// <summary>
    /// The name of the executable for Kernel Streaming (KS).
    /// </summary>
    public readonly static string KSExecutableName = "audiorepeater_ks.exe";

    /// <summary>
    /// Typically `C:\Program Files\Virtual Audio Cable\`.
    /// </summary>
    public readonly static string ParentPathNameForBitMatchedProcessAndSystem =
      $"{SystemRootPathName}Program Files\\{ReferencedApplicationName}\\";

    /// <summary>
    /// Typically "C:\Program Files (x86)\Virtual Audio Cable\".
    /// </summary>
    public readonly static string ParentPathNameForBitUnmatchedProcessAndSystem =
      $"{SystemRootPathName}Program Files (x86)\\{ReferencedApplicationName}\\";

    public static string ExecutableName
    {
      get;
      set;
    } = UseMultimediaExtensions ?
      MMEExecutableName :
      KSExecutableName;

    /// <summary>
    /// The expected executable path name.
    /// </summary>
    public static string ExpectedExecutablePathName
    {
      get;
      set;
    } = DoesProcessAndSystemBitMatch ?
      ParentPathNameForBitMatchedProcessAndSystem :
      ParentPathNameForBitUnmatchedProcessAndSystem;

    /// <summary>
    /// Typically "C:\".
    /// </summary>
    public static string SystemRootPathName
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

    #endregion
  }
}