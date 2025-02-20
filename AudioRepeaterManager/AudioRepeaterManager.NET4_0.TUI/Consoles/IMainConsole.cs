namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public interface IMainConsole : IConsole
  {
    #region Parameters

    bool AutomateMinimumViableSetup { get; set; }
    bool EnableAllDevices { get; set; }
    bool EnableAllRepeaterDevices { get; set; }
    bool StartAllRepeaters { get; set; }
    bool IgnoreWarnings { get; set; }
    bool ParseAudioRepeaterPathName { get; set; }
    string AudioRepeaterPathName { get; set; }
    string FilePath { get; set; }
    string[] Arguments { get; set; }
    string[] ChildConsoles { get; }

    #endregion

    #region Logic

    void Main(string[] arguments);

    #endregion
  }
}
