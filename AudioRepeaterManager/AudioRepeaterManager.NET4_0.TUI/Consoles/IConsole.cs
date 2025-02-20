namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public interface IConsole
  {
    #region Logic

    void Exit(int code);

    void Initialize();
    void Show();

    #endregion
  }
}
