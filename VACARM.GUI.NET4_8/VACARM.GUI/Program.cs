using System;
using System.Windows.Forms;

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
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      //ApplicationConfiguration.Initialize();
      //new ArgumentController(argumentArray);
      //Application.Run(new MainForm());
    }

    #endregion
  }
}