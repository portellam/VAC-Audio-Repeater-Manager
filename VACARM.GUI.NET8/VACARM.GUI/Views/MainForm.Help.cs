using VACARM.GUI.Helpers;

namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters



    #endregion

    #region Presentation Logic



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

      string projectName = "vac-audio-repeater-manager";

      try
      {
        UrlRedirectHelper
          .GoToSite
          (
            string.Format
            (
              "https://www.github.com/portellam/{0}",
              projectName
            )
          );

        return;
      }

      catch
      {
      }

      try
      {
        UrlRedirectHelper
          .GoToSite
          (
            string.Format
            (
              "https://www.codeberg.org/portellam/{0}",
              projectName
            )
          );

        return;
      }

      catch
      {
      }
    }

    #endregion
  }
}