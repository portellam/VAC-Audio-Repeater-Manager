namespace VACARM.GUI
{
  /// <summary>
  /// Command line arguments.
  /// </summary>
  public class Arguments
  {
    private bool AlwaysOnTop
    {
      set
      {
        Info.AlwaysOnTop = value;
      }
    }

    private bool AutoStartRepeaters
    {
      set
      {
        Common.Info
          .AutoStartRepeaters = value;
      }
    }

    private bool DarkTheme
    {
      set
      {
        Info.DarkTheme = value;
      }
    }

    private bool ForceMMX
    {
      set
      {
        Common.Info
          .UseMultimediaExtensions = value;
      }
    }

    private bool FullScreen
    {
      set
      {
        Info.FullScreen = value;
      }
    }

    private bool SafeMode
    {
      set
      {
        Common.Info
          .IgnoreMaxLegacyEndpointCount = !value;

        Common.Info
          .IgnoreSafeMaxRepeaterCount = !value;
      }
    }

    private string ExecName
    {
      set
      {
        Common.Info
          .ExecutableName = value;
      }
    }

    private string ExecPath = Common.Info
      .ExpectedExecutablePathName;

    private string FilePath
    {
      set
      {
        Common.Info
          .ExpectedExecutablePathName = Environment.GetFolderPath
          (
            Environment.SpecialFolder
              .UserProfile
          );

      }
    }

    private string[] Files
    {
      set
      {
        Common.Info
          .FileNames = value;
      }
    }
  }
}