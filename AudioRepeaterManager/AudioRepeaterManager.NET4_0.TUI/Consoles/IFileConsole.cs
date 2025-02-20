using System.IO;

namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public interface IFileConsole : IChildConsole
  {
    #region Parameters

    FileStream FileStream { get; set; }

    #endregion

    #region Logic
    void Read();
    void Write();
    void Update();

    #endregion
  }
}
