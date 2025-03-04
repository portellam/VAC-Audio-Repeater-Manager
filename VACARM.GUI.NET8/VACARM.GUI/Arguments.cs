using System.Reflection;

namespace VACARM.GUI
{
  /// <summary>
  /// Command line arguments. See also <seealso cref="Enums.Arguments"/>.
  /// </summary>
  public class Arguments
  {
    #region Parameters

    public bool AlwaysOnTop
    {
      set
      {
        Info.AlwaysOnTop = value;
      }
    }

    public bool AutoStartRepeaters
    {
      set
      {
        Common.Info
          .AutoStartRepeaters = value;
      }
    }

    public bool DarkTheme
    {
      set
      {
        Info.DarkTheme = value;
      }
    }

    public bool ForceMMX
    {
      set
      {
        Common.Info
          .UseMultimediaExtensions = value;
      }
    }

    public bool FullScreen
    {
      set
      {
        Info.FullScreen = value;
      }
    }

    public bool SafeMode
    {
      set
      {
        Common.Info
          .IgnoreMaxLegacyEndpointCount = !value;

        Common.Info
          .IgnoreSafeMaxRepeaterCount = !value;
      }
    }

    public string ExecName
    {
      set
      {
        Common.Info
          .ExecutableName = value;
      }
    }

    public string ExecPath = Common.Info
      .ExpectedExecutablePathName;

    public string FilePath
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

    public string[] Files
    {
      set
      {
        Common.Info
          .FileNames = value;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public Arguments()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="argumentPairEnumerable">The enumerable of argument(s)</param>
    public Arguments
    (IEnumerable<KeyValuePair<string, string>> argumentPairEnumerable)
    {
      this.SetRange(argumentPairEnumerable);
    }

    public void SetRange
    (IEnumerable<KeyValuePair<string, string>> argumentPairEnumerable)
    {
      foreach (var item in argumentPairEnumerable)
      {
        this.Set(item);
      }
    }

    public void Set(KeyValuePair<string, string> argumentPair)
    {
      var key = argumentPair.Key;

      var isValid = Enum.TryParse
        (
          typeof(Enums.Arguments),
          key,
          false,
          out var result
      );

      if (!isValid)
      {
        return;
      }

      this.GetType()
        .GetProperty(key)
        .SetValue
        (
          this,
          argumentPair.Value
        );
    }

    #endregion
  }
}