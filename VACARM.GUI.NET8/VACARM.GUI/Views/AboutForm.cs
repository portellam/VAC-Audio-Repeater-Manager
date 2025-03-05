using VACARM.GUI.Accessors;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.GUI.Views
{
  partial class AboutForm :
    Form
  {
    #region Logic

    private void okButton_Click
    (
      object sender, 
      EventArgs eventArgs
    )
    {
      this.Dispose();
    }

    private void SetComponents()
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
      this.SetComponents();
    }

    #endregion

  }
}