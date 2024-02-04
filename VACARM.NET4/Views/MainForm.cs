using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace VACARM.NET4.Views
{
    public partial class MainForm : Form
    {
        private string fileName;
        private readonly static string applicationNameAsAbbreviation = "VACARM";
        private readonly static string fileExtension = "." +
            applicationNameAsAbbreviation.ToLower();

        /// <summary>
        /// Constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        public MainForm()
        {
            InitializeComponent();
            PostInitializeComponent();
        }

        ////////////////////////////////// DeviceMenu //////////////////////////////////
        /// <summary>
        /// Click event logic for exitToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {

        }

        /////////////////////////////////// FileMenu ///////////////////////////////////
        /// <summary>
        /// Click event logic for openToolStripMenuItem.
        /// Get filename if dialog result is OK.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void openToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = Application.CommonAppDataPath,
                Filter = $"{applicationNameAsAbbreviation} files| *{fileExtension}*",
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
        internal void exitToolStripMenuItem_Click(object sender, EventArgs eventArgs)
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

        /////////////////////////////////// HelpMenu ///////////////////////////////////
        /// <summary>
        /// Click event logic for exitToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void helpToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {

        }

        /////////////////////////////////// LinkMenu ///////////////////////////////////
        /// <summary>
        /// Click event logic for exitToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {

        }
    }
}
