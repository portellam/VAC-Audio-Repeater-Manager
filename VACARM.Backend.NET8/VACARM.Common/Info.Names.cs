using System;
using System.Runtime.InteropServices;

namespace VACARM.Common
{
  public partial class Info
  {
    #region Parameters

    public readonly static string ReferencedApplicationAbbreviatedName = "VAC";

    public readonly static string ReferencedApplicationName = 
      "Virtual Audio Cable";

    public static string ApplicationAbbreviatedName { get; } = string.Format
      (
        "{0}ARM",
        ReferencedApplicationAbbreviatedName
      );

    public static string ApplicationName { get; } = string.Format
      (
        "{0} Audio Repeater Manager",
        ReferencedApplicationName
      );

    public static string ApplicationPartialAbbreviatedName { get; } = string.Format
      (
        "{0} Audio Repeater Manager",
        ReferencedApplicationAbbreviatedName
      );

    public static string ReferencedFileExtension { get; } = ".vac";

    public static string ScriptFileExtension
    {
      get
      {
        if (!IsOSWindows)
        {
          throw new NotImplementedException
            (
              string.Format
              (
                "Failed to determine script file extension."
                + "Operating System {0} is not supported.",
                OSDescription
              )
            );
        }

        return ".bat";
      }
    }

    #endregion
  }
}