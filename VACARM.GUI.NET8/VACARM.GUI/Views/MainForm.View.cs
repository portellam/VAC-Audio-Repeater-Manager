namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    private bool preferDarkTheme
    {
      get
      {
        return viewPreferDarkThemeToolStripMenuItem.Checked;
      }
      set
      {
        viewPreferDarkThemeToolStripMenuItem.Checked = value;
        viewPreferSystemThemeToolStripMenuItem.Checked = !value;
      }
    }

    private bool preferSystemTheme
    {
      get
      {
        return viewPreferSystemThemeToolStripMenuItem.Checked;
      }
      set
      {

        viewPreferSystemThemeToolStripMenuItem.Checked = value;
        viewPreferDarkThemeToolStripMenuItem.Checked = !value;
      }
    }

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
      if (sender == null)
      {
        return;
      }


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
        this.viewPreferSystemThemeToolStripMenuItem
          .Enabled = false;
      }

      this.preferSystemTheme = true;
    }

    private void viewPreferDarkThemeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void viewToggleFullScreenModeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    #endregion
  }
}