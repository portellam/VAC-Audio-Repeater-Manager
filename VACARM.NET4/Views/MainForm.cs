using System;
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
            SetDeviceList();
            InitializeComponent();
            PostInitializeComponent();
        }

        /// <summary>
        /// Set the device list.
        /// </summary>
        internal void SetDeviceList()
        {
            deviceList = new DeviceList();
        }

        #endregion

        #region 1. File menu logic

        /// <summary>
        /// Click event logic for openToolStripMenuItem.
        /// Get filename if dialog result is OK.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void fileOpenToolStripMenuItem_Click(object sender, EventArgs eventArgs)
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
        internal void fileExitToolStripMenuItem_Click(object sender, EventArgs eventArgs)
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
        /// Click event logic for deviceAddAllToolStripMenuItemDropDown.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddAllToolStripMenuItemDropDown_Click
            (object sender, EventArgs eventArgs)
        {
            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            deviceList.MoveAllDevicesToSelectedLists();
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for deviceAddToolStripMenuItemDropDown.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddToolStripMenuItemDropDown_Click
            (object sender, EventArgs eventArgs)
        {
            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            deviceList.MoveDeviceToSelectedList
                ((sender as ToolStripMenuItem).ToolTipText);
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for reloadAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceReloadAllToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            deviceList.SetDeviceLists();
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for deviceRemoveAllToolStripMenuItemDropDown.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceRemoveAllToolStripMenuItemDropDown_Click
            (object sender, EventArgs eventArgs)
        {
            SetDeviceList();
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for deviceRemoveToolStripMenuItemDropDown.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceRemoveToolStripMenuItemDropDown_Click
            (object sender, EventArgs eventArgs)
        {
            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            deviceList.MoveDeviceFromSelectedList
                ((sender as ToolStripMenuItem).ToolTipText);
            InitializeLists();
        }
        
        #endregion

        #region 3. Link menu logic

        /// <summary>
        /// Click event logic for linkWaveInDeviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkWaveInDeviceToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        /// <summary>
        /// Click event logic for linkWaveOutDeviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkWaveOutDeviceToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {

        }

        /// <summary>
        /// Click event logic for unlinkWaveInDeviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void unlinkWaveInDeviceToolStripMenuItem_Click
            (object sender, EventArgs eventArgse)
        {

        }

        /// <summary>
        /// Click event logic for unlinkWaveOutDeviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void unlinkWaveOutDeviceToolStripMenuItem_Click
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
        internal void viewToggleDarkModeToolStripMenuItem_Click
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
        internal void helpAboutToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
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