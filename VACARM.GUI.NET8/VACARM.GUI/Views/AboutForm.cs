using VACARM.GUI.Accessors;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.GUI.Views
{
  partial class AboutForm :
    Form
  {
    #region Logic

    private void SetComponentsNameProperties()
    {
      this.labelCompanyName.Name = nameof(this.labelCompanyName);
      this.labelCopyright.Name = nameof(this.labelCopyright);
      this.labelProductName.Name = nameof(this.labelProductName);
      this.labelVersion.Name = nameof(this.labelVersion);
      this.tableLayoutPanel.Name = nameof(this.tableLayoutPanel);
      this.logoPictureBox.Name = nameof(this.logoPictureBox);
      this.textBoxDescription.Name = nameof(this.textBoxDescription);
      this.okButton.Name = nameof(this.okButton);
    }

    private void SetComponentsTextProperties()
    {
      this.labelCopyright
        .Text = AssemblyInformationAccessor.AssemblyCopyright;

      this.labelCompanyName
        .Text = AssemblyInformationAccessor.AssemblyCompany;

      this.labelProductName
        .Text = AssemblyInformationAccessor.AssemblyProduct;

      this.labelVersion
        .Text = string.Format
        (
          this.labelVersion
            .Text,
          AssemblyInformationAccessor.AssemblyVersion
        );

      this.Text = string
        .Format
        (
          this.Text,
          Common.Info
            .ApplicationPartialAbbreviatedName
        );

      this.textBoxDescription
        .Text = AssemblyInformationAccessor.AssemblyDescription;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public AboutForm()
    {
      this.InitializeComponent();
      this.SetComponentsNameProperties();
      this.SetComponentsTextProperties();
    }

    #endregion
  }
}