using VACARM.Common;

namespace VACARM.GUI.Controllers
{
  internal partial class MainController
  {
    #region Parameters

    internal bool PreferAlwaysOnTop { get; set; } = false;
    internal bool PreferAutoStartOfRepeaters { get; set; } = false;
    internal bool PreferFullscreenMode { get; set; } = false;
    internal bool PreferDarkTheme { get; set; } = false;

    internal bool PreferMultimediaExtensions
    {
      get;
      set
      {
        Info.UseMultimediaExtensions = value;
      }
    } = false;

    internal bool PreferNoSafeMode
    {
      get;
      set
      {
        Info.IgnoreSafeMaxRepeaterCount = value;
        Info.IgnoreMaxLegacyEndpointCount = value;
      }
    } = false;

    internal string DefaultFilePath { get; set; } =
      Environment.CurrentDirectory;

    internal string ReferencedExecutableName { get; set; } =
      Info.ExecutableName;

    internal string ReferencedExecutablePath { get; set; } =
      Info.ExpectedExecutablePathName;

    internal string[] FilePathNames { get; set; } = Array.Empty<String>();

    #endregion
  }
}