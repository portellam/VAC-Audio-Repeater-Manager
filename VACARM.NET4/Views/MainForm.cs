using NAudio.CoreAudioApi;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VACARM.NET4.Models;
using VACARM.NET4.ViewModels;

namespace VACARM.NET4.Views
{
    /// <summary>
    /// Main form view
    /// </summary>
    public partial class MainForm : Form
    {
        #region Parameters

        private string fileName;
        private DeviceListModel deviceListModel;
        private DeviceControl inputDeviceControl { get; set; }
        private DeviceControl outputDeviceControl { get; set; }
        private RepeaterDataModel repeaterDataModel { get; set; }

        public const string WaveInAsString = "Wave In";
        public const string WaveOutAsString = "Wave Out";

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
            deviceListModel = new DeviceListModel();
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
        /// Click event logic for deviceAddAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddAllToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            deviceListModel.MoveAllMMDevicesToSelectedLists();
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for deviceAddWaveInToolStripMenuItemDropDown.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddWaveInToolStripMenuItemDropDown_Click
            (object sender, EventArgs eventArgs)
        {
            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            deviceListModel.MoveMMDeviceToSelectedList
                ((sender as ToolStripMenuItem).ToolTipText, DataFlow.Capture);
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for deviceAddWaveOutToolStripMenuItemDropDown.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddWaveOutToolStripMenuItemDropDown_Click
            (object sender, EventArgs eventArgs)
        {
            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            deviceListModel.MoveMMDeviceToSelectedList
                ((sender as ToolStripMenuItem).ToolTipText, DataFlow.Render);
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
            SetDeviceList();
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for deviceRemoveAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceRemoveAllToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            SetDeviceList();
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for deviceRemoveWaveInToolStripMenuItemDropDown.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceRemoveWaveInToolStripMenuItemDropDown_Click
            (object sender, EventArgs eventArgs)
        {
            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            deviceListModel.MoveMMDeviceFromSelectedList
                ((sender as ToolStripMenuItem).ToolTipText, DataFlow.Capture);
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for deviceRemoveWaveOutToolStripMenuItemDropDown.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceRemoveWaveOutToolStripMenuItemDropDown_Click
            (object sender, EventArgs eventArgs)
        {
            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            deviceListModel.MoveMMDeviceFromSelectedList
                ((sender as ToolStripMenuItem).ToolTipText, DataFlow.Render);
            InitializeLists();
        }

        #endregion

        #region 3. Link menu logic

        /// <summary>
        /// Add input and output device controls to repeater model.
        /// </summary>
        internal void AddToRepeaterModel()
        {
            if (inputDeviceControl is null || inputDeviceControl.MMDevice is null
                || outputDeviceControl is null || outputDeviceControl.MMDevice is null)
            {
                return;
            }

            //repeaterDataModel.AddDictionary(inputDeviceControl, outputDeviceControl);
            inputDeviceControl = null;
            outputDeviceControl = null;
        }

        /// <summary>
        /// Click event logic for linkWaveInDeviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkWaveInDeviceToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            if (sender is null || sender.GetType() != typeof(ToolStripMenuItem))
            {
                return;
            }

            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            MMDevice mMDevice = deviceListModel.GetMMDevice
                (toolStripMenuItem.ToolTipText, DataFlow.Capture);

            if (mMDevice is null)
            {
                return;
            }

            inputDeviceControl = new DeviceControl()
            {
                MMDevice = mMDevice,
            };

            SetPropertiesOfToolStripMenuItem
                (ref linkAddWaveInToolStripMenuItem, toolStripMenuItem);
            AddToRepeaterModel();
        }

        /// <summary>
        /// Click event logic for linkWaveOutDeviceToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkWaveOutDeviceToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            if (sender is null || sender.GetType() != typeof(ToolStripMenuItem))
            {
                return;
            }

            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            MMDevice mMDevice = deviceListModel.GetMMDevice
                (toolStripMenuItem.ToolTipText, DataFlow.Render);

            if (mMDevice is null)
            {
                return;
            }

            outputDeviceControl = new DeviceControl()
            {
                MMDevice = mMDevice,
            };

            SetPropertiesOfToolStripMenuItem
                (ref linkAddWaveOutToolStripMenuItem, toolStripMenuItem);
            AddToRepeaterModel();
        }

        /// <summary>
        /// Set properties of every nested tool strip item. If the item matches the
        /// child tool strip menu item, append a substring and disable the item.
        /// If not, reverse the changes.
        /// </summary>
        /// <param name="parentToolStripMenuItem">The parent tool strip menu
        /// item</param>
        /// <param name="childToolStripMenuItem">The child tool strip menu item</param>
        internal void SetPropertiesOfToolStripMenuItem
            (ref ToolStripMenuItem parentToolStripMenuItem,
            ToolStripMenuItem childToolStripMenuItem)
        {
            if (!parentToolStripMenuItem.DropDownItems.Contains(childToolStripMenuItem))
            {
                return;
            }

            string isSelectedSuffix = " (Selected)";

            foreach
                (ToolStripItem toolStripItem in parentToolStripMenuItem.DropDownItems)
            {
                bool isNotSelected = toolStripItem != childToolStripMenuItem;

                if (isNotSelected)
                {
                    toolStripItem.Enabled = isNotSelected;
                    toolStripItem.Text = Regex.Replace
                        (toolStripItem.Text, isSelectedSuffix, string.Empty);
                }
                else
                {
                    toolStripItem.Enabled = isNotSelected;
                    toolStripItem.Text += isSelectedSuffix;
                }
            }
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

        #region 4. Repeater menu logic

        #endregion

        #region 5. View menu logic
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

        #region 6. Help menu logic

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

        internal DataFlow getDataFlowOfToolStripMenuItemText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return DataFlow.All;
            }

            if (text == WaveInAsString)
            {
                return DataFlow.Capture;
            }
            else if (text == WaveOutAsString)
            {
                return DataFlow.Render;
            }
            else
            {
                return DataFlow.All;
            }
        }
    }
}