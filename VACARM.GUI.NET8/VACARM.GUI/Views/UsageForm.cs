namespace VACARM.GUI.Views
{
  partial class UsageForm :
    Form
  {
    #region Logic

    private void SetComponents()
    {
      this.Text = String.Format
      (
        "{0} Command Argument Help",
        Common.Info.ApplicationAbbreviatedName
      );

      var exeName = Common.Info
            .ApplicationAbbreviatedName
            .ToLower() + ".exe";

      this.textBoxDescription
        .Text = string.Format
        (
          this.textBoxDescription
            .Text,
          exeName,
          Common.Info
            .ExpectedExecutablePathName
        );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public UsageForm()
    {
      this.InitializeComponent();
      this.SetComponents();
    }

    private void textBoxDescription_TextChanged(object sender, EventArgs e)
    {
    }

    #endregion

    private void okButton_Click(object sender, EventArgs e)
    {
      this.Dispose();
    }
  }
}