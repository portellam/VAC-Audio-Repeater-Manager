using VACARM.GUI.Accessors;
using VACARM.GUI.Helpers;

namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
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

      new UsageForm();
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