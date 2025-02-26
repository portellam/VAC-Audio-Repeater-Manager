namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    private bool preferModernApplication
    {
      get
      {
        return settingsPreferModernApplicationToolStripMenuItem.Checked;
      }
      set
      {
        settingsPreferModernApplicationToolStripMenuItem.Checked = value;
        settingsPreferLegacyApplicationToolStripMenuItem.Checked = !value;
      }
    }

    private bool preferLegacyApplication
    {
      get
      {
        return settingsPreferLegacyApplicationToolStripMenuItem.Checked;
      }
      set
      {
        settingsPreferLegacyApplicationToolStripMenuItem.Checked = value;
        settingsPreferModernApplicationToolStripMenuItem.Checked = !value;
      }
    }

    #endregion

    #region Presentation Logic



    #endregion

    #region Interaction Logic

    private void settingsPreferModernApplicationToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if
      (
        !Environment.Is64BitOperatingSystem
        || sender is null
      )
      {
        return;
      }

      this.preferModernApplication = true;
    }

    private void settingsPreferLegacyApplicationToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if
      (
        !Environment.Is64BitOperatingSystem
        || sender is null
      )
      {
        return;
      }

      this.preferLegacyApplication = true;
    }

    private void settingsSetApplicationPathToolStripMenuItem_Click
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

    private void settingsStartAllRepeatersOnLoadToolStripMenuItem_Click
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

    private void settingsToggleBogusModeToolStripMenuItem_Click
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

    private void settingsToggleSafeModeToolStripMenuItem_Click
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