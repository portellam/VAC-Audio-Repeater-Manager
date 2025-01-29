using System.Runtime.InteropServices;

namespace VACARM.Backend.Common
{
  public partial class Info
  {
    #region Parameters

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
  }
}