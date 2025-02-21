using System.Runtime.InteropServices;

namespace VACARM.Common
{
  public partial class Info
  {
    #region Parameters

    public readonly static string ReferencedApplicationAbbreviatedName = "VAC";

    public readonly static string ReferencedApplicationName = 
      "Virtual Audio Cable";

    public string ApplicationAbbreviatedName { get; } = string.Format
      (
        "{0}ARM",
        ReferencedApplicationAbbreviatedName
      );

    public string ApplicationName { get; } = string.Format
      (
        "{0} Audio Repeater Manager",
        ReferencedApplicationName
      );

    public string ApplicationPartialAbbreviatedName { get; } = string.Format
      (
        "{0} Audio Repeater Manager",
        ReferencedApplicationAbbreviatedName
      );

    public string ReferencedFileExtension { get; } = ".vac";

    public string ScriptFileExtension
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

    public string XMLFileExtension { get; } = ".xml";

    #endregion
  }
}