using AudioRepeaterManager.NET8_0.GUI.Accessors;
using AudioRepeaterManager.NET8_0.Domain.Shared;

namespace AudioRepeaterManager.NET8_0.GUI
{
  partial class AboutForm : Form
  {
    #region Logic

    public AboutForm()
    {
      InitializeComponent();
      SetComponentsTextProperties();
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
          Global.ApplicationPartialAbbreviatedName
        );

      this.textBoxDescription.Text = AssemblyInformationAccessor.AssemblyDescription;
    }

    #endregion
  }
}