using VACARM.GUI.Accessors;
using VACARM.GUI.Helpers;

namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Presentation Logic

    private void SetHelpComponents()
    {
      this.helpAboutToolStripMenuItem
        .Text = string.Format
        (
          this.helpAboutToolStripMenuItem
            .Text,
          Common.Info
            .ApplicationAbbreviatedName
        );

      this.helpApplicationWebsiteToolStripMenuItem
        .Text = string.Format
        (
          this.helpApplicationWebsiteToolStripMenuItem
            .Text,
          Common.Info
            .ReferencedApplicationName
        );

      this.helpWebsiteToolStripMenuItem
        .Text = string.Format
        (
          this.helpWebsiteToolStripMenuItem
            .Text,
          Common.Info
            .ApplicationAbbreviatedName
        );
    }

    #endregion

    #region Interaction Logic

    private void helpAboutToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      new AboutForm().ShowDialog();
    }

    private void helpApplicationWebsiteToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      try
      {
        UrlRedirectHelper.GoToSite("https://vac.muzychenko.net");
      }

      catch
      {
      }
    }

    private void helpCommandLineArgumentsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      new UsageForm().ShowDialog();
    }

    private void helpWebsiteToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      try
      {
        UrlRedirectHelper.GoToSite(AssemblyInformationAccessor.AssemblyWebsite);
        return;
      }

      catch
      {
      }
    }

    #endregion
  }
}