using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using VACARM.NET4.Models;
using VACARM.NET4.ViewModels;

namespace VACARM.NET4.Views
{
    public partial class MainForm : INotifyPropertyChanged
    {
        public bool IsDarkModeEnabledDuringRunTime
        {
            get
            {
                Program.IsDarkModeEnabledDuringRunTime =
                    toggleDarkModeToolStripMenuItem.Checked;
                return toggleDarkModeToolStripMenuItem.Checked;
            }
            set
            {
                Program.IsDarkModeEnabledDuringRunTime = value;
                toggleDarkModeToolStripMenuItem.Checked = value;
                OnPropertyChanged(nameof(IsDarkModeEnabledDuringRunTime));
            }
        }

        private string darkModeText
        {
            get
            {
                string text = "Dark Mode";

                if (IsDarkModeEnabledDuringRunTime)
                {
                    return $"Disable {text}";
                }

                return $"Enable {text}";
            }
        }

        private BackgroundWorker backgroundWorker1;
        private List<Control> controlList = new List<Control>();
        private List<ToolStripMenuItem> toolStripMenuItemList =
            new List<ToolStripMenuItem>();
        private Manina.Windows.Forms.TabControl tabControl1;
        private Manina.Windows.Forms.Tab gridTab;
        private Manina.Windows.Forms.Tab graphTab;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAllDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWaveInDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            addWaveOutDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultBitRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultBufferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultPrefillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultResyncAtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            defaultSamplingRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableWaveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableWaveOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableWaveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableWaveOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkWaveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkWaveOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newlinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            removeDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            removeAllDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            removeAllLinkedDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            removeAllUnlinkedDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeWaveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeWaveOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repeaterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveACopyAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleDarkModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkWaveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkWaveOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator linkToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator linkToolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator repeaterToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator repeaterToolStripSeparator2;
        private ToolStripRenderer initialMenuStrip1Renderer;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [ExcludeFromCodeCoverage]
        internal void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveACopyAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableWaveInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableWaveOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableWaveInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableWaveOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWaveInDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWaveOutDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAllDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeWaveInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeWaveOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllLinkedDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllUnlinkedDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newlinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkWaveInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkWaveOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.unlinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkWaveInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkWaveOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.defaultBitRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultBufferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultPrefillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultResyncAtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultSamplingRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeaterToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repeaterToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleDarkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new Manina.Windows.Forms.TabControl();
            this.gridTab = new Manina.Windows.Forms.Tab();
            this.gridTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.graphTab = new Manina.Windows.Forms.Tab();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.gridTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.deviceToolStripMenuItem,
            this.linkToolStripMenuItem,
            this.repeaterToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(622, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveACopyAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.fileToolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // saveACopyAsToolStripMenuItem
            // 
            this.saveACopyAsToolStripMenuItem.Name = "saveACopyAsToolStripMenuItem";
            this.saveACopyAsToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.saveACopyAsToolStripMenuItem.Text = "Save a Copy As...";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // fileToolStripSeparator1
            // 
            this.fileToolStripSeparator1.Name = "fileToolStripSeparator1";
            this.fileToolStripSeparator1.Size = new System.Drawing.Size(228, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadAllToolStripMenuItem,
            this.deviceToolStripSeparator1,
            this.disableToolStripMenuItem,
            this.enableToolStripMenuItem,
            this.deviceToolStripSeparator2,
            this.addDeviceToolStripMenuItem,
            this.addAllDevicesToolStripMenuItem,
            this.deviceToolStripSeparator3,
            this.removeDeviceToolStripMenuItem,
            this.removeAllDevicesToolStripMenuItem,
            this.removeAllLinkedDevicesToolStripMenuItem,
            this.removeAllUnlinkedDevicesToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.deviceToolStripMenuItem.Text = "Device";
            // 
            // reloadAllToolStripMenuItem
            // 
            this.reloadAllToolStripMenuItem.Name = "reloadAllToolStripMenuItem";
            this.reloadAllToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.reloadAllToolStripMenuItem.Tag = "";
            this.reloadAllToolStripMenuItem.Text = "Reload All";
            this.reloadAllToolStripMenuItem.Click += new System.EventHandler(this.reloadAllToolStripMenuItem_Click);
            // 
            // deviceToolStripSeparator1
            // 
            this.deviceToolStripSeparator1.Name = "deviceToolStripSeparator1";
            this.deviceToolStripSeparator1.Size = new System.Drawing.Size(227, 6);
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableWaveInToolStripMenuItem,
            this.disableWaveOutToolStripMenuItem});
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.disableToolStripMenuItem.Text = "Disable..";
            // 
            // disableWaveInToolStripMenuItem
            // 
            this.disableWaveInToolStripMenuItem.Name = "disableWaveInToolStripMenuItem";
            this.disableWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.disableWaveInToolStripMenuItem.Tag = "";
            this.disableWaveInToolStripMenuItem.Text = "Wave In";
            this.disableWaveInToolStripMenuItem.Click += new System.EventHandler(this.disableWaveInDeviceToolStripMenuItem_Click);
            // 
            // disableWaveOutToolStripMenuItem
            // 
            this.disableWaveOutToolStripMenuItem.Name = "disableWaveOutToolStripMenuItem";
            this.disableWaveOutToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.disableWaveOutToolStripMenuItem.Tag = "";
            this.disableWaveOutToolStripMenuItem.Text = "Wave Out";
            this.disableWaveOutToolStripMenuItem.Click += new System.EventHandler(this.disableWaveOutDeviceToolStripMenuItem_Click);
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableWaveInToolStripMenuItem,
            this.enableWaveOutToolStripMenuItem});
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.enableToolStripMenuItem.Tag = "";
            this.enableToolStripMenuItem.Text = "Enable..";
            // 
            // enableWaveInToolStripMenuItem
            // 
            this.enableWaveInToolStripMenuItem.Name = "enableWaveInToolStripMenuItem";
            this.enableWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.enableWaveInToolStripMenuItem.Tag = "";
            this.enableWaveInToolStripMenuItem.Text = "Wave In";
            this.enableWaveInToolStripMenuItem.Click += new System.EventHandler(this.enableWaveInDeviceToolStripMenuItem_Click);
            // 
            // enableWaveOutToolStripMenuItem
            // 
            this.enableWaveOutToolStripMenuItem.Name = "enableWaveOutToolStripMenuItem";
            this.enableWaveOutToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.enableWaveOutToolStripMenuItem.Tag = "";
            this.enableWaveOutToolStripMenuItem.Text = "Wave Out";
            this.enableWaveOutToolStripMenuItem.Click += new System.EventHandler(this.enableWaveOutDeviceToolStripMenuItem_Click);
            // 
            // deviceToolStripSeparator2
            // 
            this.deviceToolStripSeparator2.Name = "deviceToolStripSeparator2";
            this.deviceToolStripSeparator2.Size = new System.Drawing.Size(227, 6);
            // 
            // addDeviceToolStripMenuItem
            // 
            this.addDeviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addWaveInDeviceToolStripMenuItem,
            this.addWaveOutDeviceToolStripMenuItem});
            this.addDeviceToolStripMenuItem.Name = "addDeviceToolStripMenuItem";
            this.addDeviceToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.addDeviceToolStripMenuItem.Text = "Add...";
            // 
            // addWaveInDeviceToolStripMenuItem
            // 
            this.addWaveInDeviceToolStripMenuItem.Name = "addWaveInDeviceToolStripMenuItem";
            this.addWaveInDeviceToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.addWaveInDeviceToolStripMenuItem.Tag = "";
            this.addWaveInDeviceToolStripMenuItem.Text = "Wave In";
            // 
            // addWaveOutDeviceToolStripMenuItem
            // 
            this.addWaveOutDeviceToolStripMenuItem.Name = "addWaveOutDeviceToolStripMenuItem";
            this.addWaveOutDeviceToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.addWaveOutDeviceToolStripMenuItem.Tag = "";
            this.addWaveOutDeviceToolStripMenuItem.Text = "Wave Out";
            // 
            // addAllDevicesToolStripMenuItem
            // 
            this.addAllDevicesToolStripMenuItem.Name = "addAllDevicesToolStripMenuItem";
            this.addAllDevicesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.addAllDevicesToolStripMenuItem.Tag = "";
            this.addAllDevicesToolStripMenuItem.Text = "Add All";
            // 
            // deviceToolStripSeparator3
            // 
            this.deviceToolStripSeparator3.Name = "deviceToolStripSeparator3";
            this.deviceToolStripSeparator3.Size = new System.Drawing.Size(227, 6);
            // 
            // removeDeviceToolStripMenuItem
            // 
            this.removeDeviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeWaveInToolStripMenuItem,
            this.removeWaveOutToolStripMenuItem});
            this.removeDeviceToolStripMenuItem.Name = "removeDeviceToolStripMenuItem";
            this.removeDeviceToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.removeDeviceToolStripMenuItem.Text = "Remove...";
            // 
            // removeWaveInToolStripMenuItem
            // 
            this.removeWaveInToolStripMenuItem.Name = "removeWaveInToolStripMenuItem";
            this.removeWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.removeWaveInToolStripMenuItem.Tag = "";
            this.removeWaveInToolStripMenuItem.Text = "Wave In";
            this.removeWaveInToolStripMenuItem.Click += new System.EventHandler(this.removeWaveInDeviceToolStripMenuItem_Click);
            // 
            // removeWaveOutToolStripMenuItem
            // 
            this.removeWaveOutToolStripMenuItem.Name = "removeWaveOutToolStripMenuItem";
            this.removeWaveOutToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.removeWaveOutToolStripMenuItem.Tag = "";
            this.removeWaveOutToolStripMenuItem.Text = "Wave Out";
            this.removeWaveOutToolStripMenuItem.Click += new System.EventHandler(this.removeWaveOutDeviceToolStripMenuItem_Click);
            // 
            // removeAllDevicesToolStripMenuItem
            // 
            this.removeAllDevicesToolStripMenuItem.Name = "removeAllDevicesToolStripMenuItem";
            this.removeAllDevicesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.removeAllDevicesToolStripMenuItem.Tag = "";
            this.removeAllDevicesToolStripMenuItem.Text = "Remove All";
            // 
            // removeAllLinkedDevicesToolStripMenuItem
            // 
            this.removeAllLinkedDevicesToolStripMenuItem.Name = "removeAllLinkedDevicesToolStripMenuItem";
            this.removeAllLinkedDevicesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.removeAllLinkedDevicesToolStripMenuItem.Tag = "";
            this.removeAllLinkedDevicesToolStripMenuItem.Text = "Remove All Linked";
            // 
            // removeAllUnlinkedDevicesToolStripMenuItem
            // 
            this.removeAllUnlinkedDevicesToolStripMenuItem.Name = "removeAllUnlinkedDevicesToolStripMenuItem";
            this.removeAllUnlinkedDevicesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.removeAllUnlinkedDevicesToolStripMenuItem.Tag = "";
            this.removeAllUnlinkedDevicesToolStripMenuItem.Text = "Remove All Unlinked";
            // 
            // linkToolStripMenuItem
            // 
            this.linkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newlinkToolStripMenuItem,
            this.linkToolStripSeparator1,
            this.unlinkToolStripMenuItem,
            this.unlinkAllToolStripMenuItem,
            this.linkToolStripSeparator2,
            this.defaultBitRateToolStripMenuItem,
            this.defaultBufferToolStripMenuItem,
            this.defaultChannelsToolStripMenuItem,
            this.defaultPrefillToolStripMenuItem,
            this.defaultResyncAtToolStripMenuItem,
            this.defaultSamplingRateToolStripMenuItem});
            this.linkToolStripMenuItem.Name = "linkToolStripMenuItem";
            this.linkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
            this.linkToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.linkToolStripMenuItem.Text = "Link";
            // 
            // newlinkToolStripMenuItem
            // 
            this.newlinkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linkWaveInToolStripMenuItem,
            this.linkWaveOutToolStripMenuItem});
            this.newlinkToolStripMenuItem.Name = "newlinkToolStripMenuItem";
            this.newlinkToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.newlinkToolStripMenuItem.Text = "Link...";
            // 
            // linkWaveInToolStripMenuItem
            // 
            this.linkWaveInToolStripMenuItem.Name = "linkWaveInToolStripMenuItem";
            this.linkWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.linkWaveInToolStripMenuItem.Tag = "";
            this.linkWaveInToolStripMenuItem.Text = "Wave In";
            this.linkWaveInToolStripMenuItem.Click += new System.EventHandler(this.linkWaveInDeviceToolStripMenuItem_Click);
            // 
            // linkWaveOutToolStripMenuItem
            // 
            this.linkWaveOutToolStripMenuItem.Name = "linkWaveOutToolStripMenuItem";
            this.linkWaveOutToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.linkWaveOutToolStripMenuItem.Tag = "";
            this.linkWaveOutToolStripMenuItem.Text = "Wave Out";
            this.linkWaveOutToolStripMenuItem.Click += new System.EventHandler(this.linkWaveOutDeviceToolStripMenuItem_Click);
            // 
            // linkToolStripSeparator1
            // 
            this.linkToolStripSeparator1.Name = "linkToolStripSeparator1";
            this.linkToolStripSeparator1.Size = new System.Drawing.Size(282, 6);
            // 
            // unlinkToolStripMenuItem
            // 
            this.unlinkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unlinkWaveInToolStripMenuItem,
            this.unlinkWaveOutToolStripMenuItem});
            this.unlinkToolStripMenuItem.Name = "unlinkToolStripMenuItem";
            this.unlinkToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.unlinkToolStripMenuItem.Text = "Unlink...";
            // 
            // unlinkWaveInToolStripMenuItem
            // 
            this.unlinkWaveInToolStripMenuItem.Name = "unlinkWaveInToolStripMenuItem";
            this.unlinkWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.unlinkWaveInToolStripMenuItem.Tag = "";
            this.unlinkWaveInToolStripMenuItem.Text = "Wave In";
            this.unlinkWaveInToolStripMenuItem.Click += new System.EventHandler(this.unlinkWaveInDeviceToolStripMenuItem_Click);
            // 
            // unlinkWaveOutToolStripMenuItem
            // 
            this.unlinkWaveOutToolStripMenuItem.Name = "unlinkWaveOutToolStripMenuItem";
            this.unlinkWaveOutToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.unlinkWaveOutToolStripMenuItem.Tag = "";
            this.unlinkWaveOutToolStripMenuItem.Text = "Wave Out";
            this.unlinkWaveOutToolStripMenuItem.Click += new System.EventHandler(this.unlinkWaveOutDeviceToolStripMenuItem_Click);
            // 
            // unlinkAllToolStripMenuItem
            // 
            this.unlinkAllToolStripMenuItem.Name = "unlinkAllToolStripMenuItem";
            this.unlinkAllToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.unlinkAllToolStripMenuItem.Tag = "";
            this.unlinkAllToolStripMenuItem.Text = "Unlink All";
            // 
            // linkToolStripSeparator2
            // 
            this.linkToolStripSeparator2.Name = "linkToolStripSeparator2";
            this.linkToolStripSeparator2.Size = new System.Drawing.Size(282, 6);
            // 
            // defaultBitRateToolStripMenuItem
            // 
            this.defaultBitRateToolStripMenuItem.Name = "defaultBitRateToolStripMenuItem";
            this.defaultBitRateToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultBitRateToolStripMenuItem.Tag = "";
            this.defaultBitRateToolStripMenuItem.Text = "Default Bit Rate (Bit/Sample)";
            // 
            // defaultBufferToolStripMenuItem
            // 
            this.defaultBufferToolStripMenuItem.Name = "defaultBufferToolStripMenuItem";
            this.defaultBufferToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultBufferToolStripMenuItem.Tag = "";
            this.defaultBufferToolStripMenuItem.Text = "Default Buffer (ms)";
            // 
            // defaultChannelsToolStripMenuItem
            // 
            this.defaultChannelsToolStripMenuItem.Name = "defaultChannelsToolStripMenuItem";
            this.defaultChannelsToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultChannelsToolStripMenuItem.Tag = "";
            this.defaultChannelsToolStripMenuItem.Text = "Default Channels";
            // 
            // defaultPrefillToolStripMenuItem
            // 
            this.defaultPrefillToolStripMenuItem.Name = "defaultPrefillToolStripMenuItem";
            this.defaultPrefillToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultPrefillToolStripMenuItem.Tag = "";
            this.defaultPrefillToolStripMenuItem.Text = "Default Prefill (%)";
            // 
            // defaultResyncAtToolStripMenuItem
            // 
            this.defaultResyncAtToolStripMenuItem.Name = "defaultResyncAtToolStripMenuItem";
            this.defaultResyncAtToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultResyncAtToolStripMenuItem.Tag = "";
            this.defaultResyncAtToolStripMenuItem.Text = "Default Resync At (%)";
            // 
            // defaultSamplingRateToolStripMenuItem
            // 
            this.defaultSamplingRateToolStripMenuItem.Name = "defaultSamplingRateToolStripMenuItem";
            this.defaultSamplingRateToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultSamplingRateToolStripMenuItem.Tag = "";
            this.defaultSamplingRateToolStripMenuItem.Text = "Default Sampling Rate (Hz)";
            // 
            // repeaterToolStripMenuItem
            // 
            this.repeaterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.restartAllToolStripMenuItem,
            this.repeaterToolStripSeparator1,
            this.startToolStripMenuItem,
            this.startAllToolStripMenuItem,
            this.repeaterToolStripSeparator2,
            this.stopToolStripMenuItem,
            this.stopAllToolStripMenuItem});
            this.repeaterToolStripMenuItem.Name = "repeaterToolStripMenuItem";
            this.repeaterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.repeaterToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.repeaterToolStripMenuItem.Text = "Repeater";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.restartToolStripMenuItem.Tag = "";
            this.restartToolStripMenuItem.Text = "Restart...";
            // 
            // restartAllToolStripMenuItem
            // 
            this.restartAllToolStripMenuItem.Name = "restartAllToolStripMenuItem";
            this.restartAllToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.restartAllToolStripMenuItem.Tag = "";
            this.restartAllToolStripMenuItem.Text = "Restart All";
            // 
            // repeaterToolStripSeparator1
            // 
            this.repeaterToolStripSeparator1.Name = "repeaterToolStripSeparator1";
            this.repeaterToolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.CheckOnClick = true;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.startToolStripMenuItem.Tag = "";
            this.startToolStripMenuItem.Text = "Start...";
            // 
            // startAllToolStripMenuItem
            // 
            this.startAllToolStripMenuItem.CheckOnClick = true;
            this.startAllToolStripMenuItem.Name = "startAllToolStripMenuItem";
            this.startAllToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.startAllToolStripMenuItem.Tag = "";
            this.startAllToolStripMenuItem.Text = "Start All";
            // 
            // repeaterToolStripSeparator2
            // 
            this.repeaterToolStripSeparator2.Name = "repeaterToolStripSeparator2";
            this.repeaterToolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.CheckOnClick = true;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.stopToolStripMenuItem.Tag = "";
            this.stopToolStripMenuItem.Text = "Stop...";
            // 
            // stopAllToolStripMenuItem
            // 
            this.stopAllToolStripMenuItem.CheckOnClick = true;
            this.stopAllToolStripMenuItem.Name = "stopAllToolStripMenuItem";
            this.stopAllToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.stopAllToolStripMenuItem.Tag = "";
            this.stopAllToolStripMenuItem.Text = "Stop All";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleDarkModeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toggleDarkModeToolStripMenuItem
            // 
            this.toggleDarkModeToolStripMenuItem.CheckOnClick = true;
            this.toggleDarkModeToolStripMenuItem.Name = "toggleDarkModeToolStripMenuItem";
            this.toggleDarkModeToolStripMenuItem.Size = new System.Drawing.Size(83, 26);
            this.toggleDarkModeToolStripMenuItem.Click += new System.EventHandler(this.ToggleDarkModeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabControl1.ContentAlignment = Manina.Windows.Forms.Alignment.Center;
            this.tabControl1.Controls.Add(this.gridTab);
            this.tabControl1.Controls.Add(this.graphTab);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.MinimumSize = new System.Drawing.Size(622, 411);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(622, 411);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Tabs.Add(this.gridTab);
            this.tabControl1.Tabs.Add(this.graphTab);
            this.tabControl1.TabSize = new System.Drawing.Size(75, 25);
            this.tabControl1.TabSizing = Manina.Windows.Forms.TabSizing.Fixed;
            // 
            // gridTab
            // 
            this.gridTab.Controls.Add(this.gridTableLayoutPanel);
            this.gridTab.Location = new System.Drawing.Point(1, 25);
            this.gridTab.Name = "gridTab";
            this.gridTab.Size = new System.Drawing.Size(620, 385);
            this.gridTab.Text = "Grid";
            // 
            // gridTableLayoutPanel
            // 
            this.gridTableLayoutPanel.AutoScroll = true;
            this.gridTableLayoutPanel.AutoSize = true;
            this.gridTableLayoutPanel.BackColor = System.Drawing.SystemColors.Highlight;
            this.gridTableLayoutPanel.ColumnCount = 2;
            this.gridTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.gridTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.gridTableLayoutPanel.Name = "gridTableLayoutPanel";
            this.gridTableLayoutPanel.RowCount = 2;
            this.gridTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gridTableLayoutPanel.Size = new System.Drawing.Size(620, 385);
            this.gridTableLayoutPanel.TabIndex = 0;
            this.gridTableLayoutPanel.TabStop = true;
            // 
            // graphTab
            // 
            this.graphTab.Location = new System.Drawing.Point(0, 0);
            this.graphTab.Name = "graphTab";
            this.graphTab.Size = new System.Drawing.Size(0, 0);
            this.graphTab.Text = "Graph";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.gridTab.ResumeLayout(false);
            this.gridTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        /// Add all menu items to list.
        /// </summary>
        internal void InitializeMenuItemsList()
        {
            toolStripMenuItemList.Clear();
            toolStripMenuItemList.Add(aboutToolStripMenuItem);
            toolStripMenuItemList.Add(addAllDevicesToolStripMenuItem);
            toolStripMenuItemList.Add(addDeviceToolStripMenuItem);
            toolStripMenuItemList.Add(addWaveInDeviceToolStripMenuItem);
            toolStripMenuItemList.Add(addWaveOutDeviceToolStripMenuItem);
            toolStripMenuItemList.Add(closeToolStripMenuItem);
            toolStripMenuItemList.Add(disableToolStripMenuItem);
            toolStripMenuItemList.Add(defaultBitRateToolStripMenuItem);
            toolStripMenuItemList.Add(defaultBufferToolStripMenuItem);
            toolStripMenuItemList.Add(defaultChannelsToolStripMenuItem);
            toolStripMenuItemList.Add(defaultPrefillToolStripMenuItem);
            toolStripMenuItemList.Add(defaultResyncAtToolStripMenuItem);
            toolStripMenuItemList.Add(defaultSamplingRateToolStripMenuItem);
            toolStripMenuItemList.Add(exitToolStripMenuItem);
            toolStripMenuItemList.Add(enableToolStripMenuItem);
            toolStripMenuItemList.Add(fileToolStripMenuItem);
            toolStripMenuItemList.Add(helpToolStripMenuItem);
            toolStripMenuItemList.Add(linkToolStripMenuItem);
            toolStripMenuItemList.Add(linkWaveInToolStripMenuItem);
            toolStripMenuItemList.Add(linkWaveOutToolStripMenuItem);
            toolStripMenuItemList.Add(newlinkToolStripMenuItem);
            toolStripMenuItemList.Add(newToolStripMenuItem);
            toolStripMenuItemList.Add(openToolStripMenuItem);
            toolStripMenuItemList.Add(reloadAllToolStripMenuItem);
            toolStripMenuItemList.Add(removeAllDevicesToolStripMenuItem);
            toolStripMenuItemList.Add(removeAllLinkedDevicesToolStripMenuItem);
            toolStripMenuItemList.Add(removeAllUnlinkedDevicesToolStripMenuItem);
            toolStripMenuItemList.Add(removeDeviceToolStripMenuItem);
            toolStripMenuItemList.Add(removeWaveOutToolStripMenuItem);
            toolStripMenuItemList.Add(repeaterToolStripMenuItem);
            toolStripMenuItemList.Add(restartAllToolStripMenuItem);
            toolStripMenuItemList.Add(restartToolStripMenuItem);
            toolStripMenuItemList.Add(saveACopyAsToolStripMenuItem);
            toolStripMenuItemList.Add(saveAsToolStripMenuItem);
            toolStripMenuItemList.Add(saveToolStripMenuItem);
            toolStripMenuItemList.Add(startAllToolStripMenuItem);
            toolStripMenuItemList.Add(startToolStripMenuItem);
            toolStripMenuItemList.Add(stopAllToolStripMenuItem);
            toolStripMenuItemList.Add(stopToolStripMenuItem);
            toolStripMenuItemList.Add(toggleDarkModeToolStripMenuItem);
            toolStripMenuItemList.Add(enableWaveInToolStripMenuItem);
            toolStripMenuItemList.Add(enableWaveOutToolStripMenuItem);
            toolStripMenuItemList.Add(unlinkAllToolStripMenuItem);
            toolStripMenuItemList.Add(unlinkToolStripMenuItem);
            toolStripMenuItemList.Add(unlinkWaveInToolStripMenuItem);
            toolStripMenuItemList.Add(unlinkWaveOutToolStripMenuItem);
            toolStripMenuItemList.Add(viewToolStripMenuItem);
            toolStripMenuItemList.Add(removeWaveInToolStripMenuItem);
        }

        /// <summary>
        /// Initialize a device tool strip menu item drop down collection by parsing the
        /// related device list.
        /// </summary>
        /// <param name="refToolStripMenuItem">The device tool strip menu item</param>
        /// <param name="mMDeviceList">The device list</param>
        internal void InitializeDeviceDropDownCollection
            (ref ToolStripMenuItem refToolStripMenuItem, List<MMDevice> mMDeviceList)
        {
            refToolStripMenuItem.DropDownItems.Clear();
            List<ToolStripMenuItem> toolStripMenuItemList = 
                new List<ToolStripMenuItem>();

            foreach (MMDevice mMDevice in mMDeviceList.ToList())
            {
                if (mMDevice.State == DeviceState.NotPresent)
                {
                    continue;
                }

                bool itemIsEnabled = mMDevice.State != DeviceState.Disabled;
                string text = $"{mMDevice.FriendlyName} ";

                if (itemIsEnabled)
                {
                    text += "(Enabled)";
                }
                else
                {
                    text += "(Disabled)";
                }

                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
                {
                    BackColor = FormColorUpdater.BackColor,
                    ForeColor = FormColorUpdater.ForeColor,
                    Enabled = itemIsEnabled,
                    Text = text,
                    ToolTipText = mMDevice.DeviceFriendlyName,
                };

                toolStripMenuItem.Click += new System.EventHandler
                    (addWaveInDeviceToolStripMenuItem_Click);
                toolStripMenuItemList.Add(toolStripMenuItem);
            }

            toolStripMenuItemList.ForEach(toolStripMenuItem =>
            {
                string prefix = 
                    $"{toolStripMenuItemList.IndexOf(toolStripMenuItem).ToString()}. ";
                toolStripMenuItem.Text = string.Format
                    ("{0,4} {1}", prefix, toolStripMenuItem.Text);
            });

            refToolStripMenuItem.DropDownItems.AddRange
                (toolStripMenuItemList.ToArray());
            string toolTipTextWhereDropDownItemsIsEmpty = "No devices found.";

            if (refToolStripMenuItem.DropDownItems.Count != 0)
            {
                return;
            }

            refToolStripMenuItem.ToolTipText = toolTipTextWhereDropDownItemsIsEmpty;
        }

        /// <summary>
        /// Logs event when property has changed.
        /// </summary>
        /// <param name="propertyName">The property name</param>
        internal void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Initialize all lists.
        /// </summary>
        internal void InitializeLists()
        {
            //TODO: add async task, pop up window to prompt "Loading sound devices."

            InitializeDeviceDropDownCollection(ref addWaveInDeviceToolStripMenuItem, 
                deviceList.AllWaveInDeviceList);
            InitializeDeviceDropDownCollection(ref addWaveOutDeviceToolStripMenuItem, 
                deviceList.AllWaveOutDeviceList);
            InitializeControlsList();
            InitializeMenuItemsList();
        }

        /// <summary>
        /// Code to run after generated code.
        /// </summary>
        internal void PostInitializeComponent()
        {
            InitializeLists();
            SetInitialChanges();
            SetColorTheme();
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
            FormColorUpdater.SetColorsOfConstructor(this);
            FormColorUpdater.SetColorsOfControlCollection(Controls);
            FormColorUpdater.SetColorsOfControlList(controlList);
            FormColorUpdater.SetColorsOfToolStripMenuItemList(toolStripMenuItemList);
            toggleDarkModeToolStripMenuItem.Text = darkModeText;
            Invalidate();
        }

        /// <summary>
        /// Set initial changes to form.
        /// </summary>
        internal void SetInitialChanges()
        {
            Text = AssemblyInformationAccessor.AssemblyTitle;
            IsDarkModeEnabledDuringRunTime = Program.IsDarkModeEnabledBeforeRunTime;
        }

        /// <summary>
        /// Toggle the type of renderer given dark mode is enabled or not.
        /// </summary>
        internal void ToggleDarkModeRenderer()
        {
            if (IsDarkModeEnabledDuringRunTime)
            {
                menuStrip1.RenderMode = ToolStripRenderMode.Professional;
                menuStrip1.Renderer = new ToolStripProfessionalRenderer
                    (new DarkColorTable());
                //menuStrip1.Renderer = new ToolStripDarkRenderer();                    //NOTE: breaks context menu colors.
            }
            else
            {
                menuStrip1.RenderMode = ToolStripRenderMode.ManagerRenderMode;
                menuStrip1.Renderer = initialMenuStrip1Renderer;
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="doDispose">true if managed resources should be disposed; 
        /// otherwise, false.</param>
        protected override void Dispose(bool doDispose)
        {
            if (doDispose && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(doDispose);
        }

        private TableLayoutPanel gridTableLayoutPanel;
    }
}