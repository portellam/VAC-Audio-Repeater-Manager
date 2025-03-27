using VACARM.GUI.Accessors;
using VACARM.Common;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.GUI.Views
{
  partial class AboutForm :
    Form
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public AboutForm()
    {
      InitializeComponent();
      SetComponentsNameProperties();
      SetComponentsTextProperties();
    }

    private void SetComponentsNameProperties()
    {
      labelCompanyName.Name = nameof(this.labelCompanyName);
      labelCopyright.Name = nameof(this.labelCopyright);
      labelProductName.Name = nameof(this.labelProductName);
      labelVersion.Name = nameof(this.labelVersion);
      tableLayoutPanel.Name = nameof(this.tableLayoutPanel);
      logoPictureBox.Name = nameof(this.logoPictureBox);
      textBoxDescription.Name = nameof(this.textBoxDescription);
      okButton.Name = nameof(this.okButton);
    }

    private void SetComponentsTextProperties()
    {
      this.labelCopyright.Text = AssemblyInformationAccessor.AssemblyCopyright;
      this.labelCompanyName.Text = AssemblyInformationAccessor.AssemblyCompany;
      this.labelProductName.Text = AssemblyInformationAccessor.AssemblyProduct;

      this.labelVersion.Text = string.Format
        (
          "Version {0}",
          AssemblyInformationAccessor.AssemblyVersion
        );

      this.Text = string
        .Format
        (
          "About {0}",
          Common.Info.ApplicationPartialAbbreviatedName
        );

      this.textBoxDescription.Text =
        AssemblyInformationAccessor.AssemblyDescription;
    }

    #endregion
  }
}