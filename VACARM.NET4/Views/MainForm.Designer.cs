using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace VACARM.NET4.Views
{
    public partial class MainForm
    {
        public bool IsDarkModeEnabledDuringRunTime
        {
            get
            {
                return this.toggleDarkModeToolStripMenuItem.Checked;
            }
            set
            {
                Program.IsDarkModeEnabledDuringRunTime = value;
                this.toggleDarkModeToolStripMenuItem.Checked = value;
            }
        }

        private ToolStripProfessionalRenderer initialMenuStrip1ProfessionalRenderer;
        private ToolStripRenderMode initialMenuStrip1RenderMode;
        private ToolStripRenderer initialMenuStrip1Renderer;

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

        private List<ToolStripMenuItem> toolStripMenuItemList =
            new List<ToolStripMenuItem>();

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
            removeWaveInDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            removeAllDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            removeAllLinkedDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            removeAllUnlinkedDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem
            removeWaveOutDeviceToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem unlinkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkWaveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unlinkWaveOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator linkToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator linkToolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator repeaterToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator repeaterToolStripSeparator2;

        private readonly string applicationName = "VAC Audio Repeater Manager";
        private readonly Color darkBackColor = Color.FromArgb(60, 63, 65);
        private readonly Color lightBackColor = Color.White;
        private readonly Color darkTextColor = Color.White;
        private readonly Color lightTextColor = Color.Black;

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
            this.saveACopyAsToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripSeparator1 =
                new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadAllToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripSeparator1 =
                new System.Windows.Forms.ToolStripSeparator();
            this.disableToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.disableWaveInToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.disableWaveOutToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripSeparator2 =
                new System.Windows.Forms.ToolStripSeparator();
            this.addDeviceToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.addWaveInDeviceToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.addWaveOutDeviceToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.addAllDevicesToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripSeparator3 =
                new System.Windows.Forms.ToolStripSeparator();
            this.removeWaveInDeviceToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.waveInToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.removeWaveOutDeviceToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllDevicesToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllLinkedDevicesToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllUnlinkedDevicesToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.newlinkToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.linkWaveInToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.linkWaveOutToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripSeparator1 =
                new System.Windows.Forms.ToolStripSeparator();
            this.unlinkToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkWaveInToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkWaveOutToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.unlinkAllToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripSeparator2 =
                new System.Windows.Forms.ToolStripSeparator();
            this.defaultBitRateToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.defaultBufferToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.defaultChannelsToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.defaultPrefillToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.defaultResyncAtToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.defaultSamplingRateToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.repeaterToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.restartAllToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.repeaterToolStripSeparator1 =
                new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAllToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.repeaterToolStripSeparator2 =
                new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleDarkModeToolStripMenuItem =
                new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1012, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveACopyAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.fileToolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.toolStripMenuItemList.Add(fileToolStripMenuItem);

            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.newToolStripMenuItem.Text = "New";
            this.toolStripMenuItemList.Add(newToolStripMenuItem);

            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler
                (this.openToolStripMenuItem_Click);
            this.toolStripMenuItemList.Add(openToolStripMenuItem);

            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.toolStripMenuItemList.Add(saveToolStripMenuItem);

            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.toolStripMenuItemList.Add(saveAsToolStripMenuItem);

            // 
            // saveACopyAsToolStripMenuItem
            // 
            this.saveACopyAsToolStripMenuItem.Name = "saveACopyAsToolStripMenuItem";
            this.saveACopyAsToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.saveACopyAsToolStripMenuItem.Text = "Save a Copy As...";
            this.toolStripMenuItemList.Add(saveACopyAsToolStripMenuItem);

            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.closeToolStripMenuItem.Text = "Close";
            this.toolStripMenuItemList.Add(closeToolStripMenuItem);

            // 
            // fileToolStripSeparator1
            // 
            this.fileToolStripSeparator1.Name = "fileToolStripSeparator1";
            this.fileToolStripSeparator1.Size = new System.Drawing.Size(199, 6);

            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler
                (this.exitToolStripMenuItem_Click);
            this.toolStripMenuItemList.Add(exitToolStripMenuItem);

            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.reloadAllToolStripMenuItem,
            this.deviceToolStripSeparator1,
            this.disableToolStripMenuItem,
            this.enableToolStripMenuItem,
            this.deviceToolStripSeparator2,
            this.addDeviceToolStripMenuItem,
            this.addAllDevicesToolStripMenuItem,
            this.deviceToolStripSeparator3,
            this.removeWaveInDeviceToolStripMenuItem,
            this.removeAllDevicesToolStripMenuItem,
            this.removeAllLinkedDevicesToolStripMenuItem,
            this.removeAllUnlinkedDevicesToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.deviceToolStripMenuItem.Text = "Device";
            this.deviceToolStripMenuItem.Click += new System.EventHandler
                (this.deviceToolStripMenuItem_Click);
            this.toolStripMenuItemList.Add(deviceToolStripMenuItem);

            // 
            // reloadAllToolStripMenuItem
            // 
            this.reloadAllToolStripMenuItem.Name = "reloadAllToolStripMenuItem";
            this.reloadAllToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.reloadAllToolStripMenuItem.Tag = "";
            this.reloadAllToolStripMenuItem.Text = "Reload All";
            this.toolStripMenuItemList.Add(reloadAllToolStripMenuItem);

            // 
            // deviceToolStripSeparator1
            // 
            this.deviceToolStripSeparator1.Name = "deviceToolStripSeparator1";
            this.deviceToolStripSeparator1.Size = new System.Drawing.Size(227, 6);

            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.disableWaveInToolStripMenuItem,
            this.disableWaveOutToolStripMenuItem});
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.disableToolStripMenuItem.Text = "Disable..";
            this.toolStripMenuItemList.Add(disableToolStripMenuItem);

            // 
            // disableWaveInToolStripMenuItem
            // 
            this.disableWaveInToolStripMenuItem.Name = "disableWaveInToolStripMenuItem";
            this.disableWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.disableWaveInToolStripMenuItem.Tag = "";
            this.disableWaveInToolStripMenuItem.Text = "Wave In";
            this.toolStripMenuItemList.Add(disableWaveInToolStripMenuItem);

            // 
            // disableWaveOutToolStripMenuItem
            // 
            this.disableWaveOutToolStripMenuItem.Name =
                "disableWaveOutToolStripMenuItem";
            this.disableWaveOutToolStripMenuItem.Size =
                new System.Drawing.Size(156, 26);
            this.disableWaveOutToolStripMenuItem.Tag = "";
            this.disableWaveOutToolStripMenuItem.Text = "Wave Out";
            this.toolStripMenuItemList.Add(disableWaveOutToolStripMenuItem);

            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem15,
            this.toolStripMenuItem16});
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.enableToolStripMenuItem.Tag = "";
            this.enableToolStripMenuItem.Text = "Enable..";
            this.toolStripMenuItemList.Add(enableToolStripMenuItem);

            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(156, 26);
            this.toolStripMenuItem15.Tag = "";
            this.toolStripMenuItem15.Text = "Wave In";
            this.toolStripMenuItemList.Add(toolStripMenuItem16);

            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(156, 26);
            this.toolStripMenuItem16.Tag = "";
            this.toolStripMenuItem16.Text = "Wave Out";
            this.toolStripMenuItemList.Add(toolStripMenuItem16);

            // 
            // deviceToolStripSeparator2
            // 
            this.deviceToolStripSeparator2.Name = "deviceToolStripSeparator2";
            this.deviceToolStripSeparator2.Size = new System.Drawing.Size(227, 6);

            // 
            // addDeviceToolStripMenuItem
            // 
            this.addDeviceToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.addWaveInDeviceToolStripMenuItem,
            this.addWaveOutDeviceToolStripMenuItem});
            this.addDeviceToolStripMenuItem.Name = "addDeviceToolStripMenuItem";
            this.addDeviceToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.addDeviceToolStripMenuItem.Text = "Add...";
            this.toolStripMenuItemList.Add(addDeviceToolStripMenuItem);

            // 
            // addWaveInDeviceToolStripMenuItem
            // 
            this.addWaveInDeviceToolStripMenuItem.Name =
                "addWaveInDeviceToolStripMenuItem";
            this.addWaveInDeviceToolStripMenuItem.Size =
                new System.Drawing.Size(156, 26);
            this.addWaveInDeviceToolStripMenuItem.Tag = "";
            this.addWaveInDeviceToolStripMenuItem.Text = "Wave In";
            this.toolStripMenuItemList.Add(addWaveInDeviceToolStripMenuItem);

            // 
            // addWaveOutDeviceToolStripMenuItem
            // 
            this.addWaveOutDeviceToolStripMenuItem.Name =
                "addWaveOutDeviceToolStripMenuItem";
            this.addWaveOutDeviceToolStripMenuItem.Size =
                new System.Drawing.Size(156, 26);
            this.addWaveOutDeviceToolStripMenuItem.Tag = "";
            this.addWaveOutDeviceToolStripMenuItem.Text = "Wave Out";
            this.toolStripMenuItemList.Add(addWaveOutDeviceToolStripMenuItem);

            // 
            // addAllDevicesToolStripMenuItem
            // 
            this.addAllDevicesToolStripMenuItem.Name = "addAllDevicesToolStripMenuItem";
            this.addAllDevicesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.addAllDevicesToolStripMenuItem.Tag = "";
            this.addAllDevicesToolStripMenuItem.Text = "Add All";
            this.toolStripMenuItemList.Add(addAllDevicesToolStripMenuItem);

            // 
            // deviceToolStripSeparator3
            // 
            this.deviceToolStripSeparator3.Name = "deviceToolStripSeparator3";
            this.deviceToolStripSeparator3.Size = new System.Drawing.Size(227, 6);

            // 
            // removeWaveInDeviceToolStripMenuItem
            // 
            this.removeWaveInDeviceToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.waveInToolStripMenuItem,
            this.removeWaveOutDeviceToolStripMenuItem});
            this.removeWaveInDeviceToolStripMenuItem.Name =
                "removeWaveInDeviceToolStripMenuItem";
            this.removeWaveInDeviceToolStripMenuItem.Size =
                new System.Drawing.Size(230, 26);
            this.removeWaveInDeviceToolStripMenuItem.Text = "Remove...";
            this.toolStripMenuItemList.Add(removeWaveInDeviceToolStripMenuItem);

            // 
            // waveInToolStripMenuItem
            // 
            this.waveInToolStripMenuItem.Name = "waveInToolStripMenuItem";
            this.waveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.waveInToolStripMenuItem.Tag = "";
            this.waveInToolStripMenuItem.Text = "Wave In";
            this.toolStripMenuItemList.Add(waveInToolStripMenuItem);

            // 
            // removeWaveOutDeviceToolStripMenuItem
            // 
            this.removeWaveOutDeviceToolStripMenuItem.Name =
                "removeWaveOutDeviceToolStripMenuItem";
            this.removeWaveOutDeviceToolStripMenuItem.Size =
                new System.Drawing.Size(156, 26);
            this.removeWaveOutDeviceToolStripMenuItem.Tag = "";
            this.removeWaveOutDeviceToolStripMenuItem.Text = "Wave Out";
            this.toolStripMenuItemList.Add(removeWaveOutDeviceToolStripMenuItem);

            // 
            // removeAllDevicesToolStripMenuItem
            // 
            this.removeAllDevicesToolStripMenuItem.Name =
                "removeAllDevicesToolStripMenuItem";
            this.removeAllDevicesToolStripMenuItem.Size =
                new System.Drawing.Size(230, 26);
            this.removeAllDevicesToolStripMenuItem.Tag = "";
            this.removeAllDevicesToolStripMenuItem.Text = "Remove All";
            this.toolStripMenuItemList.Add(removeAllDevicesToolStripMenuItem);

            // 
            // removeAllLinkedDevicesToolStripMenuItem
            // 
            this.removeAllLinkedDevicesToolStripMenuItem.Name =
                "removeAllLinkedDevicesToolStripMenuItem";
            this.removeAllLinkedDevicesToolStripMenuItem.Size =
                new System.Drawing.Size(230, 26);
            this.removeAllLinkedDevicesToolStripMenuItem.Tag = "";
            this.removeAllLinkedDevicesToolStripMenuItem.Text = "Remove All Linked";
            this.toolStripMenuItemList.Add(removeAllLinkedDevicesToolStripMenuItem);

            // 
            // removeAllUnlinkedDevicesToolStripMenuItem
            // 
            this.removeAllUnlinkedDevicesToolStripMenuItem.Name =
                "removeAllUnlinkedDevicesToolStripMenuItem";
            this.removeAllUnlinkedDevicesToolStripMenuItem.Size =
                new System.Drawing.Size(230, 26);
            this.removeAllUnlinkedDevicesToolStripMenuItem.Tag = "";
            this.removeAllUnlinkedDevicesToolStripMenuItem.Text = "Remove All Unlinked";
            this.toolStripMenuItemList.Add(removeAllUnlinkedDevicesToolStripMenuItem);

            // 
            // linkToolStripMenuItem
            // 
            this.linkToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
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
            this.linkToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.linkToolStripMenuItem.Text = "Link";
            this.linkToolStripMenuItem.Click += new System.EventHandler
                (this.linkToolStripMenuItem_Click);
            this.toolStripMenuItemList.Add(linkToolStripMenuItem);

            // 
            // newlinkToolStripMenuItem
            // 
            this.newlinkToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.linkWaveInToolStripMenuItem,
            this.linkWaveOutToolStripMenuItem});
            this.newlinkToolStripMenuItem.Name = "newlinkToolStripMenuItem";
            this.newlinkToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.newlinkToolStripMenuItem.Text = "Link...";
            this.toolStripMenuItemList.Add(newlinkToolStripMenuItem);

            // 
            // linkWaveInToolStripMenuItem
            // 
            this.linkWaveInToolStripMenuItem.Name = "linkWaveInToolStripMenuItem";
            this.linkWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.linkWaveInToolStripMenuItem.Tag = "";
            this.linkWaveInToolStripMenuItem.Text = "Wave In";
            this.toolStripMenuItemList.Add(linkWaveInToolStripMenuItem);

            // 
            // linkWaveOutToolStripMenuItem
            // 
            this.linkWaveOutToolStripMenuItem.Name = "linkWaveOutToolStripMenuItem";
            this.linkWaveOutToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.linkWaveOutToolStripMenuItem.Tag = "";
            this.linkWaveOutToolStripMenuItem.Text = "Wave Out";
            this.toolStripMenuItemList.Add(linkWaveOutToolStripMenuItem);

            // 
            // linkToolStripSeparator1
            // 
            this.linkToolStripSeparator1.Name = "linkToolStripSeparator1";
            this.linkToolStripSeparator1.Size = new System.Drawing.Size(282, 6);

            // 
            // unlinkToolStripMenuItem
            // 
            this.unlinkToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.unlinkWaveInToolStripMenuItem,
            this.unlinkWaveOutToolStripMenuItem});
            this.unlinkToolStripMenuItem.Name = "unlinkToolStripMenuItem";
            this.unlinkToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.unlinkToolStripMenuItem.Text = "Unlink...";
            this.toolStripMenuItemList.Add(unlinkToolStripMenuItem);

            // 
            // unlinkWaveInToolStripMenuItem
            // 
            this.unlinkWaveInToolStripMenuItem.Name = "unlinkWaveInToolStripMenuItem";
            this.unlinkWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.unlinkWaveInToolStripMenuItem.Tag = "";
            this.unlinkWaveInToolStripMenuItem.Text = "Wave In";
            this.toolStripMenuItemList.Add(unlinkWaveInToolStripMenuItem);

            // 
            // unlinkWaveOutToolStripMenuItem
            // 
            this.unlinkWaveOutToolStripMenuItem.Name = "unlinkWaveOutToolStripMenuItem";
            this.unlinkWaveOutToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.unlinkWaveOutToolStripMenuItem.Tag = "";
            this.unlinkWaveOutToolStripMenuItem.Text = "Wave Out";
            this.toolStripMenuItemList.Add(unlinkWaveOutToolStripMenuItem);

            // 
            // unlinkAllToolStripMenuItem
            // 
            this.unlinkAllToolStripMenuItem.Name = "unlinkAllToolStripMenuItem";
            this.unlinkAllToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.unlinkAllToolStripMenuItem.Tag = "";
            this.unlinkAllToolStripMenuItem.Text = "Unlink All";
            this.toolStripMenuItemList.Add(unlinkAllToolStripMenuItem);

            // 
            // linkToolStripSeparator2
            // 
            this.linkToolStripSeparator2.Name = "linkToolStripSeparator2";
            this.linkToolStripSeparator2.Size = new System.Drawing.Size(282, 6);

            // 
            // defaultBitRateToolStripMenuItem
            // 
            this.defaultBitRateToolStripMenuItem.Name =
                "defaultBitRateToolStripMenuItem";
            this.defaultBitRateToolStripMenuItem.Size =
                new System.Drawing.Size(285, 26);
            this.defaultBitRateToolStripMenuItem.Tag = "";
            this.defaultBitRateToolStripMenuItem.Text = "Default Bit Rate (Bit/Sample)";
            this.toolStripMenuItemList.Add(defaultBitRateToolStripMenuItem);

            // 
            // defaultBufferToolStripMenuItem
            // 
            this.defaultBufferToolStripMenuItem.Name = "defaultBufferToolStripMenuItem";
            this.defaultBufferToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultBufferToolStripMenuItem.Tag = "";
            this.defaultBufferToolStripMenuItem.Text = "Default Buffer (ms)";
            this.toolStripMenuItemList.Add(defaultBufferToolStripMenuItem);

            // 
            // defaultChannelsToolStripMenuItem
            // 
            this.defaultChannelsToolStripMenuItem.Name =
                "defaultChannelsToolStripMenuItem";
            this.defaultChannelsToolStripMenuItem.Size =
                new System.Drawing.Size(285, 26);
            this.defaultChannelsToolStripMenuItem.Tag = "";
            this.defaultChannelsToolStripMenuItem.Text = "Default Channels";
            this.toolStripMenuItemList.Add(defaultChannelsToolStripMenuItem);

            // 
            // defaultPrefillToolStripMenuItem
            // 
            this.defaultPrefillToolStripMenuItem.Name =
                "defaultPrefillToolStripMenuItem";
            this.defaultPrefillToolStripMenuItem.Size =
                new System.Drawing.Size(285, 26);
            this.defaultPrefillToolStripMenuItem.Tag = "";
            this.defaultPrefillToolStripMenuItem.Text = "Default Prefill (%)";
            this.toolStripMenuItemList.Add(defaultPrefillToolStripMenuItem);

            // 
            // defaultResyncAtToolStripMenuItem
            // 
            this.defaultResyncAtToolStripMenuItem.Name =
                "defaultResyncAtToolStripMenuItem";
            this.defaultResyncAtToolStripMenuItem.Size =
                new System.Drawing.Size(285, 26);
            this.defaultResyncAtToolStripMenuItem.Tag = "";
            this.defaultResyncAtToolStripMenuItem.Text = "Default Resync At (%)";
            this.toolStripMenuItemList.Add(defaultResyncAtToolStripMenuItem);

            // 
            // defaultSamplingRateToolStripMenuItem
            // 
            this.defaultSamplingRateToolStripMenuItem.Name =
                "defaultSamplingRateToolStripMenuItem";
            this.defaultSamplingRateToolStripMenuItem.Size =
                new System.Drawing.Size(285, 26);
            this.defaultSamplingRateToolStripMenuItem.Tag = "";
            this.defaultSamplingRateToolStripMenuItem.Text =
                "Default Sampling Rate (Hz)";
            this.toolStripMenuItemList.Add(defaultSamplingRateToolStripMenuItem);

            // 
            // repeaterToolStripMenuItem
            // 
            this.repeaterToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.restartAllToolStripMenuItem,
            this.repeaterToolStripSeparator1,
            this.startToolStripMenuItem,
            this.startAllToolStripMenuItem,
            this.repeaterToolStripSeparator2,
            this.stopToolStripMenuItem,
            this.stopAllToolStripMenuItem});
            this.repeaterToolStripMenuItem.Name = "repeaterToolStripMenuItem";
            this.repeaterToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.repeaterToolStripMenuItem.Text = "Repeater";
            this.toolStripMenuItemList.Add(repeaterToolStripMenuItem);

            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.restartToolStripMenuItem.Tag = "";
            this.restartToolStripMenuItem.Text = "Restart...";
            this.toolStripMenuItemList.Add(restartToolStripMenuItem);

            // 
            // restartAllToolStripMenuItem
            // 
            this.restartAllToolStripMenuItem.Name = "restartAllToolStripMenuItem";
            this.restartAllToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.restartAllToolStripMenuItem.Tag = "";
            this.restartAllToolStripMenuItem.Text = "Restart All";
            this.toolStripMenuItemList.Add(restartAllToolStripMenuItem);

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
            this.toolStripMenuItemList.Add(startToolStripMenuItem);

            // 
            // startAllToolStripMenuItem
            // 
            this.startAllToolStripMenuItem.CheckOnClick = true;
            this.startAllToolStripMenuItem.Name = "startAllToolStripMenuItem";
            this.startAllToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.startAllToolStripMenuItem.Tag = "";
            this.startAllToolStripMenuItem.Text = "Start All";
            this.toolStripMenuItemList.Add(startAllToolStripMenuItem);

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
            this.toolStripMenuItemList.Add(stopToolStripMenuItem);

            // 
            // stopAllToolStripMenuItem
            // 
            this.stopAllToolStripMenuItem.CheckOnClick = true;
            this.stopAllToolStripMenuItem.Name = "stopAllToolStripMenuItem";
            this.stopAllToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.stopAllToolStripMenuItem.Tag = "";
            this.stopAllToolStripMenuItem.Text = "Stop All";
            this.toolStripMenuItemList.Add(stopAllToolStripMenuItem);

            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.toggleDarkModeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            this.toolStripMenuItemList.Add(viewToolStripMenuItem);

            // 
            // toggleDarkModeToolStripMenuItem
            // 
            this.toggleDarkModeToolStripMenuItem.CheckOnClick = true;
            this.toggleDarkModeToolStripMenuItem.Name =
                "toggleDarkModeToolStripMenuItem";
            this.toggleDarkModeToolStripMenuItem.Size = new System.Drawing.Size(83, 26);
            this.toggleDarkModeToolStripMenuItem.Click +=
                new System.EventHandler(this.toggleDarkModeToolStripMenuItem_Click);
            this.toolStripMenuItemList.Add(toggleDarkModeToolStripMenuItem);

            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange
                (new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.toolStripMenuItemList.Add(helpToolStripMenuItem);

            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click +=
                new System.EventHandler(this.aboutToolStripMenuItem_Click);
            this.toolStripMenuItemList.Add(this.aboutToolStripMenuItem);

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1012, 253);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            PostDesignerGeneratedLogic();
        }

        #endregion

        /// <summary>
        /// Code to run after generated code.
        /// </summary>
        internal void PostDesignerGeneratedLogic()
        {
            SetInitialChanges();
            SaveInitialRenderer();
            ToggleDarkMode();
        }

        /// <summary>
        /// Save renderer to new parameter before making changes.
        /// </summary>
        internal void SaveInitialRenderer()
        {
            this.initialMenuStrip1Renderer = this.menuStrip1.Renderer;
            this.initialMenuStrip1RenderMode = this.menuStrip1.RenderMode;
        }

        /// <summary>
        /// Set initial changes to form.
        /// </summary>
        internal void SetInitialChanges()
        {
            this.Text = applicationName;
            IsDarkModeEnabledDuringRunTime = Program.IsDarkModeEnabledBeforeRunTime;
        }

        /// <summary>
        /// Toggle changes given dark mode is enabled or not.
        /// </summary>
        internal void ToggleDarkMode()
        {
            ToggleDarkModeRenderer();
            SetConstructorBackColor();
            SetBackAndForeColorOfEveryControl(this.Controls);
            SetForeColorOfEveryMenuItem();
            this.toggleDarkModeToolStripMenuItem.Text = darkModeText;
            this.Invalidate();
        }

        /// <summary>
        /// Toggle the type of renderer given dark mode is enabled or not.
        /// </summary>
        internal void ToggleDarkModeRenderer()
        {
            if (IsDarkModeEnabledDuringRunTime)
            {
                this.menuStrip1.RenderMode = ToolStripRenderMode.Professional;
                this.menuStrip1.Renderer = new ToolStripProfessionalRenderer
                    (new DarkColorTable());
            }
            else
            {
                this.menuStrip1.RenderMode = this.initialMenuStrip1RenderMode;
                this.menuStrip1.Renderer = this.initialMenuStrip1Renderer;
            }
        }

        /// <summary>
        /// Set the backcolor of the constructor, given dark mode is enabled or not.
        /// </summary>
        internal void SetConstructorBackColor()
        {
            if (IsDarkModeEnabledDuringRunTime)
            {
                this.BackColor = darkBackColor;
            }
            else
            {
                this.BackColor = lightBackColor;
            }
        }

        /// <summary>
        /// Set the backcolor and forecolor of every control, 
        /// given dark mode is enabled or not.
        /// </summary>
        /// <param name="controlCollection">The control collection</param>
        internal void SetBackAndForeColorOfEveryControl
            (Control.ControlCollection controlCollection)
        {
            Color backColor, foreColor;

            if (IsDarkModeEnabledDuringRunTime)
            {
                backColor = darkBackColor;
                foreColor = lightBackColor;
            }
            else
            {
                backColor = lightBackColor;
                foreColor = darkBackColor;
            }

            foreach (var control in controlCollection)
            {
                if (control is Control.ControlCollection)
                {
                    SetBackAndForeColorOfEveryControl(control as Control.ControlCollection);
                }

                (control as Control).BackColor = backColor;
                (control as Control).ForeColor = foreColor;
            }
        }

        /// <summary>
        /// Set the forecolor (text color) of every menu item, 
        /// given dark mode is enabled or not.
        /// </summary>
        internal void SetForeColorOfEveryMenuItem()
        {
            Color foreColor;

            if (IsDarkModeEnabledDuringRunTime)
            {
                foreColor = darkTextColor;
            }
            else
            {
                foreColor = lightTextColor;
            }

            toolStripMenuItemList.ForEach(toolStripMenuItem =>
            {
                toolStripMenuItem.ForeColor = foreColor;
            });
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
    }
}