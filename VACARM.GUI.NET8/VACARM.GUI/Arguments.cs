namespace VACARM.GUI
{
  /// <summary>
  /// Command line arguments.
  /// </summary>
  public class Arguments
  {
    public bool AlwaysOnTop = false;
    public bool AutoStart = false;
    public bool DarkTheme = false;
    public bool ForceMMX = false;
    public bool FullScreen = false;
    public bool SafeMode = false;

    public string ExecName = Common.Info
      .ExecutableName;

    public string ExecPath = Common.Info
      .ExpectedExecutablePathName;

    public string FilePath = Environment.GetFolderPath
      (
        Environment.SpecialFolder
          .UserProfile
      );

    public string[] Files = Array.Empty<String>();
  }
}