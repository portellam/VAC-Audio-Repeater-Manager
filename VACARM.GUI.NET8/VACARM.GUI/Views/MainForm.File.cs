namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters



    #endregion

    #region Presentation Logic



    #endregion

    #region Interaction Logic

    private void fileCloseAllToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileCloseMultipleToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileCloseToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileExitToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      // check if it is safe to exit.

      Environment.Exit(0);
    }

    private void fileNewToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileOpenToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      OpenFileDialog openFileDialog = new OpenFileDialog()
      {
        AddExtension = true,
        DefaultExt = ".vacarm",
        CheckFileExists = true,
        CheckPathExists = true,
        InitialDirectory = "C:\\",
        Multiselect = true,
        OkRequiresInteraction = true,
        ShowPreview = true,

      };

      openFileDialog.ShowDialog();

      string fileName = openFileDialog.FileName;
      //send to FileController?
      //file controller populates an instance of the repositories?

      openFileDialog.AddToRecent = true;
      openFileDialog.ShowPreview = true;
    }

    private void fileOpenContainingFolderToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileSaveToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileSaveAsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileSaveACopyAsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileSaveAllToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion
  }
}
