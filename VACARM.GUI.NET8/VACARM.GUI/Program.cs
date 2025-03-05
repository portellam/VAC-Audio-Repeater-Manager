using VACARM.GUI.Controllers;
using VACARM.GUI.Views;

namespace VACARM.GUI
{
  internal static class Program
  {
    #region Logic

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    /// <param name="argumentArray">The array of argument(s)</param>
    [STAThread]
    static void Main(string[] argumentArray)
    {
      ApplicationConfiguration.Initialize();
      new ArgumentController(argumentArray);
      System.Windows.Forms.Application.Run(new MainForm());
    }

    #endregion
  }
}