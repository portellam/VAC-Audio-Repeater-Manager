using System;
using System.Reflection;
using System.Windows.Forms;

namespace VACARM.NET4.Views
{
    /// <summary>
    /// About form view
    /// </summary>
    partial class AboutForm : Form
    {
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
    }
}