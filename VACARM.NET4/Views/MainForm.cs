using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
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

        private bool doAddSelectedWaveInOrWaveOutContainCheckedMenuItem
        {
            get
            {
                return DoesToolStripItemCollectionContainCheckedMenuItem
                    (deviceAddSelectWaveInToolStripMenuItem.DropDownItems)
                    || DoesToolStripItemCollectionContainCheckedMenuItem
                    (deviceAddSelectWaveOutToolStripMenuItem.DropDownItems);
            }
        }

        private bool toggleDeviceAddSelectAllToolStripMenuItem;
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
            toggleDeviceAddSelectAllToolStripMenuItem = false;
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

        /// <summary>
        /// Create new repeater data model. Called whenever a file is opened or closed.
        /// </summary>
        internal void SetRepeaterDataModel()
        {
            repeaterDataModel = new RepeaterDataModel();
        }

        #endregion

        #region 2. Device menu logic

        /// <summary>
        /// Click event logic for deviceAddSelectAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddSelectAllToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            ToggleDeviceAddSelectAllToolStripMenuItem();

            SetCheckedPropertyForEachToolStripMenuItem
                (ref deviceAddSelectWaveInToolStripMenuItem,
                toggleDeviceAddSelectAllToolStripMenuItem);

            SetCheckedPropertyForEachToolStripMenuItem
                (ref deviceAddSelectWaveOutToolStripMenuItem,
                toggleDeviceAddSelectAllToolStripMenuItem);

            SetEnabledPropertyOfDeviceAddConfirmToolStripMenuItem();
        }

        /// <summary>
        /// Click event logic for deviceAddConfirmToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddConfirmToolStripMenuItem_Click
            (object sender, EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            MoveAllCheckedToolStripMenuItemsToNewToolStripItemCollection
                (ref deviceAddSelectWaveInToolStripMenuItem,
                ref deviceRemoveWaveInToolStripMenuItem);

            MoveAllCheckedToolStripMenuItemsToNewToolStripItemCollection
                (ref deviceAddSelectWaveOutToolStripMenuItem,
                ref deviceRemoveWaveOutToolStripMenuItem);

            //deviceListModel.MoveMMDeviceToSelectedList()                              //FIXME

            SetEnabledPropertyOfDeviceAddConfirmToolStripMenuItem();
            SetEnabledPropertyOfDeviceAddSelectAllToolStripMenuItem();
        }

        internal void deviceAddConfirmToolStripMenuItemEnabled_Toggle
            (object sender, EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            SetEnabledPropertyOfDeviceAddConfirmToolStripMenuItem();
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

            SetEnabledPropertyOfDeviceAddConfirmToolStripMenuItem();
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

        /// <summary>
        /// Set Enable property of deviceAddConfirmToolStripMenuItem.
        /// </summary>
        internal void SetEnabledPropertyOfDeviceAddConfirmToolStripMenuItem()
        {
            deviceAddConfirmToolStripMenuItem.Enabled =
                doAddSelectedWaveInOrWaveOutContainCheckedMenuItem;
        }

        /// <summary>
        /// Set Enable property of deviceAddSelectAllToolStripMenuItem.
        /// </summary>
        internal void SetEnabledPropertyOfDeviceAddSelectAllToolStripMenuItem()
        {
            deviceAddSelectAllToolStripMenuItem.Enabled =
                doAddSelectedWaveInOrWaveOutContainCheckedMenuItem;
        }

        /// <summary>
        /// Toggle bool value, call deviceAddSelectAllToolStripMenuItem is clicked.
        /// </summary>
        internal void ToggleDeviceAddSelectAllToolStripMenuItem()
        {
            toggleDeviceAddSelectAllToolStripMenuItem =
                !toggleDeviceAddSelectAllToolStripMenuItem;
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

            repeaterDataModel.AddDictionary(inputDeviceControl, outputDeviceControl);
            inputDeviceControl = null;
            outputDeviceControl = null;
            InitializeLists();
        }

        /// <summary>
        /// Click event logic for linkAddWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkAddWaveInToolStripMenuItem_Click
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

            inputDeviceControl = new DeviceControl(mMDevice);
            SetPropertiesOfSelectedToolStripMenuItem
                (ref linkAddWaveInToolStripMenuItem, toolStripMenuItem);
            AddToRepeaterModel();
        }

        /// <summary>
        /// Click event logic for linkAddWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkAddWaveOutToolStripMenuItem_Click
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

            outputDeviceControl = new DeviceControl(mMDevice);
            SetPropertiesOfSelectedToolStripMenuItem
                (ref linkAddWaveOutToolStripMenuItem, toolStripMenuItem);
            AddToRepeaterModel();
        }

        /// <summary>
        /// Remove input and output device controls to repeater model.
        /// </summary>
        internal void RemoveFromRepeaterModel()
        {
            if (inputDeviceControl is null || outputDeviceControl is null)
            {
                return;
            }

            repeaterDataModel.RemoveDictionary(inputDeviceControl, outputDeviceControl);
            ResetPropertiesForSelectedLinkAddToolStripMenuItem(inputDeviceControl);
            ResetPropertiesForSelectedLinkAddToolStripMenuItem(outputDeviceControl);
            inputDeviceControl = null;
            outputDeviceControl = null;
            InitializeLists();
        }

        /// <summary>
        /// Resets properties for selected linkAddWaveIn or linkAddWaveOut.
        /// </summary>
        /// <param name="deviceControl">The device control</param>
        internal void ResetPropertiesForSelectedLinkAddToolStripMenuItem
            (DeviceControl deviceControl)
        {
            if (deviceControl is null || deviceControl.MMDevice is null)
            {
                return;
            }

            if (deviceControl.MMDevice.DataFlow == DataFlow.Capture)
            {
                ResetPropertiesForEachSelectedToolStripMenuItem
                    (deviceControl.MMDevice, ref linkAddWaveInToolStripMenuItem);
            }
            else
            {
                ResetPropertiesForEachSelectedToolStripMenuItem
                    (deviceControl.MMDevice, ref linkAddWaveOutToolStripMenuItem);
            }
        }

        /// <summary>
        /// Click event logic for linkRemoveWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkRemoveWaveInToolStripMenuItem_Click
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

            inputDeviceControl = new DeviceControl(mMDevice);
            SetPropertiesOfSelectedToolStripMenuItem
                (ref linkRemoveWaveInToolStripMenuItem, toolStripMenuItem);
            RemoveFromRepeaterModel();
        }

        /// <summary>
        /// Click event logic for linkRemoveWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkRemoveWaveOutToolStripMenuItem_Click
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

            outputDeviceControl = new DeviceControl(mMDevice);
            SetPropertiesOfSelectedToolStripMenuItem
                (ref linkRemoveWaveOutToolStripMenuItem, toolStripMenuItem);
            RemoveFromRepeaterModel();
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

        #region ToolStrip helper logic

        /// <summary>
        /// Does tool strip item collection contain any checked tool strip menu item(s).
        /// </summary>
        /// <param name="toolStripItemCollection">The tool strip item collection</param>
        /// <returns>True/False</returns>
        internal bool DoesToolStripItemCollectionContainCheckedMenuItem
            (ToolStripItemCollection toolStripItemCollection)
        {
            if (toolStripItemCollection is null || toolStripItemCollection.Count == 0)
            {
                return false;
            }

            foreach (var item in toolStripItemCollection)
            {
                if (!(item is ToolStripMenuItem))
                {
                    continue;
                }

                if ((item as ToolStripMenuItem).Checked)
                {
                    return true;
                }
            }

            return false;
        }

        internal void MoveAllCheckedToolStripMenuItemsToNewToolStripItemCollection
            (ref ToolStripMenuItem firstToolStripMenuItem,
            ref ToolStripMenuItem secondToolStripMenuItem)
        {
            if (firstToolStripMenuItem is null
                || firstToolStripMenuItem.DropDownItems.Count == 0
                || secondToolStripMenuItem is null)
            {
                return;
            }

            List<ToolStripMenuItem> tempToolStripMenuItemList =
                new List<ToolStripMenuItem>();

            foreach (var item in firstToolStripMenuItem.DropDownItems)
            {
                if (item is null || !(item is ToolStripMenuItem))
                {
                    continue;
                }

                tempToolStripMenuItemList.Add(item as ToolStripMenuItem);
            }

            foreach (ToolStripMenuItem toolStripMenuItem in tempToolStripMenuItemList)
            {
                if (!toolStripMenuItem.Checked)
                {
                    continue;
                }

                firstToolStripMenuItem.DropDownItems.Remove(toolStripMenuItem);

                if (secondToolStripMenuItem.DropDownItems.Contains(toolStripMenuItem))
                {
                    continue;
                }

                secondToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }

            SetPropertiesOfToolStripMenuItemGivenItemCollectionIsEmptyOrNot
                (ref firstToolStripMenuItem);

            SetPropertiesOfToolStripMenuItemGivenItemCollectionIsEmptyOrNot
                (ref secondToolStripMenuItem);

            GC.Collect();
        }

        /// <summary>
        /// Resets properties for selected nested tool strip item in tool strip menu
        /// item.
        /// </summary>
        /// <param name="mMDevice">The MMDevice</param>
        /// <param name="parentToolStripMenuItem">The referenced parent tool strip menu
        /// item</param>
        internal void ResetPropertiesForEachSelectedToolStripMenuItem
            (MMDevice mMDevice, ref ToolStripMenuItem parentToolStripMenuItem)
        {
            string isSelectedSuffix = " (Selected)";

            foreach (ToolStripItem toolStripItem in
                parentToolStripMenuItem.DropDownItems)
            {
                if (toolStripItem.ToolTipText != mMDevice.FriendlyName)
                {
                    continue;
                }

                toolStripItem.Text = Regex.Replace
                    (toolStripItem.Text, isSelectedSuffix, string.Empty);
            }
        }

        internal void SetAutoClosePropertyOfToolStripDropDown_MouseEnter
            (object sender, EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripDropDown))
            {
                return;
            }

            (sender as ToolStripDropDown).AutoClose = false;
        }

        internal void SetAutoClosePropertyOfToolStripDropDown_MouseLeave
            (object sender, EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripDropDown))
            {
                return;
            }

            (sender as ToolStripDropDown).AutoClose = true;
        }

        internal void SetCheckedPropertyForEachToolStripMenuItem
            (ref ToolStripMenuItem toolStripMenuItem, bool isChecked)
        {
            if (toolStripMenuItem is null || toolStripMenuItem.DropDownItems.Count == 0)
            {
                return;
            }

            foreach (var item in toolStripMenuItem.DropDownItems)
            {
                if (item is null || !(item is ToolStripMenuItem))
                {
                    continue;
                }

                (item as ToolStripMenuItem).Checked = isChecked;
            }
        }

        /// <summary>
        /// Set properties of tool strip menu item given tool strip item collection is
        /// empty or not.
        /// </summary>
        /// <param name="toolStripMenuItem">The tool strip menu item</param>
        internal void SetPropertiesOfToolStripMenuItemGivenItemCollectionIsEmptyOrNot
            (ref ToolStripMenuItem toolStripMenuItem)
        {
            bool isNotEmpty = toolStripMenuItem.DropDownItems.Count != 0;
            toolStripMenuItem.Enabled = isNotEmpty;

            if (isNotEmpty)
            {
                toolStripMenuItem.ToolTipText = string.Empty;
            }
            else
            {
                toolStripMenuItem.ToolTipText = "No items found.";
            }
        }

        /// <summary>
        /// Set properties of every nested tool strip item. If the item matches the
        /// child tool strip menu item, append a substring and disable the item.
        /// If not, reverse the changes.
        /// </summary>
        /// <param name="parentToolStripMenuItem">The parent tool strip menu
        /// item</param>
        /// <param name="childToolStripMenuItem">The child tool strip menu item</param>
        internal void SetPropertiesOfSelectedToolStripMenuItem
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
                if (toolStripItem != childToolStripMenuItem)
                {
                    toolStripItem.Text = Regex.Replace
                        (toolStripItem.Text, isSelectedSuffix, string.Empty);
                }
                else
                {
                    toolStripItem.Text += isSelectedSuffix;
                }
            }
        }

        #endregion
    }

    /*
    * TODO:
    * -import/export system MMEnumeration to file.
    * -repeaters > disable item of and start repeaters whose device(s) are disabled or not present.
    * -check for existing running repeaters (should this app exit).
    * -run task of starting repeaters in another thread.
    * -check for glitches in repeaters.
    * -make suggestions if audio glitches occur.
    * -allow for enabling/disabling devices.
    * -click event to open active audio repeater.
    */
}