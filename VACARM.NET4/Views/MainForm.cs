using NAudio.CoreAudioApi;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using VACARM.NET4.Models;

namespace VACARM.NET4.Views
{
    /// <summary>
    /// Main form view
    /// </summary>
    public partial class MainForm : Form
    {
        #region Parameters

        private string fileName;
        private DeviceList deviceList;

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        public MainForm()
        {
            deviceList = new DeviceList();
            InitializeComponent();
            PostInitializeComponent();
        }

        #endregion

        #region 1. File menu logic

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

        #region 2. Device menu logic

        /// <summary>
        /// Click event logic for addWaveInDeviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void addWaveInDeviceToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            deviceList.MoveDeviceToSelectedList(sender as string);
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for addWaveOutDeviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void addWaveOutDeviceToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            deviceList.MoveDeviceToSelectedList(sender as string);
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for disableWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void disableWaveInToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        /// <summary>
        /// Click event logic for disableWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void disableWaveOutToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
        }

        /// <summary>
        /// Click event logic for enableWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void enableWaveInToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        /// <summary>
        /// Click event logic for enableWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void enableWaveOutToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        /// <summary>
        /// Click event logic for reloadAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void reloadAllToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            deviceList.SetDeviceLists();
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for removeWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void removeWaveInToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        /// <summary>
        /// Click event logic for removeWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void removeWaveOutToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        #endregion

        #region 3. Link menu logic

        /// <summary>
        /// Click event logic for linkWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void linkWaveInToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        /// <summary>
        /// Click event logic for linkWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void linkWaveOutToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        /// <summary>
        /// Click event logic for unlinkWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void unlinkWaveInToolStripMenuItem_Click
            (object sender, EventArgs eventArgse)
        {

        }

        /// <summary>
        /// Click event logic for unlinkWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        private void unlinkWaveOutToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        #endregion

        #region 4. View menu logic
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

        #region 5. Help menu logic

        /// <summary>
        /// Click event logic for aboutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void AboutToolStripMenuItem_Click(object sender, EventArgs eventArgs)
        {
            if (Application.OpenForms.OfType<AboutForm>().Count() > 0)
            {
                Application.OpenForms.OfType<AboutForm>().ToList().ForEach
                    (x => x.Close());
            }

            new AboutForm().Show();
        }

        #endregion
    }
}