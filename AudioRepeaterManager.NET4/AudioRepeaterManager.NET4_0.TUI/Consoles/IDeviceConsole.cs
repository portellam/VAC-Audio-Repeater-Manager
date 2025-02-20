using System.Collections.Generic;

namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public interface IDeviceConsole : IChildConsole
  {
    #region Logic

    void Disable(string id);
    void DisableAll();
    void DisableRange(List<string> id);
    void Enable(string id);
    void EnableAll();
    void EnableRange(List<string> id);
    void Refresh();
    void ReadFile();
    void Remove(string id);
    void SetAsDefault(string id);
    void Update(string id);
    void WriteFile();

    List<string> GetAllAbsent();
    List<string> GetAllDisabled();
    List<string> GetAllDuplex();
    List<string> GetAllEnabled();
    List<string> GetAllInput();
    List<string> GetAllOutput();
    List<string> GetAllPresent();

    #endregion
  }
}
