namespace VACARM.NET4.Views
{
    partial class MainForm
    {
        private readonly string applicationName = "VAC Audio Repeater Manager";

        private void PostInitializeComponent()
        {
            this.Text = applicationName;
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCopyAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waveOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllLinkedDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllUnlinkedDevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.engineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.waveInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waveOutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addWaveInDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.defaultBitrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultBuffermsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultPrefillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultResyncAtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultSamplingRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleDarkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolStripMenuItem3,
            this.engineToolStripMenuItem,
            this.toolStripMenuItem4,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1012, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveCopyAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exotToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.openToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(202, 26);
            this.openToolStripMenuItem1.Text = "Open...";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            // 
            // saveCopyAsToolStripMenuItem
            // 
            this.saveCopyAsToolStripMenuItem.Name = "saveCopyAsToolStripMenuItem";
            this.saveCopyAsToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.saveCopyAsToolStripMenuItem.Text = "Save a Copy As...";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // exotToolStripMenuItem
            // 
            this.exotToolStripMenuItem.Name = "exotToolStripMenuItem";
            this.exotToolStripMenuItem.Size = new System.Drawing.Size(202, 26);
            this.exotToolStripMenuItem.Text = "Exit";
            this.exotToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceToolStripMenuItem,
            this.toolStripSeparator2,
            this.addDeviceToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripSeparator7,
            this.removeDeviceToolStripMenuItem,
            this.removeAllDevicesToolStripMenuItem,
            this.removeAllLinkedDevicesToolStripMenuItem,
            this.removeAllUnlinkedDevicesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.editToolStripMenuItem.Text = "Device";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.deviceToolStripMenuItem.Tag = "ReloadDeviceList";
            this.deviceToolStripMenuItem.Text = "Reload";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(227, 6);
            // 
            // addDeviceToolStripMenuItem
            // 
            this.addDeviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addWaveInDeviceToolStripMenuItem,
            this.waveOutToolStripMenuItem});
            this.addDeviceToolStripMenuItem.Name = "addDeviceToolStripMenuItem";
            this.addDeviceToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.addDeviceToolStripMenuItem.Text = "Add...";
            // 
            // waveOutToolStripMenuItem
            // 
            this.waveOutToolStripMenuItem.Name = "waveOutToolStripMenuItem";
            this.waveOutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.waveOutToolStripMenuItem.Tag = "AddWaveOutDeviceList";
            this.waveOutToolStripMenuItem.Text = "Wave Out";
            // 
            // removeDeviceToolStripMenuItem
            // 
            this.removeDeviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.waveInToolStripMenuItem,
            this.waveOutToolStripMenuItem1});
            this.removeDeviceToolStripMenuItem.Name = "removeDeviceToolStripMenuItem";
            this.removeDeviceToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.removeDeviceToolStripMenuItem.Text = "Remove...";
            // 
            // removeAllDevicesToolStripMenuItem
            // 
            this.removeAllDevicesToolStripMenuItem.Name = "removeAllDevicesToolStripMenuItem";
            this.removeAllDevicesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.removeAllDevicesToolStripMenuItem.Tag = "RemoveAllDevicesList";
            this.removeAllDevicesToolStripMenuItem.Text = "Remove All";
            // 
            // removeAllLinkedDevicesToolStripMenuItem
            // 
            this.removeAllLinkedDevicesToolStripMenuItem.Name = "removeAllLinkedDevicesToolStripMenuItem";
            this.removeAllLinkedDevicesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.removeAllLinkedDevicesToolStripMenuItem.Tag = "RemoveAllLinkedDevicesList";
            this.removeAllLinkedDevicesToolStripMenuItem.Text = "Remove All Linked";
            // 
            // removeAllUnlinkedDevicesToolStripMenuItem
            // 
            this.removeAllUnlinkedDevicesToolStripMenuItem.Name = "removeAllUnlinkedDevicesToolStripMenuItem";
            this.removeAllUnlinkedDevicesToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.removeAllUnlinkedDevicesToolStripMenuItem.Tag = "RemoveAllUnlinkedDevicesList";
            this.removeAllUnlinkedDevicesToolStripMenuItem.Text = "Remove All Unlinked";
            // 
            // engineToolStripMenuItem
            // 
            this.engineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.restartToolStripMenuItem,
            this.toolStripSeparator5,
            this.toolStripMenuItem6,
            this.startToolStripMenuItem,
            this.toolStripSeparator6,
            this.toolStripMenuItem7,
            this.stopToolStripMenuItem});
            this.engineToolStripMenuItem.Name = "engineToolStripMenuItem";
            this.engineToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.engineToolStripMenuItem.Text = "Engine";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem1.Tag = "RestartRepeaterList";
            this.toolStripMenuItem1.Text = "Restart";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.restartToolStripMenuItem.Tag = "RestartAllRepeatersList";
            this.restartToolStripMenuItem.Text = "Restart All";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.CheckOnClick = true;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.startToolStripMenuItem.Tag = "StartAllRepeatersList";
            this.startToolStripMenuItem.Text = "Start All";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.CheckOnClick = true;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.stopToolStripMenuItem.Tag = "StopAllRepeatersList";
            this.stopToolStripMenuItem.Text = "Stop All";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(230, 26);
            this.toolStripMenuItem2.Tag = "AddAllDevicesList";
            this.toolStripMenuItem2.Text = "Add All";
            // 
            // waveInToolStripMenuItem
            // 
            this.waveInToolStripMenuItem.Name = "waveInToolStripMenuItem";
            this.waveInToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.waveInToolStripMenuItem.Tag = "RemoveWaveInDeviceList";
            this.waveInToolStripMenuItem.Text = "Wave In";
            // 
            // waveOutToolStripMenuItem1
            // 
            this.waveOutToolStripMenuItem1.Name = "waveOutToolStripMenuItem1";
            this.waveOutToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.waveOutToolStripMenuItem1.Tag = "RemoveWaveOutDeviceList";
            this.waveOutToolStripMenuItem1.Text = "Wave Out";
            // 
            // addWaveInDeviceToolStripMenuItem
            // 
            this.addWaveInDeviceToolStripMenuItem.Name = "addWaveInDeviceToolStripMenuItem";
            this.addWaveInDeviceToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.addWaveInDeviceToolStripMenuItem.Tag = "AddWaveInDeviceList";
            this.addWaveInDeviceToolStripMenuItem.Text = "Wave In";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripSeparator8,
            this.toolStripMenuItem8,
            this.toolStripMenuItem13,
            this.toolStripSeparator4,
            this.defaultBitrateToolStripMenuItem,
            this.defaultBuffermsToolStripMenuItem,
            this.defaultChannelsToolStripMenuItem,
            this.defaultPrefillToolStripMenuItem,
            this.defaultResyncAtToolStripMenuItem,
            this.defaultSamplingRateToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(123, 24);
            this.toolStripMenuItem3.Text = "Link / Repeater";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(285, 26);
            this.toolStripMenuItem5.Tag = "LinkRepeaterList";
            this.toolStripMenuItem5.Text = "Link";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(285, 26);
            this.toolStripMenuItem8.Tag = "UnlinkRepeaterList";
            this.toolStripMenuItem8.Text = "Unlink";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(285, 26);
            this.toolStripMenuItem13.Tag = "UnlinkAllRepeatersList";
            this.toolStripMenuItem13.Text = "Unlink All";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(232, 6);
            // 
            // defaultBitrateToolStripMenuItem
            // 
            this.defaultBitrateToolStripMenuItem.Name = "defaultBitrateToolStripMenuItem";
            this.defaultBitrateToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultBitrateToolStripMenuItem.Tag = "DefaultBitRate";
            this.defaultBitrateToolStripMenuItem.Text = "Default Bit Rate (Bit/Sample)";
            // 
            // defaultBuffermsToolStripMenuItem
            // 
            this.defaultBuffermsToolStripMenuItem.Name = "defaultBuffermsToolStripMenuItem";
            this.defaultBuffermsToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultBuffermsToolStripMenuItem.Tag = "DefaultBuffer";
            this.defaultBuffermsToolStripMenuItem.Text = "Default Buffer (ms)";
            // 
            // defaultPrefillToolStripMenuItem
            // 
            this.defaultPrefillToolStripMenuItem.Name = "defaultPrefillToolStripMenuItem";
            this.defaultPrefillToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultPrefillToolStripMenuItem.Tag = "DefaultPrefill";
            this.defaultPrefillToolStripMenuItem.Text = "Default Prefill (%)";
            // 
            // defaultResyncAtToolStripMenuItem
            // 
            this.defaultResyncAtToolStripMenuItem.Name = "defaultResyncAtToolStripMenuItem";
            this.defaultResyncAtToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultResyncAtToolStripMenuItem.Tag = "DefaultResyncAt";
            this.defaultResyncAtToolStripMenuItem.Text = "Default Resync At (%)";
            // 
            // defaultSamplingRateToolStripMenuItem
            // 
            this.defaultSamplingRateToolStripMenuItem.Name = "defaultSamplingRateToolStripMenuItem";
            this.defaultSamplingRateToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultSamplingRateToolStripMenuItem.Tag = "DefaultSamplingRate";
            this.defaultSamplingRateToolStripMenuItem.Text = "Default Sampling Rate (Hz)";
            // 
            // defaultChannelsToolStripMenuItem
            // 
            this.defaultChannelsToolStripMenuItem.Name = "defaultChannelsToolStripMenuItem";
            this.defaultChannelsToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            this.defaultChannelsToolStripMenuItem.Tag = "DefaultChannels";
            this.defaultChannelsToolStripMenuItem.Text = "Default Channels";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.CheckOnClick = true;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem6.Tag = "StartRepeaterList";
            this.toolStripMenuItem6.Text = "Start";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.CheckOnClick = true;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem7.Tag = "StopRepeaterList";
            this.toolStripMenuItem7.Text = "Stop";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(221, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(221, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(227, 6);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(282, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleDarkModeToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(55, 24);
            this.toolStripMenuItem4.Text = "View";
            // 
            // toggleDarkModeToolStripMenuItem
            // 
            this.toggleDarkModeToolStripMenuItem.CheckOnClick = true;
            this.toggleDarkModeToolStripMenuItem.Name = "toggleDarkModeToolStripMenuItem";
            this.toggleDarkModeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.toggleDarkModeToolStripMenuItem.Text = "Toggle Dark Mode";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1012, 253);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCopyAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem engineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waveOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllLinkedDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllUnlinkedDevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWaveInDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem waveInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waveOutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem defaultBitrateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultBuffermsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultPrefillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultResyncAtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultSamplingRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toggleDarkModeToolStripMenuItem;
    }
}