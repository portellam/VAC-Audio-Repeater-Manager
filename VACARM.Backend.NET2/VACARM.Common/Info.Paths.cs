using System;
using System.IO;

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

    public readonly static string ExecutableName = "audiorepeater.exe";

    /// <summary>
    /// Typically `C:\Program Files\Virtual Audio Cable\`.
    /// </summary>
    public readonly static string ParentPathNameForBitMatchedProcessAndSystem =
      $"{SystemRootPathName}Program Files\\{ReferencedApplicationName}\\";

    /// <summary>
    /// The expected executable path name.
    /// </summary>
    public static string ExpectedExecutablePathName
    {
      get;
      set;
    } = ParentPathNameForBitMatchedProcessAndSystem;

    /// <summary>
    /// Typically "C:\".
    /// </summary>
    public static string SystemRootPathName
    {
      get
      {
        string name = Path.GetPathRoot
        (
          Environment.GetFolderPath
          (
            Environment.SpecialFolder.System
          )
        );

        if (StringExtension.IsNullOrWhiteSpace(name))
        {
          name = "C:\\";
        }

        return name;
      }
    }

    #endregion
  }
}