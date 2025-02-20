using System;

namespace AudioRepeaterManager.NET4_0.TUI.Consoles
{
  public abstract class Console : IConsole
  {
    #region Parameters

    /// <summary>
    /// The display name of the console view.
    /// </summary>
    public abstract string Name { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Exit from the main console.
    /// </summary>
    /// <param name="code"></param>
    public void Exit(int code)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Initialize the console view.
    /// </summary>
    public void Initialize()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Show the console view. 
    /// </summary>
    public void Show()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
