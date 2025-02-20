using System.Collections.Generic;

namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public class ChildConsoles
  {
    #region Parameters

    public readonly static Dictionary<string, string> ChildConsoleTitleAndNamespace =
      new Dictionary<string, string>()
      {
        {
          "About",
          nameof(AboutConsole)
        },
        {
          "File",
          nameof(FileConsole)
        },
        {
          "Devices",
          nameof(DeviceConsole)
        },
        {
          "Repeaters",
          nameof(RepeaterConsole)
        },
        {
          "Help",
          nameof(HelpConsole)
        }
      };

    #endregion
  }
}
