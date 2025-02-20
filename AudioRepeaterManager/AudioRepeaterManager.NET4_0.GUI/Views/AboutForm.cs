using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AudioRepeaterManager.NET4_0.Backend.ViewModels.Accessors;
using AudioRepeaterManager.NET4_0.Backend.ViewModels;

namespace AudioRepeaterManager.NET4_0.Backend.Views
{
    /// <summary>
    /// About form view
    /// </summary>
    partial class AboutForm : Form
    {
        #region Parameters

        private List<Control> controlList = new List<Control>();

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();
            PostInitializeComponent();
        }

        /// <summary>
        /// Click event logic for OkButton_Click.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void OkButton_Click(object sender, EventArgs eventArgs)
        {
            this.Close();
        }

        #endregion

        #region Windows Form Designer custom code

        /// <summary>
        /// Add all controls to list.
        /// </summary>
        internal void AddControlsToList()
        {
            controlList.Add(labelCompanyName);
            controlList.Add(labelCopyright);
            controlList.Add(labelProductName);
            controlList.Add(labelVersion);
            controlList.Add(okButton);
            controlList.Add(tableLayoutPanel);
            controlList.Add(textBoxDescription);
        }

        /// <summary>
        /// Code to run after generated code.
        /// </summary>
        internal void PostInitializeComponent()
        {
            SetAssemblyInformation();
            AddControlsToList();
            SetColorTheme();
            CenterToScreen();
        }

        /// <summary>
        /// Set about page information.
        /// </summary>
        internal void SetAssemblyInformation()
        {
            labelCompanyName.Text = AssemblyInformationAccessor.AssemblyCompany;
            labelCopyright.Text = AssemblyInformationAccessor.AssemblyCopyright;
            labelProductName.Text = AssemblyInformationAccessor.AssemblyProduct;
            labelVersion.Text = String.Format
                ("Version {0}", AssemblyInformationAccessor.AssemblyVersion);
            Text = String.Format
                ("About {0}", AssemblyInformationAccessor.AssemblyTitle);
            textBoxDescription.Text = AssemblyInformationAccessor.AssemblyDescription;
        }

        /// <summary>
        /// Set color theme given dark mode is enabled or not.
        /// </summary>
        public void SetColorTheme()
        {
            FormColorUpdater.SetColorsOfConstructor(this);
            FormColorUpdater.SetColorsOfControlCollection(Controls);
            FormColorUpdater.SetColorsOfControlList(controlList);
            Invalidate();
        }

        #endregion
    }
}