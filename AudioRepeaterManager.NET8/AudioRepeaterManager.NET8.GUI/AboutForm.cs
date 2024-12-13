﻿using System.Reflection;

namespace AudioRepeaterManager.NET8.GUI
{
  partial class AboutForm : Form
  {
    public AboutForm()
    {
      InitializeComponent();

      #region Logic

      this.Text = String
        .Format
        (
          "About {0}",
          AssemblyTitle
        );

      this.labelProductName.Text = AssemblyProduct;

      this.labelVersion.Text = String.Format
        (
          "Version {0}",
          AssemblyVersion
        );

      this.labelCopyright.Text = AssemblyCopyright;
      this.labelCompanyName.Text = AssemblyCompany;
      this.textBoxDescription.Text = AssemblyDescription;

      #endregion
    }

    #region Parameters

    #region Assembly Attribute Accessors

    public string AssemblyTitle
      {
        get
        {
          object[] attributes = Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes
            (
              typeof(AssemblyTitleAttribute),
              false
            );

          if (attributes.Length > 0)
          {
            AssemblyTitleAttribute titleAttribute =
              (AssemblyTitleAttribute)attributes[0];

            if (! string.IsNullOrWhiteSpace(titleAttribute.Title))
            {
              return titleAttribute.Title;
            }
          }

          return Path
            .GetFileNameWithoutExtension
            (
              Assembly
                .GetExecutingAssembly()
                .CodeBase
            );
        }
      }

      public string AssemblyVersion
      {
        get
        {
          return Assembly
            .GetExecutingAssembly()
            .GetName()
            .Version
            .ToString();
        }
      }

      public string AssemblyDescription
      {
        get
        {
          object[] attributes = Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes
            (
              typeof(AssemblyDescriptionAttribute),
              false
            );
        
          if (attributes.Length == 0)
          {
            return string.Empty;
          }

          return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }
      }

      public string AssemblyProduct
      {
        get
        {
          object[] attributes = Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes
            (
              typeof(AssemblyProductAttribute),
              false
            );

          if (attributes.Length == 0)
          {
            return string.Empty;
          }

          return ((AssemblyProductAttribute)attributes[0]).Product;
        }
      }

      public string AssemblyCopyright
      {
        get
        {
          object[] attributes = Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes
            (
              typeof(AssemblyCopyrightAttribute),
              false
            );

          if (attributes.Length == 0)
          {
            return string.Empty;
          }

          return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }
      }

      public string AssemblyCompany
      {
        get
        {
          object[] attributes = Assembly
            .GetExecutingAssembly()
            .GetCustomAttributes(
              typeof(AssemblyCompanyAttribute),
              false
            );

          if (attributes.Length == 0)
          {
            return string.Empty;
          }

          return ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
      }

      #endregion
    #endregion
  }
}