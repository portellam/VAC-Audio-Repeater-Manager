namespace VACARM.GUI
{
  internal static class Program
  {
    #region Logic

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      ApplicationConfiguration.Initialize();
      System.Windows.Forms.Application.Run(new MainForm());
    }

    #endregion
  }
}