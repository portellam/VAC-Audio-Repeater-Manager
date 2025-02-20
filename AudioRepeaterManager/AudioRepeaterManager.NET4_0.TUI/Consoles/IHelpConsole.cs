namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  internal interface IHelpConsole
  {
    #region

    string LastConsole { get; set; }

    #endregion

    #region Logic

    void ShowHelp();

    void Return();

    #endregion
  }
}
