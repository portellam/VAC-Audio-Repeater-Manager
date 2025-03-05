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
    /// <param name="argumentEnumerable">The enumerable of argument(s)</param>
    [STAThread]
    static void Main(IEnumerable<string> argumentEnumerable)
    {
      ApplicationConfiguration.Initialize();
      var argumentController = new ArgumentController(argumentEnumerable);
      argumentController.Dispose();
      System.Windows.Forms.Application.Run(new MainForm());
    }

    #endregion
  }
}