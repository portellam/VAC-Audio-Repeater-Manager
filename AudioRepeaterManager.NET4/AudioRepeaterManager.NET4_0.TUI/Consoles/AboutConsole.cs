using System.ComponentModel;

namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public class AboutConsole :
    Console,
    IAboutConsole
    //INotifyPropertyChanged
  {
    #region Parameters

    public override string Name
    { 
      get
      {
        return "About";
      }
    }

    #endregion

    #region Logic

    void Initialize();
    void Show();

    #endregion
  }
}
