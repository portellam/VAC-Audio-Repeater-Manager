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
  }
}