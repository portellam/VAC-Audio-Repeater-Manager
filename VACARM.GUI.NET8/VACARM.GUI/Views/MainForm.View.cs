namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    private bool preferDarkTheme
    {
      get
      {
        return this.viewPreferDarkThemeToolStripMenuItem
          .Checked;
      }
      set
      {
        this.viewPreferDarkThemeToolStripMenuItem
          .Checked = value;

        this.viewPreferSystemThemeToolStripMenuItem
          .Checked = !value;
      }
    }

    private bool preferSystemTheme
    {
      get
      {
        return this.viewPreferSystemThemeToolStripMenuItem
          .Checked;
      }
      set
      {

        this.viewPreferSystemThemeToolStripMenuItem
          .Checked = value;

        this.viewPreferDarkThemeToolStripMenuItem
          .Checked = !value;
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