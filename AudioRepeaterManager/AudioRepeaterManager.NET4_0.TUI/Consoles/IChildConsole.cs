using System.Collections.Generic;

namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public interface IChildConsole : IConsole
  {
    #region Parameters

    string LastConsole { get; set; }
    Dictionary<string, string> ChildConsoleTitleAndNamespace { get; set; }

    #endregion

    #region Logic

    void GoTo(string consoleName);
    
    void GoToPrevious();

    #endregion
  }
}
