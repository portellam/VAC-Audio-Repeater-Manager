namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public interface IRepeaterConsole : IChildConsole
  {
    #region Logic

    void Pause(string id);
    void PauseAll();
    void ReadFile();
    void Start(string id);
    void StartAll();
    void Stop(string id);
    void StopAll();
    void Update(string id);
    void UpdateAll();
    void WriteFile();

    #endregion
  }
}
