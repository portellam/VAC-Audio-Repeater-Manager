using System;
using System.Windows.Forms;

namespace VACARM.NET4.Views
{
    public partial class MainForm : Form
    {
        private string fileName;
        private readonly static string applicationNameAsAbbreviation = "VACARM";
        private readonly static string fileExtension = "." +
            applicationNameAsAbbreviation.ToLower();

        public MainForm()
        {
            InitializeComponent();
            PostInitializeComponent();
        }

        /// <summary>
        /// Click event logic for openToolStripMenuItem.
        /// Get filename if dialog result is OK.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs eventArgs)
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void deviceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void linkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
