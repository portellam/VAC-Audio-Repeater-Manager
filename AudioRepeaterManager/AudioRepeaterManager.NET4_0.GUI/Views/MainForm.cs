using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AudioRepeaterManager.NET4_0.Backend.Models;
using AudioRepeaterManager.NET4_0.Backend.ViewModels;
using AudioRepeaterManager.NET4_0.Backend.ViewModels.Accessors;
using AudioRepeaterManager.NET4_0.Backend.ViewModels.ColorTable;
using AudioRepeaterManager.NET4_0.GUI.Extensions;
using AudioRepeaterManager.NET4_0.GUI.Extensions.PropertyValueChanged;
using PropertyValueChangedEventArgs =
    AudioRepeaterManager.NET4_0.GUI.Extensions.PropertyValueChanged.PropertyValueChangedEventArgs;

namespace AudioRepeaterManager.NET4_0.Backend.Views
{
    public partial class MainForm : Form
    {
        #region Parameters

        internal AboutForm aboutForm;

        internal string darkModeText
        {
            get
            {
                string text = "Dark Mode";

                if (!viewToggleDarkModeToolStripMenuItem.Checked)
                {
                    return $"Enable {text}";
                }

                return $"Disable {text}";
            }
        }

        internal bool DoForceColorTheme
        {
            get
            {
                return GraphicsWindow.DoForceColorTheme;
            }
        }

        internal bool IsLightThemeEnabled
        {
            get
            {
                return GraphicsWindow.IsLightThemeEnabled;
            }
            set
            {
                GraphicsWindow.IsLightThemeEnabled = value;
                OnLightThemeIsEnabledValueChanged();
            }
        }

        internal bool isCheckedDeviceAddNameListEmpty
        {
            get
            {
                return !deviceAddSelectWaveInToolStripMenuItem.DropDownItems
                        .Cast<ToolStripMenuItem>()
                        .Any(toolStripMenuItem => toolStripMenuItem.Checked)
                    && !deviceAddSelectWaveOutToolStripMenuItem.DropDownItems
                        .Cast<ToolStripMenuItem>()
                        .Any(toolStripMenuItem => toolStripMenuItem.Checked);
            }
        }

        internal bool isCheckedDeviceAddNameListFull
        {
            get
            {
                return !isCheckedDeviceAddNameListEmpty
                    && deviceAddSelectWaveInToolStripMenuItem.DropDownItems
                        .Cast<ToolStripMenuItem>()
                        .All(toolStripMenuItem => toolStripMenuItem.Checked)
                    && deviceAddSelectWaveOutToolStripMenuItem.DropDownItems
                        .Cast<ToolStripMenuItem>()
                        .All(toolStripMenuItem => toolStripMenuItem.Checked);
            }
        }

        internal bool isCheckedDeviceRemoveNameListEmpty
        {
            get
            {
                return !deviceRemoveSelectWaveInToolStripMenuItem.DropDownItems
                        .Cast<ToolStripMenuItem>()
                        .Any(toolStripMenuItem => toolStripMenuItem.Checked)
                    && !deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems
                        .Cast<ToolStripMenuItem>()
                        .Any(toolStripMenuItem => toolStripMenuItem.Checked);
            }
        }

        internal bool isCheckedDeviceRemoveNameListFull
        {
            get
            {
                return !isCheckedDeviceRemoveNameListEmpty
                    && deviceRemoveSelectWaveInToolStripMenuItem.DropDownItems
                        .Cast<ToolStripMenuItem>()
                        .All(toolStripMenuItem => toolStripMenuItem.Checked)
                    && deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems
                        .Cast<ToolStripMenuItem>()
                        .All(toolStripMenuItem => toolStripMenuItem.Checked);
            }
        }

        internal bool isDeviceAddNameListEmpty
        {
            get
            {
                return isDeviceAddWaveInNameListEmpty
                    && isDeviceAddWaveOutNameListEmpty;
            }
        }

        internal bool isDeviceAddWaveInNameListEmpty
        {
            get
            {
                return deviceAddSelectWaveInToolStripMenuItem is null
                     || deviceAddSelectWaveInToolStripMenuItem.DropDownItems is null
                     || deviceAddSelectWaveInToolStripMenuItem.DropDownItems.Count == 0;
            }
        }

        internal bool isDeviceAddWaveOutNameListEmpty
        {
            get
            {
                return deviceAddSelectWaveOutToolStripMenuItem is null
                    || deviceAddSelectWaveOutToolStripMenuItem.DropDownItems is null
                    || deviceAddSelectWaveOutToolStripMenuItem.DropDownItems.Count == 0;
            }
        }

        internal bool isDeviceRemoveNameListEmpty
        {
            get
            {
                return isDeviceRemoveWaveInNameListEmpty
                    && isDeviceRemoveWaveOutNameListEmpty;
            }
        }

        internal bool isDeviceRemoveWaveInNameListEmpty
        {
            get
            {
                return
                    deviceRemoveSelectWaveOutToolStripMenuItem is null ||
                    deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems is null ||
                    deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems.Count == 0;
            }
        }

        internal bool isDeviceRemoveWaveOutNameListEmpty
        {
            get
            {
                return
                    deviceRemoveSelectWaveOutToolStripMenuItem is null ||
                    deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems is null ||
                    deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems.Count == 0;
            }
        }

        //internal OldDeviceListModel deviceListModel; //REPLACE
        internal DeviceControl inputDeviceControl { get; set; }
        internal DeviceControl outputDeviceControl { get; set; }
        //internal RepeaterDataModel repeaterDataModel { get; set; } //REPLACE
    internal string fileName;

        internal List<Control> controlList = new List<Control>();

        internal List<string> checkedDeviceAddWaveInNameList
        {
            get
            {
                return deviceAddSelectWaveInToolStripMenuItem.DropDownItems
                    .Cast<ToolStripMenuItem>()
                    .Where(toolStripMenuItem => toolStripMenuItem.Checked)
                    .Select(toolStripMenuItem => toolStripMenuItem.ToolTipText)
                    .ToList();
            }
        }

        internal List<string> checkedDeviceAddWaveOutNameList
        {
            get
            {
                return deviceAddSelectWaveOutToolStripMenuItem.DropDownItems
                    .Cast<ToolStripMenuItem>()
                    .Where(toolStripMenuItem => toolStripMenuItem.Checked)
                    .Select(toolStripMenuItem => toolStripMenuItem.ToolTipText)
                    .ToList();
            }
        }

        internal List<string> checkedDeviceRemoveWaveInNameList
        {
            get
            {
                return deviceRemoveSelectWaveInToolStripMenuItem.DropDownItems
                    .Cast<ToolStripMenuItem>()
                    .Where(toolStripMenuItem => toolStripMenuItem.Checked)
                    .Select(toolStripMenuItem => toolStripMenuItem.ToolTipText)
                    .ToList();
            }
        }

        internal List<string> checkedDeviceRemoveWaveOutNameList
        {
            get
            {
                return deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems
                    .Cast<ToolStripMenuItem>()
                    .Where(toolStripMenuItem => toolStripMenuItem.Checked)
                    .Select(toolStripMenuItem => toolStripMenuItem.ToolTipText)
                    .ToList();
            }
        }

        internal List<string> unselectedDeviceWaveInNameList
        {
            get
            {
                return deviceAddSelectWaveInToolStripMenuItem.DropDownItems
                    .Cast<ToolStripMenuItem>()
                    .Select(toolStripMenuItem => toolStripMenuItem.ToolTipText)
                    .ToList();
            }
        }

        internal List<string> unselectedDeviceWaveOutNameList
        {
            get
            {
                return deviceAddSelectWaveOutToolStripMenuItem.DropDownItems
                    .Cast<ToolStripMenuItem>()
                    .Select(toolStripMenuItem => toolStripMenuItem.ToolTipText)
                    .ToList();
            }
        }

        internal List<string> selectedDeviceWaveInNameList
        {
            get
            {
                return deviceRemoveSelectWaveInToolStripMenuItem.DropDownItems
                    .Cast<ToolStripMenuItem>()
                    .Select(toolStripMenuItem => toolStripMenuItem.ToolTipText)
                    .ToList();
            }
        }

        internal List<string> selectedDeviceWaveOutNameList
        {
            get
            {
                return deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems
                    .Cast<ToolStripMenuItem>()
                    .Select(toolStripMenuItem => toolStripMenuItem.ToolTipText)
                    .ToList();
            }
        }

        internal List<ToolStripItem> toolStripItemList = new List<ToolStripItem>();

        public const string WaveInAsString = "Wave In";
        public const string WaveOutAsString = "Wave Out";
        public event PropertyValueChangedDelegate IsLightThemeEnabledValueChanged;

        public readonly static MessageBoxWrapper MessageBoxWrapper =
          new MessageBoxWrapper(AssemblyInformationAccessor.AssemblyTitle);
    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
        public MainForm()
        {
            InitializeComponent();
            PostInitializeComponent();
        }

        /// <summary>
        /// Set the device list model.
        /// </summary>
        internal void SetDeviceListModel()
        {
            deviceListModel = new OldDeviceListModel();
        }

        /// <summary>
        /// Set the device list model properties given the current tool strip menu item
        /// drop downs.
        /// </summary>
        internal void SetDeviceListModelLists()
        {
            deviceListModel.SetSelectedList(DataFlow.Capture,
                selectedDeviceWaveInNameList);

            deviceListModel.SetSelectedList(DataFlow.Render,
                selectedDeviceWaveOutNameList);

            deviceListModel.SetUnselectedList(DataFlow.Capture,
                unselectedDeviceWaveInNameList);

            deviceListModel.SetUnselectedList(DataFlow.Render,
                unselectedDeviceWaveOutNameList);
        }

        #endregion

        #region Intialize list logic (Append new objects here!)

        /// <summary>
        /// Add all controls to list.
        /// </summary>
        internal void InitializeControlsList()
        {
            controlList.Clear();
            controlList.Add(tabControl1);
            controlList.Add(graphTab);
            controlList.Add(gridTab);
        }

        /// <summary>
        /// Add all tool strip items to list.
        /// </summary>
        internal void InitializeToolStripItemList()
        {
            toolStripItemList.Clear();
            toolStripItemList.Add(deviceAddConfirmToolStripMenuItem);
            toolStripItemList.Add(deviceAddSelectAllToolStripMenuItem);
            toolStripItemList.Add(deviceAddSelectToolStripMenuItem);
            toolStripItemList.Add(deviceAddSelectWaveInToolStripMenuItem);
            toolStripItemList.Add(deviceAddSelectWaveOutToolStripMenuItem);
            toolStripItemList.Add(deviceAddSelectToolStripMenuItem);
            toolStripItemList.Add(deviceAddToolStripMenuItem);
            toolStripItemList.Add(deviceReloadAllToolStripMenuItem);
            toolStripItemList.Add(deviceRemoveConfirmToolStripMenuItem);
            toolStripItemList.Add(deviceRemoveSelectAllLinkedToolStripMenuItem);
            toolStripItemList.Add(deviceRemoveSelectAllToolStripMenuItem);
            toolStripItemList.Add(deviceRemoveSelectAllUnlinkedToolStripMenuItem);
            toolStripItemList.Add(deviceRemoveSelectWaveInToolStripMenuItem);
            toolStripItemList.Add(deviceRemoveSelectWaveOutToolStripMenuItem);
            toolStripItemList.Add(deviceRemoveSelectToolStripMenuItem);
            toolStripItemList.Add(deviceRemoveToolStripMenuItem);
            toolStripItemList.Add(deviceToolStripSeparator1);
            toolStripItemList.Add(deviceToolStripSeparator2);
            toolStripItemList.Add(deviceToolStripSeparator2);
            toolStripItemList.Add(fileCloseToolStripMenuItem);
            toolStripItemList.Add(fileExitToolStripMenuItem);
            toolStripItemList.Add(fileNewToolStripMenuItem);
            toolStripItemList.Add(fileOpenToolStripMenuItem);
            toolStripItemList.Add(fileSaveACopyAsToolStripMenuItem);
            toolStripItemList.Add(fileSaveAsToolStripMenuItem);
            toolStripItemList.Add(fileSaveToolStripMenuItem);
            toolStripItemList.Add(fileToolStripMenuItem);
            toolStripItemList.Add(fileToolStripSeparator1);
            toolStripItemList.Add(helpAboutToolStripMenuItem);
            toolStripItemList.Add(helpToolStripMenuItem);
            toolStripItemList.Add(linkAddConfirmToolStripMenuItem);
            toolStripItemList.Add(linkAddSelectWaveInToolStripMenuItem);
            toolStripItemList.Add(linkAddSelectWaveOutToolStripMenuItem);
            toolStripItemList.Add(linkAddSelectToolStripMenuItem);
            toolStripItemList.Add(linkAddToolStripMenuItem);
            toolStripItemList.Add(linkDefaultBitRateToolStripMenuItem);
            toolStripItemList.Add(linkDefaultBufferToolStripMenuItem);
            toolStripItemList.Add(linkDefaultChannelsToolStripMenuItem);
            toolStripItemList.Add(linkDefaultPrefillToolStripMenuItem);
            toolStripItemList.Add(linkDefaultResyncAtToolStripMenuItem);
            toolStripItemList.Add(linkDefaultSamplingRateToolStripMenuItem);
            toolStripItemList.Add(linkRemoveConfirmToolStripMenuItem);
            toolStripItemList.Add(linkRemoveSelectAllToolStripMenuItem);
            toolStripItemList.Add(linkRemoveSelectWaveInToolStripMenuItem);
            toolStripItemList.Add(linkRemoveSelectWaveOutToolStripMenuItem);
            toolStripItemList.Add(linkRemoveSelectToolStripMenuItem);
            toolStripItemList.Add(linkRemoveToolStripMenuItem);
            toolStripItemList.Add(linkToolStripMenuItem);
            toolStripItemList.Add(linkToolStripSeparator1);
            toolStripItemList.Add(linkToolStripSeparator2);
            toolStripItemList.Add(repeaterRestartSelectAllToolStripMenuItem);
            toolStripItemList.Add(repeaterRestartToolStripMenuItem);
            toolStripItemList.Add(repeaterStartSelectAllToolStripMenuItem);
            toolStripItemList.Add(repeaterStartToolStripMenuItem);
            toolStripItemList.Add(repeaterStopSelectAllToolStripMenuItem);
            toolStripItemList.Add(repeaterStopToolStripMenuItem);
            toolStripItemList.Add(repeaterToolStripMenuItem);
            toolStripItemList.Add(repeaterToolStripSeparator1);
            toolStripItemList.Add(repeaterToolStripSeparator2);
            toolStripItemList.Add(viewToggleDarkModeToolStripMenuItem);
            toolStripItemList.Add(viewToolStripMenuItem);
        }

        #endregion

        #region Initialization logic

        /// <summary>
        /// Initialize device drop downs.
        /// </summary>
        internal void InitializeDeviceToolStripMenuItemDropDowns()
        {
            deviceAddSelectWaveInToolStripMenuItem.DropDownItems.Clear();
            deviceAddSelectWaveOutToolStripMenuItem.DropDownItems.Clear();
            deviceRemoveSelectWaveInToolStripMenuItem.DropDownItems.Clear();
            deviceRemoveSelectWaveOutToolStripMenuItem.DropDownItems.Clear();
            linkAddSelectWaveInToolStripMenuItem.DropDownItems.Clear();
            linkAddSelectWaveOutToolStripMenuItem.DropDownItems.Clear();
            linkRemoveSelectWaveInToolStripMenuItem.DropDownItems.Clear();
            linkRemoveSelectWaveOutToolStripMenuItem.DropDownItems.Clear();

            deviceToolStripMenuItem.Enabled = false;
            Invalidate();

            InitializeDeviceToolStripMenuItemDropDown
                (deviceAddSelectToolStripMenuItemEnabled_Confirm,
                deviceAddSelectWaveInToolStripMenuItem,
                deviceListModel.UnselectedWaveInMMDeviceList);

            InitializeDeviceToolStripMenuItem(deviceAddSelectWaveInToolStripMenuItem);

            InitializeDeviceToolStripMenuItemDropDown
                (deviceAddSelectToolStripMenuItemEnabled_Confirm,
                deviceAddSelectWaveOutToolStripMenuItem,
                deviceListModel.UnselectedWaveOutMMDeviceList);

            InitializeDeviceToolStripMenuItem(deviceAddSelectWaveOutToolStripMenuItem);

            InitializeDeviceToolStripMenuItemDropDown
                (deviceRemoveSelectToolStripMenuItemEnabled_Confirm,
                deviceRemoveSelectWaveInToolStripMenuItem,
                deviceListModel.SelectedWaveInMMDeviceList);

            InitializeDeviceToolStripMenuItem
                (deviceRemoveSelectWaveInToolStripMenuItem);

            InitializeDeviceToolStripMenuItemDropDown
                (deviceRemoveSelectToolStripMenuItemEnabled_Confirm,
                deviceRemoveSelectWaveOutToolStripMenuItem,
                deviceListModel.SelectedWaveOutMMDeviceList);

            InitializeDeviceToolStripMenuItem
                (deviceRemoveSelectWaveOutToolStripMenuItem);

            InitializeDeviceToolStripMenuItemDropDown
                (linkAddSelectWaveInToolStripMenuItem_Click,
                linkAddSelectWaveInToolStripMenuItem,
                deviceListModel.SelectedWaveInMMDeviceList);

            InitializeDeviceToolStripMenuItem(linkAddSelectWaveInToolStripMenuItem);

            InitializeDeviceToolStripMenuItemDropDown
                (linkAddSelectWaveOutToolStripMenuItem_Click,
                linkAddSelectWaveOutToolStripMenuItem,
                deviceListModel.SelectedWaveOutMMDeviceList);

            InitializeDeviceToolStripMenuItem(linkAddSelectWaveOutToolStripMenuItem);

            InitializeDeviceToolStripMenuItemDropDown
                (linkRemoveSelectWaveInToolStripMenuItem_Click,
                linkRemoveSelectWaveInToolStripMenuItem,
                repeaterDataModel.LinkWaveInMMDeviceList);

            InitializeDeviceToolStripMenuItem(linkRemoveSelectWaveInToolStripMenuItem);

            InitializeDeviceToolStripMenuItemDropDown
                (linkRemoveSelectWaveOutToolStripMenuItem_Click,
                linkRemoveSelectWaveOutToolStripMenuItem,
                repeaterDataModel.LinkWaveOutMMDeviceList);

            InitializeDeviceToolStripMenuItem(linkRemoveSelectWaveOutToolStripMenuItem);

            deviceToolStripMenuItem.Enabled = true;
            Invalidate();
        }

        /// <summary>
        /// Initialize device tool strip menu item properties.
        /// </summary>
        /// <param name="toolStripMenuItem">The tool strip menu item</param>
        internal void InitializeDeviceToolStripMenuItem
            (ToolStripMenuItem toolStripMenuItem)
        {
            if (toolStripMenuItem is null)
            {
                return;
            }

            FormColorUpdater.SetColorsOfToolStripItem(toolStripMenuItem);
            SetMousePropertiesOfToolStripMenuItemDropDown(toolStripMenuItem);

            SetPropertiesOfToolStripMenuItemGivenItemDropDownIsEmptyOrNot
                (toolStripMenuItem);
        }

        /// <summary>
        /// Initialize the device tool strip menu item.
        /// </summary>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="mMDeviceAndNumberedFriendlyName">The MMDevice and numbered
        /// friendly name</param>
        /// <param name="toolStripMenuItemList">The tool strip menu item list</param>
        internal void InitializeDeviceToolStripMenuItemDropDownItem
            (EventHandler eventHandler,
                KeyValuePair<MMDevice, string> mMDeviceAndNumberedFriendlyName,
                List<ToolStripMenuItem> toolStripMenuItemList)
        {
            //if (mMDevice.State == DeviceState.NotPresent)
            //{
            //	return;
            //}

            if (mMDeviceAndNumberedFriendlyName.Key is null
                || mMDeviceAndNumberedFriendlyName.Value is null
                || toolStripMenuItemList is null)
            {
                return;
            }

            bool deviceIsEnabled = mMDeviceAndNumberedFriendlyName.Key.State !=
                DeviceState.Disabled;

            string text = $"{mMDeviceAndNumberedFriendlyName.Value} ";

            if (deviceIsEnabled)
            {
                text += "(Enabled)";
            }
            else
            {
                text += "(Disabled)";
            }

            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
            {
                CheckState = CheckState.Unchecked,
                CheckOnClick = true,
                Text = text,
                ToolTipText = mMDeviceAndNumberedFriendlyName.Key.FriendlyName,
            };

            if (eventHandler != null)
            {
                toolStripMenuItem.Click += new EventHandler(eventHandler);
            }

            toolStripMenuItem.CheckOnClick = true;

            if (toolStripMenuItemList.Contains(toolStripMenuItem))
            {
                return;
            }

            toolStripMenuItemList.Add(toolStripMenuItem);
        }

        /// <summary>
        /// Initialize a device tool strip menu item drop down by parsing the related
        /// device list.
        /// </summary>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="parentToolStripMenuItem">The parent device tool strip menu item
        /// </param>
        /// <param name="mMDeviceList">The device list</param>
        internal void InitializeDeviceToolStripMenuItemDropDown
            (EventHandler eventHandler, ToolStripMenuItem parentToolStripMenuItem,
            List<MMDevice> mMDeviceList)
        {
            if (parentToolStripMenuItem is null || mMDeviceList is null
                || mMDeviceList.Count == 0)
            {
                return;
            }

            DataFlow dataFlow = mMDeviceList.FirstOrDefault().DataFlow;
            parentToolStripMenuItem.DropDownItems.Clear();

            List<ToolStripMenuItem> toolStripMenuItemList =
                new List<ToolStripMenuItem>();

            deviceListModel.GetDeviceAndNumberedFriendlyNameDictionary(mMDeviceList)
                .ToList().ForEach(x =>
                {
                    InitializeDeviceToolStripMenuItemDropDownItem
                        (eventHandler, x, toolStripMenuItemList);
                });

            toolStripMenuItemList.ForEach(toolStripMenuItem =>
                {
                    InitializeDeviceToolStripMenuItemDropDownItemText(dataFlow,
                        toolStripMenuItem);
                });

            parentToolStripMenuItem.DropDownItems
                .AddRange(toolStripMenuItemList.ToArray());
        }

        

        /// <summary>
        /// Initialize the text of the device tool strip menu item.
        /// </summary>
        /// <param name="dataFlow">The data flow</param>
        /// <param name="toolStripMenuItem">The device tool strip menu item</param>
        internal void InitializeDeviceToolStripMenuItemDropDownItemText
            (DataFlow dataFlow, ToolStripMenuItem toolStripMenuItem)
        {
            int index = deviceListModel.GetIndexOfMMDevice(dataFlow,
                toolStripMenuItem.ToolTipText);

            index++;
            string prefix = $"{index}. ";

            toolStripMenuItem.Text = string.Format("{0,4} {1}", prefix,
                toolStripMenuItem.Text);
        }

        /// <summary>
        /// Initialize repeater drop downs.
        /// </summary>
        internal void InitializeLinksToolStripMenuItemDropDowns()
        {
            linkAddConfirmToolStripMenuItem.DropDownItems.Clear();
            linkRemoveConfirmToolStripMenuItem.DropDownItems.Clear();

            //TODO: work on this more.
        }

        /// <summary>
        /// Initialize all lists.
        /// </summary>
        internal void InitializeLists()
        {
            InitializeControlsList();
            InitializeToolStripItemList();
            GC.Collect();
        }

        /// <summary>
        /// Initialize repeater drop downs.
        /// </summary>
        internal void InitializeRepeaterToolStripMenuItemDropDowns()
        {
            repeaterRestartSelectToolStripMenuItem.DropDownItems.Clear();
            repeaterStartConfirmToolStripMenuItem.DropDownItems.Clear();
            repeaterStopConfirmToolStripMenuItem.DropDownItems.Clear();

            //TODO: work on this more.
        }

        /// <summary>
        /// Logic to execute before InitializeLists.
        /// </summary>
        internal void ModifyDropDownsBeforeInitialization()
        {
            InitializeDeviceToolStripMenuItemDropDowns();
            InitializeLinksToolStripMenuItemDropDowns();
            InitializeRepeaterToolStripMenuItemDropDowns();
        }

        /// <summary>
        /// Initialize background workers.
        /// </summary>
        internal void InitializeBackgroundWorkers()
        {
            deviceAddConfirmBackgroundWorker.DoWork +=
                deviceAddConfirmBackgroundWorker_DoWork;

            deviceAddConfirmBackgroundWorker.RunWorkerCompleted +=
                deviceBackgroundWorker_RunWorkerCompleted;

            deviceReloadAllBackgroundWorker.DoWork +=
                deviceReloadAllBackgroundWorker_DoWork;

            deviceReloadAllBackgroundWorker.RunWorkerCompleted +=
                deviceBackgroundWorker_RunWorkerCompleted;

            deviceRemoveConfirmBackgroundWorker.DoWork +=
                deviceRemoveConfirmBackgroundWorker_DoWork;

            deviceRemoveConfirmBackgroundWorker.RunWorkerCompleted +=
                deviceBackgroundWorker_RunWorkerCompleted;

            linkAddConfirmBackgroundWorker.DoWork +=
                linkAddConfirmBackgroundWorker_DoWork;

            linkAddConfirmBackgroundWorker.RunWorkerCompleted +=
                linkBackgroundWorker_RunWorkerCompleted;

            linkRemoveConfirmBackgroundWorker.DoWork +=
                linkRemoveConfirmBackgroundWorker_DoWork;

            linkRemoveConfirmBackgroundWorker.RunWorkerCompleted +=
                linkBackgroundWorker_RunWorkerCompleted;

            repeaterRestartConfirmBackgroundWorker.DoWork +=
                repeaterRestartConfirmBackgroundWorker_DoWork;

            repeaterRestartConfirmBackgroundWorker.RunWorkerCompleted +=
                repeaterBackgroundWorker_RunWorkerCompleted;

            repeaterStartConfirmBackgroundWorker.DoWork +=
                repeaterStartConfirmBackgroundWorker_DoWork;

            repeaterStartConfirmBackgroundWorker.RunWorkerCompleted +=
                repeaterBackgroundWorker_RunWorkerCompleted;

            repeaterStopConfirmBackgroundWorker.DoWork +=
                repeaterStopConfirmBackgroundWorker_DoWork;

            repeaterStopConfirmBackgroundWorker.RunWorkerCompleted +=
                repeaterBackgroundWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// Code to run after generated code.
        /// </summary>
        internal void PostInitializeComponent()
        {
            InitializeBackgroundWorkers();
            SetPropertiesOfDeviceToolStripMenuItemDropDown();
            SetIsLightThemeEnabledValueChangedEventArgs();                              //FIXME: See SetColorTheme.
            SetDeviceListModel();
            SetRepeaterDataModel();
            ModifyDropDownsBeforeInitialization();
            InitializeLists();
            SetInitialChanges();
            SetColorTheme();
            CloseAndSetPropertiesOfDeviceReloadAllToolStripMenuItem();
            CloseAndSetPropertiesOfDeviceAddToolStripMenuItemDropDown();
            CloseAndSetPropertiesOfDeviceRemoveToolStripMenuItemDropDown();
        }

        /// <summary>
        /// Save renderer to new parameter before making changes.
        /// </summary>
        internal void SaveInitialRenderer()
        {
            initialMenuStrip1Renderer = menuStrip1.Renderer;
        }

        /// <summary>
        /// Set color theme given dark mode is enabled or not.
        /// </summary>
        internal void SetColorTheme()
        {
            ToggleDarkModeRenderer();
            SetViewToggleDarkModeToolStripMenuItemProperties();
            FormColorUpdater.SetColorsOfConstructor(this);
            FormColorUpdater.SetColorsOfControlCollection(Controls);
            FormColorUpdater.SetColorsOfControlList(controlList);
            FormColorUpdater.SetColorsOfToolStripItemList(toolStripItemList);

            if (aboutForm != null)
            {
                aboutForm.SetColorTheme();
            }

            Invalidate();
        }

        /// <summary>
        /// Set initial changes to form and its children.
        /// </summary>
        internal void SetInitialChanges()
        {
            Text = AssemblyInformationAccessor.AssemblyTitle;
        }

        /// <summary>
        /// Set IsLightThemEnabledValueChanged event arguments.
        /// </summary>
        internal void SetIsLightThemeEnabledValueChangedEventArgs()
        {
            //GraphicsWindow.LightThemeValidator.IsLightThemeEnabledValueChanged +=       //NOTE: this will execute some of the code inside (inside SetColorTheme), but not all.
            //    (sender, propertyValueChangedEventArgs) =>
            //    {
            //        SetColorTheme();
            //    };

            IsLightThemeEnabledValueChanged +=
                (sender, propertyValueChangedEventArgs) =>
                {
                    SetColorTheme();                                                    //NOTE: this appears to execute all the code inside SetColorTheme.
                };
        }

        /// <summary>
        /// Set mouse properties of drop down tool strip menu item.
        /// </summary>
        /// <param name="toolStripMenuItem">The drop down tool strip menu item</param>
        internal void SetMousePropertiesOfToolStripMenuItemDropDown
            (ToolStripMenuItem toolStripMenuItem)
        {
            if (toolStripMenuItem is null)
            {
                return;
            }

            toolStripMenuItem.DropDown.MouseEnter += new EventHandler
                (SetAutoClosePropertyOfToolStripDropDown_MouseEnter);

            toolStripMenuItem.DropDown.MouseLeave += new EventHandler
                (SetAutoClosePropertyOfToolStripDropDown_MouseLeave);
        }

        /// <summary>
        /// Set viewToggleDarkModeToolStripMenuItem properties.
        /// </summary>
        internal void SetViewToggleDarkModeToolStripMenuItemProperties()
        {
            viewToggleDarkModeToolStripMenuItem.Checked = !IsLightThemeEnabled;
            viewToggleDarkModeToolStripMenuItem.Text = darkModeText;
        }

        /// <summary>
        /// Toggle the type of renderer given dark mode is enabled or not.
        /// </summary>
        internal void ToggleDarkModeRenderer()
        {
            if (IsLightThemeEnabled)
            {
                menuStrip1.RenderMode = ToolStripRenderMode.ManagerRenderMode;
                menuStrip1.Renderer = initialMenuStrip1Renderer;
            }
            else
            {
                menuStrip1.RenderMode = ToolStripRenderMode.Professional;
                menuStrip1.Renderer = new ToolStripProfessionalRenderer
                    (new DarkColorTable());

                //menuStrip1.Renderer = new ToolStripDarkRenderer();                    //NOTE: breaks context menu colors.
            }
        }

        #endregion

        #region Event handling

        /// <summary>
        /// Inform observers of LightThemeIsEnabled that its value has changed.
        /// </summary>
        internal void OnLightThemeIsEnabledValueChanged()
        {
            PropertyValueChangedEventArgs propertyValueChangedEventArgs =
                new PropertyValueChangedEventArgs(IsLightThemeEnabled);

            IsLightThemeEnabledValueChanged?.Invoke
                (this, propertyValueChangedEventArgs);
        }

        #endregion

        #region ToolStrip helper logic

        /// <summary>
        /// Verify the following object's parent has a parent. If true, the object's
        /// parent is the child of the first parent (menuStrip1). If false, the current
        /// object is the child of the first parent.
        /// </summary>
        /// <param name="accessibleObject">The accessible object</param>
        /// <returns>The first parent's child object</returns>
        internal AccessibleObject GetChildOfFirstParentAccessibleObject
            (AccessibleObject accessibleObject)
        {
            if (accessibleObject is null)
            {
                return null;
            }

            if (string.Equals(accessibleObject.Parent.Name, menuStrip1.Name))           //NOTE: if parent is menuStrip1, return the object.
            {
                return accessibleObject;
            }

            AccessibleObject result = GetChildOfFirstParentAccessibleObject
                (accessibleObject.Parent);

            if (result is null)                                                         //NOTE: if parent and/or grandparent doesn't exist, return the object.
            {
                return accessibleObject;
            }

            return result;
        }

        /// <summary>
        /// Get the name of the tool strip menu item which is the child of the first
        /// parent object (menuStrip1).
        /// </summary>
        /// <param name="toolStripMenuItem">The tool strip menu item</param>
        /// <returns></returns>
        internal string GetNameOfChildToFirstParentAccessibleObject
            (ToolStripMenuItem toolStripMenuItem)
        {
            if (toolStripMenuItem is null)
            {
                return null;
            }

            return GetChildOfFirstParentAccessibleObject
                (toolStripMenuItem.AccessibilityObject).Name;
        }

        /// <summary>
        /// Move all checked tool strip menu items to new tool strip item drop down.
        /// </summary>
        /// <param name="firstToolStripMenuItem">The first tool strip menu item</param>
        /// <param name="secondToolStripMenuItem">The second tool strip menu item
        /// </param>
        internal void MoveAllCheckedToolStripMenuItemDropDownToNewToolStripItemDropDown
            (ToolStripMenuItem firstToolStripMenuItem,
            ToolStripMenuItem secondToolStripMenuItem)
        {
            if (firstToolStripMenuItem is null
                || firstToolStripMenuItem.DropDownItems is null
                || firstToolStripMenuItem.DropDownItems.Count == 0
                || secondToolStripMenuItem is null
                || secondToolStripMenuItem.DropDownItems is null)
            {
                return;
            }

            List<ToolStripMenuItem> toolStripMenuItemList = firstToolStripMenuItem
                .DropDownItems.Cast<ToolStripMenuItem>()
                .Where(toolStripMenuItem => toolStripMenuItem.Checked).ToList();

            toolStripMenuItemList.ForEach(toolStripMenuItem =>
                {
                    MoveToolStripMenuItemDropDownToNewToolStripItemDropDown
                        (toolStripMenuItem, firstToolStripMenuItem,
                        secondToolStripMenuItem);
                });

            SetPropertiesOfToolStripMenuItemGivenItemDropDownIsEmptyOrNot
                (firstToolStripMenuItem);

            SetPropertiesOfToolStripMenuItemGivenItemDropDownIsEmptyOrNot
                (secondToolStripMenuItem);

            SortToolStripMenuItemDropDownByText(secondToolStripMenuItem);
        }

        /// <summary>
        /// Move tool strip menu item to new tool strip item drop down.
        /// </summary>
        /// <param name="thisToolStripMenuItem">The tool strip menu item to move</param>
        /// <param name="firstToolStripMenuItem">The first tool strip menu item</param>
        /// <param name="secondToolStripMenuItem">The second tool strip menu item
        /// </param>
        internal void MoveToolStripMenuItemDropDownToNewToolStripItemDropDown
            (ToolStripMenuItem thisToolStripMenuItem,
            ToolStripMenuItem firstToolStripMenuItem,
            ToolStripMenuItem secondToolStripMenuItem)
        {
            if (thisToolStripMenuItem is null
                || firstToolStripMenuItem is null
                || firstToolStripMenuItem.DropDownItems is null
                || firstToolStripMenuItem.DropDownItems.Count == 0
                || secondToolStripMenuItem is null
                || secondToolStripMenuItem.DropDownItems is null)
            {
                return;
            }

            thisToolStripMenuItem.Checked = false;
            firstToolStripMenuItem.DropDownItems.Remove(thisToolStripMenuItem);

            if (secondToolStripMenuItem.DropDownItems
                .Contains(thisToolStripMenuItem))
            {
                return;
            }

            secondToolStripMenuItem.DropDownItems.Add(thisToolStripMenuItem);
        }

        /// <summary>
        /// Sort tool strip menu item drop down by text.
        /// </summary>
        /// <param name="toolStripMenuItem">The tool strip menu item</param>
        internal void SortToolStripMenuItemDropDownByText
            (ToolStripMenuItem toolStripMenuItem)
        {
            if (toolStripMenuItem is null || toolStripMenuItem.DropDownItems.Count == 0)
            {
                return;
            }

            ToolStripMenuItem[] toolStripMenuItemArray = toolStripMenuItem
                .DropDownItems.Cast<ToolStripMenuItem>().OrderBy(x => x.Text)
                .ToArray();

            toolStripMenuItem.DropDownItems.Clear();
            toolStripMenuItem.DropDownItems.AddRange(toolStripMenuItemArray);
        }

        /// <summary>
        /// Recursively ShowDropDown for every parent of the current tool strip item.
        /// </summary>
        /// <param name="toolStripItem">The tool strip item</param>
        internal void RecursivelyShowDropDownForEveryParentToolStripItem
            (ToolStripItem toolStripItem)
        {
            if (toolStripItem is null)
            {
                return;
            }

            if (toolStripItem.OwnerItem != null)
            {
                RecursivelyShowDropDownForEveryParentToolStripItem
                    (toolStripItem.OwnerItem);
            }

            if (!(toolStripItem is ToolStripMenuItem))
            {
                return;
            }

            (toolStripItem as ToolStripMenuItem).ShowDropDown();
        }

        /// <summary>
        /// Recursively ShowDropDown for every parent of the current tool strip menu
        /// item.
        /// </summary>
        /// <param name="toolStripMenuItem">The tool strip menu item</param>
        internal void RecursivelyShowDropDownForEveryParentToolStripItem
            (ToolStripMenuItem toolStripMenuItem)
        {
            if (toolStripMenuItem is null)
            {
                return;
            }

            if (toolStripMenuItem.OwnerItem != null)
            {
                RecursivelyShowDropDownForEveryParentToolStripItem
                    (toolStripMenuItem.OwnerItem);
            }

            toolStripMenuItem.ShowDropDown();
        }

        /// <summary>
        /// Resets properties for selected nested tool strip item in tool strip menu
        /// item.
        /// </summary>
        /// <param name="mMDevice">The MMDevice</param>
        /// <param name="parentToolStripMenuItem">The referenced parent tool strip menu
        /// item</param>
        internal void ResetPropertiesForEachSelectedToolStripMenuItem(MMDevice mMDevice,
            ToolStripMenuItem parentToolStripMenuItem)
        {
            string isSelectedSuffix = " (Selected)";

            parentToolStripMenuItem.DropDownItems.Cast<ToolStripItem>().Where
                (toolStripItem => toolStripItem.ToolTipText != mMDevice.FriendlyName)
                .ToList().ForEach(toolStripItem =>
                    {
                        toolStripItem.Text = Regex.Replace(toolStripItem.Text,
                            isSelectedSuffix, string.Empty);
                    });
        }

        /// <summary>
        /// Set auto close property of tool strip drop down on mouse enter.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void SetAutoClosePropertyOfToolStripDropDown_MouseEnter(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripDropDown))
            {
                return;
            }

            (sender as ToolStripDropDown).AutoClose = false;
        }

        /// <summary>
        /// Set auto close property of tool strip drop down on mouse leave.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void SetAutoClosePropertyOfToolStripDropDown_MouseLeave(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripDropDown))
            {
                return;
            }

            (sender as ToolStripDropDown).AutoClose = true;
        }

        /// <summary>
        /// Set checked property of tool strip menu item.
        /// </summary>
        /// <param name="toolStripMenuItem">The tool strip menu item</param>
        /// <param name="isChecked">Is checked</param>
        internal void SetCheckedPropertyForEachToolStripMenuItem
            (ToolStripMenuItem toolStripMenuItem, bool isChecked)
        {
            if (toolStripMenuItem is null || toolStripMenuItem.DropDownItems.Count == 0)
            {
                return;
            }

            toolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>().ToList()
                .ForEach(thisToolStripMenuItem =>
                    {
                        thisToolStripMenuItem.Checked = isChecked;
                    });
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
            (ToolStripMenuItem parentToolStripMenuItem,
            ToolStripMenuItem childToolStripMenuItem)
        {
            if (!parentToolStripMenuItem.DropDownItems.Contains(childToolStripMenuItem))
            {
                return;
            }

            string isSelectedSuffix = " (Selected)";

            parentToolStripMenuItem.DropDownItems.Cast<ToolStripItem>().ToList()
                .ForEach(toolStripItem =>
                    {
                        if (toolStripItem != childToolStripMenuItem)
                        {
                            toolStripItem.Text = Regex.Replace(toolStripItem.Text,
                                isSelectedSuffix, string.Empty);
                        }
                        else
                        {
                            toolStripItem.Text += isSelectedSuffix;
                        }
                    });
        }

        /// <summary>
        /// Set properties of tool strip menu item given tool strip item drop down is
        /// empty or not.
        /// </summary>
        /// <param name="toolStripMenuItem">The tool strip menu item</param>
        internal void SetPropertiesOfToolStripMenuItemGivenItemDropDownIsEmptyOrNot
            (ToolStripMenuItem toolStripMenuItem)
        {
            bool isNotEmpty = toolStripMenuItem.DropDownItems.Count > 0;
            toolStripMenuItem.Enabled = isNotEmpty;

            if (isNotEmpty)
            {
                toolStripMenuItem.ToolTipText = string.Empty;
                return;
            }

            string name = GetNameOfChildToFirstParentAccessibleObject
                (toolStripMenuItem);

            if (string.Equals(name.ToLower(), "link"))
            {
                name = "device";
            }

            string items = "item(s)";

            if (!string.IsNullOrWhiteSpace(name))
            {
                items = $"{name.ToLower()}(s)";
            }

            toolStripMenuItem.ToolTipText = $"No {items} found.";
        }

        #endregion

        #region 1. File menu logic

        /// <summary>
        /// Click event logic for fileExitToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void fileExitToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            /*
             * TODO:
             *  -add logic to...
             *      -check if runtime data is saved to file.
             *      -warn user to save changes.
             *      -warn user that audio repeaters may exit at app shutdown.
             */

            Dispose();
            //Application.Exit();
        }

        /// <summary>
        /// Click event logic for fileOpenToolStripMenuItem.
        /// Get filename if dialog result is OK.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void fileOpenToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
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
        /// Create new repeater data model. Called whenever a file is opened or closed.
        /// </summary>
        internal void SetRepeaterDataModel()
        {
            repeaterDataModel = new RepeaterDataModel();
        }

        #endregion

        #region 2. Device menu logic

        /// <summary>
        /// Get the tool tip text for a device tool strip menu item.
        /// </summary>
        /// <param name="isBusy">Is busy</param>
        /// <param name="isListEmpty">Is list empty</param>
        /// <returns>The tool tip text</returns>
        internal string GetDeviceToolTipText(bool isBusy, bool isListEmpty)
        {
            if (isBusy)
            {
                return "Busy. Please wait...";
            }
            else if (isListEmpty)
            {
                return "No device(s) found.";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Close and set properties of deviceAddToolStripMenuItem drop downs.
        /// </summary>
        internal void CloseAndSetPropertiesOfDeviceAddToolStripMenuItemDropDown()
        {
            bool isBusy = deviceAddConfirmBackgroundWorker.IsBusy;

            if (isBusy)
            {
                deviceAddToolStripMenuItem.DropDown.Close();
            }

            bool isCheckedDeviceAddNameListEmpty =
                this.isCheckedDeviceAddNameListEmpty;

            bool isCheckedDeviceAddNameListFull = this.isCheckedDeviceAddNameListFull;
            bool isDeviceAddNameListEmpty = this.isDeviceAddNameListEmpty;
            bool isNotBusyAndListIsFull = !isBusy && isCheckedDeviceAddNameListFull;
            bool isNotBusyAndListIsNotEmpty = !isBusy && !isDeviceAddNameListEmpty;

            deviceAddConfirmToolStripMenuItem.Enabled =
                !isCheckedDeviceAddNameListEmpty;

            deviceAddSelectAllToolStripMenuItem.Checked = isNotBusyAndListIsFull;
            deviceAddSelectAllToolStripMenuItem.Enabled = isNotBusyAndListIsNotEmpty;

            deviceAddSelectWaveInToolStripMenuItem.Enabled = !isBusy
                && !isDeviceAddWaveInNameListEmpty;

            deviceAddSelectWaveOutToolStripMenuItem.Enabled = !isBusy
                && !isDeviceAddWaveOutNameListEmpty;

            string toolTipText = GetDeviceToolTipText(isBusy,
                isDeviceAddNameListEmpty);

            deviceAddConfirmToolStripMenuItem.ToolTipText = toolTipText;
            deviceAddSelectAllToolStripMenuItem.ToolTipText = toolTipText;
            deviceAddSelectToolStripMenuItem.ToolTipText = toolTipText;
        }

        /// <summary>
        /// Close and set properties of deviceReloadAllToolStripMenuItem.
        /// </summary>
        internal void CloseAndSetPropertiesOfDeviceReloadAllToolStripMenuItem()
        {
            bool isBusy = deviceAddConfirmBackgroundWorker.IsBusy;
            bool isDeviceAddNameListEmpty = this.isDeviceAddNameListEmpty;
            bool isDeviceRemoveNameListEmpty = this.isDeviceRemoveNameListEmpty;

            if (isBusy)
            {
                deviceReloadAllToolStripMenuItem.DropDown.Close();
            }

            deviceReloadAllToolStripMenuItem.Enabled = !isBusy
                && (!isDeviceAddNameListEmpty || !isDeviceRemoveNameListEmpty);

            deviceReloadAllToolStripMenuItem.ToolTipText = GetDeviceToolTipText(isBusy,
                isDeviceAddNameListEmpty && isDeviceRemoveNameListEmpty);
        }

        /// <summary>
        /// Close and set properties of deviceRemoveToolStripMenuItem drop downs.
        /// </summary>
        internal void CloseAndSetPropertiesOfDeviceRemoveToolStripMenuItemDropDown()
        {
            bool isBusy = deviceRemoveConfirmBackgroundWorker.IsBusy;

            if (isBusy)
            {
                deviceRemoveToolStripMenuItem.DropDown.Close();
            }

            bool isCheckedDeviceRemoveNameListEmpty =
                this.isCheckedDeviceRemoveNameListEmpty;

            bool isCheckedDeviceRemoveNameListFull =
                this.isCheckedDeviceRemoveNameListFull;

            bool isDeviceRemoveNameListEmpty = this.isDeviceRemoveNameListEmpty;
            bool isNotBusyAndListIsFull = !isBusy && isCheckedDeviceRemoveNameListFull;
            bool isNotBusyAndListIsNotEmpty = !isBusy && !isDeviceRemoveNameListEmpty;

            deviceRemoveConfirmToolStripMenuItem.Enabled =
                !isCheckedDeviceRemoveNameListEmpty;

            deviceRemoveSelectAllToolStripMenuItem.Checked = isNotBusyAndListIsFull;
            deviceRemoveSelectAllToolStripMenuItem.Enabled = isNotBusyAndListIsNotEmpty;

            deviceRemoveSelectAllLinkedToolStripMenuItem.Enabled =
                isNotBusyAndListIsNotEmpty;                                             //TODO: create a getter to determine if linked items exist.

            deviceRemoveSelectAllUnlinkedToolStripMenuItem.Enabled =
                isNotBusyAndListIsNotEmpty;                                             //TODO: create a getter to determine if unlinked items exist.

            deviceRemoveSelectWaveInToolStripMenuItem.Enabled = !isBusy
                && !this.isDeviceRemoveWaveInNameListEmpty;

            deviceRemoveSelectWaveOutToolStripMenuItem.Enabled = !isBusy
                && !this.isDeviceRemoveWaveOutNameListEmpty;

            string toolTipText = GetDeviceToolTipText(isBusy,
                isDeviceRemoveNameListEmpty);

            deviceRemoveConfirmToolStripMenuItem.ToolTipText = toolTipText;
            deviceRemoveSelectAllToolStripMenuItem.ToolTipText = toolTipText;
            deviceRemoveSelectAllLinkedToolStripMenuItem.ToolTipText = toolTipText;
            deviceRemoveSelectAllUnlinkedToolStripMenuItem.ToolTipText = toolTipText;
            deviceRemoveSelectToolStripMenuItem.ToolTipText = toolTipText;
        }

        /// <summary>
        /// Click event logic for deviceAddConfirmToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddConfirmToolStripMenuItem_Click(object sender,            //TODO: make called methods async?
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem)
                || isCheckedDeviceAddNameListEmpty
                || deviceAddConfirmBackgroundWorker.IsBusy
                || deviceReloadAllBackgroundWorker.IsBusy
                || deviceRemoveConfirmBackgroundWorker.IsBusy
                || !MessageBoxWrapper.ShowYesNoAndReturnTrueFalse(this,
                    "Add selected devices?"))
            {
                return;
            }

            deviceToolStripMenuItemDropDown_Action(deviceAddConfirmBackgroundWorker);
        }

        /// <summary>
        /// Click event logic for deviceAddSelectAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddSelectAllToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            SetCheckedPropertyForEachToolStripMenuItem
                (deviceAddSelectWaveInToolStripMenuItem,
                deviceAddSelectAllToolStripMenuItem.Checked);

            SetCheckedPropertyForEachToolStripMenuItem
                (deviceAddSelectWaveOutToolStripMenuItem,
                deviceAddSelectAllToolStripMenuItem.Checked);

            CloseAndSetPropertiesOfDeviceAddToolStripMenuItemDropDown();
            CloseAndSetPropertiesOfDeviceRemoveToolStripMenuItemDropDown();

            RecursivelyShowDropDownForEveryParentToolStripItem
                (deviceAddSelectAllToolStripMenuItem);
        }

        /// <summary>
        /// Confirm deviceAddSelectWaveIn or WaveOutToolStripMenuItem drop down.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceAddSelectToolStripMenuItemEnabled_Confirm(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            CloseAndSetPropertiesOfDeviceAddToolStripMenuItemDropDown();
            Invalidate();
        }

        /// <summary>
        /// Click event logic for deviceReloadAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceReloadAllToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem)
                || deviceReloadAllBackgroundWorker.IsBusy
                || deviceAddConfirmBackgroundWorker.IsBusy
                || deviceRemoveConfirmBackgroundWorker.IsBusy)
            {
                return;
            }

            string message = "Reload all devices will undo any unsaved changes to the" +
                " Grid and Graph, including devices, links, and repeaters.\nAre you " +
                "sure?";

            if (!MessageBoxWrapper.ShowYesNoAndReturnTrueFalse(this, message))
            {
                return;
            }

            deviceToolStripMenuItemDropDown_Action(deviceReloadAllBackgroundWorker);
        }

        /// <summary>
        /// Click event logic for deviceRemoveConfirmToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceRemoveConfirmToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem)
                || isCheckedDeviceRemoveNameListEmpty
                || deviceRemoveConfirmBackgroundWorker.IsBusy
                || deviceAddConfirmBackgroundWorker.IsBusy
                || deviceReloadAllBackgroundWorker.IsBusy
                || !MessageBoxWrapper.ShowYesNoAndReturnTrueFalse(this,
                    "Removed selected devices?"))
            {
                return;
            }

            deviceToolStripMenuItemDropDown_Action(deviceRemoveConfirmBackgroundWorker);
        }

        /// <summary>
        /// Click event logic for deviceRemoveSelectAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceRemoveSelectAllToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            SetCheckedPropertyForEachToolStripMenuItem
                (deviceRemoveSelectWaveInToolStripMenuItem,
                deviceRemoveSelectAllToolStripMenuItem.Checked);

            SetCheckedPropertyForEachToolStripMenuItem
                (deviceRemoveSelectWaveOutToolStripMenuItem,
                deviceRemoveSelectAllToolStripMenuItem.Checked);

            CloseAndSetPropertiesOfDeviceAddToolStripMenuItemDropDown();
            CloseAndSetPropertiesOfDeviceRemoveToolStripMenuItemDropDown();

            RecursivelyShowDropDownForEveryParentToolStripItem
                (deviceRemoveSelectAllToolStripMenuItem);
        }

        /// <summary>
        /// Confirm deviceRemoveSelectWaveIn or WaveOutToolStripMenuItem drop down.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void deviceRemoveSelectToolStripMenuItemEnabled_Confirm(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            CloseAndSetPropertiesOfDeviceRemoveToolStripMenuItemDropDown();
            Invalidate();
        }

        /// <summary>
        /// Action logic for deviceToolStripMenuItem drop down background worker.
        /// Unset properties of the parent tool strip menu item (device), set properties
        /// of each child, run the worker, then set properties of each child again, and
        /// reset properties of the parent.
        /// </summary>
        /// <param name="backgroundWorker">The background worker</param>
        internal void deviceToolStripMenuItemDropDown_Action
            (BackgroundWorker backgroundWorker)
        {
            if (backgroundWorker is null || backgroundWorker.IsBusy)
            {
                return;
            }

            LockDeviceReloadToolStripMenuItemAndDropDown();
            LockDeviceAddToolStripMenuItemAndDropDown();
            LockDeviceRemoveToolStripMenuItemAndDropDown();
            Invalidate();
            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Lock deviceAddToolStripMenuItem and drop down.
        /// </summary>
        internal void LockDeviceAddToolStripMenuItemAndDropDown()
        {
            deviceAddToolStripMenuItem.DropDown.Close();
            deviceAddConfirmToolStripMenuItem.Enabled = false;
            deviceAddSelectAllToolStripMenuItem.Enabled = false;
            deviceAddSelectWaveInToolStripMenuItem.Enabled = false;
            deviceAddSelectWaveOutToolStripMenuItem.Enabled = false;

            string toolTipText = GetDeviceToolTipText(true, false);
            deviceAddConfirmToolStripMenuItem.ToolTipText = toolTipText;
            deviceAddSelectAllToolStripMenuItem.ToolTipText = toolTipText;
            deviceAddSelectToolStripMenuItem.ToolTipText = toolTipText;
            deviceAddSelectWaveInToolStripMenuItem.ToolTipText = toolTipText;
            deviceAddSelectWaveOutToolStripMenuItem.ToolTipText = toolTipText;
        }

        /// <summary>
        /// Lock deviceReloadToolStripMenuItem and drop down.
        /// </summary>
        internal void LockDeviceReloadToolStripMenuItemAndDropDown()
        {
            deviceReloadAllToolStripMenuItem.Enabled = false;

            deviceReloadAllToolStripMenuItem.ToolTipText = GetDeviceToolTipText(true,
                false);
        }

        /// <summary>
        /// Lock deviceRemoveToolStripMenuItem and drop down.
        /// </summary>
        internal void LockDeviceRemoveToolStripMenuItemAndDropDown()
        {
            deviceRemoveToolStripMenuItem.DropDown.Close();
            deviceRemoveConfirmToolStripMenuItem.Enabled = false;
            deviceRemoveSelectAllToolStripMenuItem.Enabled = false;
            deviceRemoveSelectWaveInToolStripMenuItem.Enabled = false;
            deviceRemoveSelectWaveOutToolStripMenuItem.Enabled = false;

            string toolTipText = GetDeviceToolTipText(true, false);
            deviceRemoveConfirmToolStripMenuItem.ToolTipText = toolTipText;
            deviceRemoveSelectAllToolStripMenuItem.ToolTipText = toolTipText;
            deviceRemoveSelectToolStripMenuItem.ToolTipText = toolTipText;
            deviceRemoveSelectWaveInToolStripMenuItem.ToolTipText = toolTipText;
            deviceRemoveSelectWaveOutToolStripMenuItem.ToolTipText = toolTipText;
        }

        /// <summary>
        /// Set properties of deviceToolStripMenuItem drop downs.
        /// </summary>
        internal void SetPropertiesOfDeviceToolStripMenuItemDropDown()
        {
            deviceAddSelectWaveInToolStripMenuItem.Text = WaveInAsString;
            deviceAddSelectWaveOutToolStripMenuItem.Text = WaveOutAsString;
            deviceRemoveSelectWaveInToolStripMenuItem.Text = WaveInAsString;
            deviceRemoveSelectWaveOutToolStripMenuItem.Text = WaveOutAsString;

            helpAboutToolStripMenuItem.Text =
                $"About {Common.ApplicationNameAsAbbreviation}";

            linkAddSelectWaveInToolStripMenuItem.Text = WaveInAsString;
            linkAddSelectWaveOutToolStripMenuItem.Text = WaveOutAsString;
            linkRemoveSelectWaveInToolStripMenuItem.Text = WaveInAsString;
            linkRemoveSelectWaveOutToolStripMenuItem.Text = WaveOutAsString;

            viewToggleDarkModeToolStripMenuItem.Enabled = !DoForceColorTheme;
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
        /// Click event logic for linkAddConfirmToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkAddConfirmToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)                                                        //TODO: implement!
        {
            if (sender is null || !(sender is ToolStripMenuItem)
                || !MessageBoxWrapper.ShowYesNoAndReturnTrueFalse(this,
                    "Link selected devices?"))
            {
                return;
            }
        }

        /// <summary>
        /// Click event logic for linkAddSelectToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkAddSelectToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)                                                        //TODO: implement!
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }
        }

        /// <summary>
        /// Click event logic for linkAddSelectWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkAddSelectWaveInToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

            MMDevice mMDevice = deviceListModel.GetMMDevice(DataFlow.Capture,
                toolStripMenuItem.ToolTipText);

            if (mMDevice is null)
            {
                return;
            }

            inputDeviceControl = new DeviceControl(mMDevice);

            SetPropertiesOfSelectedToolStripMenuItem
                (linkAddSelectWaveInToolStripMenuItem, toolStripMenuItem);

            AddToRepeaterModel();
        }

        /// <summary>
        /// Click event logic for linkAddSelectWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkAddSelectWaveOutToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

            MMDevice mMDevice = deviceListModel.GetMMDevice(DataFlow.Render,
                toolStripMenuItem.ToolTipText);

            if (mMDevice is null)
            {
                return;
            }

            outputDeviceControl = new DeviceControl(mMDevice);

            SetPropertiesOfSelectedToolStripMenuItem
                (linkAddSelectWaveOutToolStripMenuItem, toolStripMenuItem);

            AddToRepeaterModel();
        }

        /// <summary>
        /// Click event logic for linkRemoveConfirmToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkRemoveConfirmToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)                                                        //TODO: implement!
        {
            if (sender is null || !(sender is ToolStripMenuItem)
                || !MessageBoxWrapper.ShowYesNoAndReturnTrueFalse(this,
                    "Unlink selected devices?"))
            {
                return;
            }
        }

        /// <summary>
        /// Click event logic for linkRemoveSelectAllToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkRemoveSelectAllToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)                                                        //TODO: implement!
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }
        }

        /// <summary>
        /// Click event logic for linkRemoveSelectToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkRemoveSelectToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)                                                        //TODO: implement!
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }
        }

        /// <summary>
        /// Click event logic for linkRemoveSelectWaveInToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkRemoveSelectWaveInToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

            MMDevice mMDevice = deviceListModel.GetMMDevice(DataFlow.Capture,
                toolStripMenuItem.ToolTipText);

            if (mMDevice is null)
            {
                return;
            }

            inputDeviceControl = new DeviceControl(mMDevice);

            SetPropertiesOfSelectedToolStripMenuItem
                (linkRemoveSelectWaveInToolStripMenuItem, toolStripMenuItem);

            RemoveFromRepeaterModel();
        }

        /// <summary>
        /// Click event logic for linkRemoveSelectWaveOutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void linkRemoveSelectWaveOutToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (sender is null || !(sender is ToolStripMenuItem))
            {
                return;
            }

            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;

            MMDevice mMDevice = deviceListModel.GetMMDevice(DataFlow.Render,
                toolStripMenuItem.ToolTipText);

            if (mMDevice is null)
            {
                return;
            }

            outputDeviceControl = new DeviceControl(mMDevice);

            SetPropertiesOfSelectedToolStripMenuItem
                (linkRemoveSelectWaveOutToolStripMenuItem, toolStripMenuItem);

            RemoveFromRepeaterModel();
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
                ResetPropertiesForEachSelectedToolStripMenuItem(deviceControl.MMDevice,
                    linkAddSelectWaveInToolStripMenuItem);
            }
            else
            {
                ResetPropertiesForEachSelectedToolStripMenuItem(deviceControl.MMDevice,
                    linkAddSelectWaveOutToolStripMenuItem);
            }
        }

        #endregion

        #region 4. Repeater menu logic

        /// <summary>
        /// Click event logic for repeaterRestartConfirmToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void repeaterRestartConfirmToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)                                                        //TODO: implement!
        {
            if (sender is null || !(sender is ToolStripMenuItem)
                || !MessageBoxWrapper.ShowYesNoAndReturnTrueFalse(this,
                    "Restart selected audio repeaters?"))
            {
                return;
            }
        }

        /// <summary>
        /// Click event logic for repeaterStartConfirmToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void repeaterStartConfirmToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)                                                        //TODO: implement!
        {
            if (sender is null || !(sender is ToolStripMenuItem)
                || !MessageBoxWrapper.ShowYesNoAndReturnTrueFalse(this,
                    "Start selected audio repeaters?"))
            {
                return;
            }
        }

        /// <summary>
        /// Click event logic for repeaterStopConfirmToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void repeaterStopConfirmToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)                                                        //TODO: implement!
        {
            if (sender is null || !(sender is ToolStripMenuItem)
                || !MessageBoxWrapper.ShowYesNoAndReturnTrueFalse(this,
                    "Stop selected audio repeaters?"))
            {
                return;
            }
        }

        #endregion

        #region 5. View menu logic

        /// <summary>
        /// Click event logic for viewToggleDarkModeToolStripMenuItem.
        /// Set the ToggleDarkModeText.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void viewToggleDarkModeToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            IsLightThemeEnabled = !viewToggleDarkModeToolStripMenuItem.Checked;
        }

        #endregion

        #region 6. Help menu logic

        /// <summary>
        /// Click event logic for helpAboutToolStripMenuItem.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArgs">The event arguments</param>
        internal void helpAboutToolStripMenuItem_Click(object sender,
            EventArgs eventArgs)
        {
            if (Application.OpenForms.OfType<AboutForm>().Count() > 0)
            {
                Application.OpenForms.OfType<AboutForm>().ToList()
                    .ForEach(aboutForm => aboutForm.Close());
            }

            aboutForm = new AboutForm();
            aboutForm.Show();
        }

        #endregion

        #region 2. Device background logic

        /// <summary>
        /// Action logic for deviceAddConfirm background worker.
        /// Return 2 if list is empty.
        /// Return 1 if moving between lists or drop downs fails.
        /// Return 0 if successful.
        /// </summary>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        /// <returns>The return value</returns>
        internal int deviceAddConfirmBackgroundWorker_Action
            (DoWorkEventArgs doWorkEventArgs)
        {
            if (isCheckedDeviceAddNameListEmpty)
            {
                doWorkEventArgs.Cancel = true;
                return 2;
            }

            try
            {
                MoveAllCheckedToolStripMenuItemDropDownToNewToolStripItemDropDown
                    (deviceAddSelectWaveInToolStripMenuItem,
                    deviceRemoveSelectWaveInToolStripMenuItem);

                MoveAllCheckedToolStripMenuItemDropDownToNewToolStripItemDropDown
                    (deviceAddSelectWaveOutToolStripMenuItem,
                     deviceRemoveSelectWaveOutToolStripMenuItem);

                SetDeviceListModelLists();
            }
            catch
            {
                doWorkEventArgs.Cancel = true;
                return 1;
            }

            return 0;
        }

        

        /// <summary>
        /// Action logic for deviceReloadAllConfirm background worker. If moving items
        /// in device list model raise exception, cancel and return 1. If moving checked
        /// items in item drop down raise exception, cancel and return 2.
        /// Else, return 0.
        /// </summary>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        /// <returns>The return value</returns>
        internal int deviceReloadAllBackgroundWorker_Action
            (DoWorkEventArgs doWorkEventArgs)
        {
            try
            {
                SetDeviceListModel();
                InitializeLists();
                CloseAndSetPropertiesOfDeviceAddToolStripMenuItemDropDown();
                CloseAndSetPropertiesOfDeviceRemoveToolStripMenuItemDropDown();
            }
            catch
            {
                doWorkEventArgs.Cancel = true;
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Action logic for deviceRemoveConfirm background worker.
        /// Return 2 if list is empty.
        /// Return 1 if moving between lists or drop downs fails.
        /// Return 0 if successful.
        /// </summary>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        /// <returns>The return value</returns>
        internal int deviceRemoveConfirmBackgroundWorker_Action
            (DoWorkEventArgs doWorkEventArgs)
        {
            if (isCheckedDeviceRemoveNameListEmpty)
            {
                doWorkEventArgs.Cancel = true;
                return 2;
            }

            try
            {
                MoveAllCheckedToolStripMenuItemDropDownToNewToolStripItemDropDown
                    (deviceRemoveSelectWaveInToolStripMenuItem,
                    deviceAddSelectWaveInToolStripMenuItem);

                MoveAllCheckedToolStripMenuItemDropDownToNewToolStripItemDropDown
                    (deviceRemoveSelectWaveOutToolStripMenuItem,
                     deviceAddSelectWaveOutToolStripMenuItem);

                SetDeviceListModelLists();
            }
            catch
            {
                doWorkEventArgs.Cancel = true;
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Work logic for deviceAddConfirm background worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        internal void deviceAddConfirmBackgroundWorker_DoWork(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            if (sender is null || !(sender is BackgroundWorker))
            {
                return;
            }

            doWorkEventArgs.Result = deviceAddConfirmBackgroundWorker_Action
                (doWorkEventArgs);
        }

        /// <summary>
        /// On complete logic for device background worker(s).
        /// worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="runWorkerCompletedEventArgs">The run worker completed event
        /// arguments</param>
        internal void deviceBackgroundWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (runWorkerCompletedEventArgs.Error != null)
            {
                MessageBoxWrapper.ShowError(this,
                    runWorkerCompletedEventArgs.Error.Message);                         //TODO: determine what is the contents of this. Should I create a message here, given return value?
                                                                                        //NOTE: undo all changes here from previous callstack operation?
            }

            CloseAndSetPropertiesOfDeviceReloadAllToolStripMenuItem();
            CloseAndSetPropertiesOfDeviceAddToolStripMenuItemDropDown();
            CloseAndSetPropertiesOfDeviceRemoveToolStripMenuItemDropDown();
            Invalidate();
        }

        /// <summary>
        /// Work logic for deviceReloadAll background worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        internal void deviceReloadAllBackgroundWorker_DoWork(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            if (sender is null || !(sender is BackgroundWorker))
            {
                return;
            }

            doWorkEventArgs.Result = deviceReloadAllBackgroundWorker_Action
                (doWorkEventArgs);
        }

        /// <summary>
        /// Work logic for deviceRemoveConfirm background worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        internal void deviceRemoveConfirmBackgroundWorker_DoWork(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            if (sender is null || !(sender is BackgroundWorker))
            {
                return;
            }

            doWorkEventArgs.Result = deviceRemoveConfirmBackgroundWorker_Action
                (doWorkEventArgs);
        }

        #endregion

        #region Link background logic

        /// <summary>
        /// Work logic for linkAddConfirm background worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        internal void linkAddConfirmBackgroundWorker_DoWork(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            if (sender is null || !(sender is BackgroundWorker))
            {
                return;
            }

            doWorkEventArgs.Result = 0;                                                 //TODO: implement here.
        }

        /// <summary>
        /// Work logic for linkRemoveConfirm background worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        internal void linkRemoveConfirmBackgroundWorker_DoWork(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            if (sender is null || !(sender is BackgroundWorker))
            {
                return;
            }

            doWorkEventArgs.Result = 0;                                                 //TODO: implement here.
        }

        /// <summary>
        /// On complete logic for link background worker(s).
        /// worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="runWorkerCompletedEventArgs">The run worker completed event
        /// arguments</param>
        internal void linkBackgroundWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (runWorkerCompletedEventArgs.Error != null)
            {
                MessageBoxWrapper.ShowError(this,
                    runWorkerCompletedEventArgs.Error.Message);                         //TODO: determine what is the contents of this. Should I create a message here, given return value?
                                                                                        //NOTE: undo all changes here from previous callstack operation?
            }

            //TODO: add logic here.
            Invalidate();
        }

        #endregion

        #region Repeater background logic

        /// <summary>
        /// Work logic for repeaterRestartConfirm background worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        internal void repeaterRestartConfirmBackgroundWorker_DoWork(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            if (sender is null || !(sender is BackgroundWorker))
            {
                return;
            }

            doWorkEventArgs.Result = 0;                                                 //TODO: implement here.
        }

        /// <summary>
        /// Work logic for repeaterStartConfirm background worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        internal void repeaterStartConfirmBackgroundWorker_DoWork(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            if (sender is null || !(sender is BackgroundWorker))
            {
                return;
            }

            doWorkEventArgs.Result = 0;                                                 //TODO: implement here.
        }

        /// <summary>
        /// Work logic for repeaterStopConfirm background worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doWorkEventArgs">The do work event arguments</param>
        internal void repeaterStopConfirmBackgroundWorker_DoWork(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            if (sender is null || !(sender is BackgroundWorker))
            {
                return;
            }

            doWorkEventArgs.Result = 0;                                                 //TODO: implement here.
        }

        /// <summary>
        /// On complete logic for repeater background worker(s).
        /// worker.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="runWorkerCompletedEventArgs">The run worker completed event
        /// arguments</param>
        internal void repeaterBackgroundWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            if (runWorkerCompletedEventArgs.Error != null)
            {
                MessageBoxWrapper.ShowError(this,
                    runWorkerCompletedEventArgs.Error.Message);                         //TODO: determine what is the contents of this. Should I create a message here, given return value?
                                                                                        //NOTE: undo all changes here from previous callstack operation?
            }

            //TODO: add logic here.
            Invalidate();
        }

        #endregion
    }

    /*
    * TODO:
    * -import/export system MMEnumeration to file.
    * -repeaters > disable item of and start repeaters whose device(s) are disabled or
    *   not present.
    * -check for existing running repeaters (should this app exit).
    * -run task of starting repeaters in another thread.
    * -check for glitches in repeaters.
    * -make suggestions if audio glitches occur.
    * -allow for enabling/disabling devices.
    * -click event to open active audio repeater.
    */
}