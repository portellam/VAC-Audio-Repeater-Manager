using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace VACARM.NET4.Views
{
    /// <summary>
    /// Main form view
    /// </summary>
    public partial class MainForm : Form
    {
        private string fileName;

        /// <summary>
        /// Constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        public MainForm()
        {
            InitializeComponent();
            PostInitializeComponent();
        }

        #region File menu

        /// <summary>
        /// Click event logic for openToolStripMenuItem.
        /// Get filename if dialog result is OK.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void OpenToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = Application.CommonAppDataPath,
                Filter = $"{Common.ApplicationNameAsAbbreviation} files| " +
                    $"*{Common.FileExtension}*",
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            fileName = openFileDialog.FileName;
        }

        /// <summary>
        /// Click event logic for exitToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void ExitToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {
            /*
             * TODO:
             *  -add logic to...
             *      -check if runtime data is saved to file.
             *      -warn user to save changes.
             *      -warn user that audio repeaters may exit at app shutdown.
             */

            Application.Exit();
        }

        #endregion

        #region Device menu

        /// <summary>
        /// Click event logic for deviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void DeviceToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {

        }

        #endregion

        #region Link menu

        /// <summary>
        /// Click event logic for linkToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void LinkToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {

        }

        #endregion

        #region View menu
        /// <summary>
        /// Click event logic for aboutToolStripMenuItem.
        /// Set the ToggleDarkModeText.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void ToggleDarkModeToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            SetColorTheme();
        }

        /// <summary>
        /// Click event logic for MainForm.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void MainForm_Load(object sender, EventArgs eventArgs)
        {
        }

        #endregion

        #region Help menu

        /// <summary>
        /// Click event logic for aboutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void AboutToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {
            new AboutForm().Show();
        }

        #endregion
    }
}