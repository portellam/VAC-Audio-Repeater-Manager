namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    private bool settingsPreferLegacyApplication
    {
      get
      {
        return this.settingsPreferLegacyApplicationToolStripMenuItem
          .Checked;
      }
      set
      {
        this.settingsPreferLegacyApplicationToolStripMenuItem
          .Checked = value;

        this.settingsPreferModernApplicationToolStripMenuItem
          .Checked = !value;
      }
    }

    private bool settingsPreferModernApplication
    {
      get
      {
        return this.settingsPreferModernApplicationToolStripMenuItem
          .Checked;
      }
      set
      {
        this.settingsPreferModernApplicationToolStripMenuItem
          .Checked = value;

        this.settingsPreferLegacyApplicationToolStripMenuItem
          .Checked = !value;
      }
    }

    #endregion

    #region Presentation Logic



    #endregion

    #region Interaction Logic

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

      this.settingsPreferLegacyApplication = true;
    }

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

      this.settingsPreferModernApplication = true;
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