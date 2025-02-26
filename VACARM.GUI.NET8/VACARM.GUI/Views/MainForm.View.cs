namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters



    #endregion

    #region Presentation Logic



    #endregion

    #region Interaction Logic

    private void viewAlwaysOnTopToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }
    private void viewPreferSystemThemeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender is null)
      {
        return;
      }

      if (Environment.OSVersion.Version.Major < 6)
      {
        viewPreferSystemThemeToolStripMenuItem.Enabled = false;
      }

      preferSystemTheme = true;
    }

    private void viewPreferDarkThemeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void viewToggleFullScreenModeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion
  }
}