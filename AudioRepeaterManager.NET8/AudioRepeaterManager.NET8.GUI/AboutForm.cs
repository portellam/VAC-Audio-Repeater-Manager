﻿using AudioRepeaterManager.NET8.GUI.Accessors;
using AudioRepeaterManager.NET8_0.Backend;

namespace AudioRepeaterManager.NET8.GUI
{
  partial class AboutForm : Form
  {
    public AboutForm()
    {
      InitializeComponent();

      #region Logic

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

      #endregion
    }
  }
}