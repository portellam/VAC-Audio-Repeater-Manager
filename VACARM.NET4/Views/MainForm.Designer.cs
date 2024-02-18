using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using VACARM.NET4.ViewModels;

namespace VACARM.NET4.Views
{
	public partial class MainForm : INotifyPropertyChanged
	{
		#region Parameters

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
		private List<ToolStripItem> toolStripItemList = new List<ToolStripItem>();

		public bool IsDarkModeEnabledDuringRunTime
		{
			get
			{
				Program.IsDarkModeEnabledDuringRunTime =
					viewToggleDarkModeToolStripMenuItem.Checked;
				return viewToggleDarkModeToolStripMenuItem.Checked;
			}
			set
			{
				Program.IsDarkModeEnabledDuringRunTime = value;
				viewToggleDarkModeToolStripMenuItem.Checked = value;
				OnPropertyChanged(nameof(IsDarkModeEnabledDuringRunTime));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Windows Form Designer generated parameters

		private Manina.Windows.Forms.TabControl tabControl1;
		private Manina.Windows.Forms.Tab gridTab;
		private Manina.Windows.Forms.Tab graphTab;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem deviceAddToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			deviceAddConfirmToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			deviceAddSelectAllToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem deviceAddSelectToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			deviceAddSelectWaveInToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			deviceAddSelectWaveOutToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem deviceReloadAllToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			deviceRemoveAllLinkedToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem deviceRemoveAllToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			deviceRemoveAllUnlinkedToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem deviceRemoveToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			deviceRemoveWaveInToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			deviceRemoveWaveOutToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileCloseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileOpenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileSaveACopyAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileSaveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileSaveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpAboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem linkAddToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem linkAddWaveInToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem linkAddWaveOutToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			linkDefaultBitRateToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			linkDefaultBufferToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			linkDefaultChannelsToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			linkDefaultPrefillToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			linkDefaultResyncAtToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			linkDefaultSamplingRateToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem linkRemoveAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem linkRemoveToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			linkRemoveWaveInToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			linkRemoveWaveOutToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem linkToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			repeaterRestartAllToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem repeaterRestartToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			repeaterStartAllToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem repeaterStartToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem repeaterStopAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem repeaterStopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem repeaterToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem
			viewToggleDarkModeToolStripMenuItem;

		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator deviceToolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator linkToolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator linkToolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator repeaterToolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator repeaterToolStripSeparator2;
		private TableLayoutPanel gridTableLayoutPanel;
		private ToolStripRenderer initialMenuStrip1Renderer;

		#endregion

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		[ExcludeFromCodeCoverage]
		internal void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();

			this.deviceAddToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceAddConfirmToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceAddSelectAllToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceAddSelectToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceAddSelectWaveInToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceAddSelectWaveOutToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceReloadAllToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceRemoveAllLinkedToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceRemoveAllToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceRemoveAllUnlinkedToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceRemoveToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceRemoveWaveInToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceRemoveWaveOutToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

			this.deviceToolStripSeparator1 =
				new System.Windows.Forms.ToolStripSeparator();

			this.deviceToolStripSeparator2 =
				new System.Windows.Forms.ToolStripSeparator();

			this.fileCloseToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.fileExitToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.fileNewToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.fileOpenToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.fileSaveACopyAsToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.fileSaveAsToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.fileSaveToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

			this.fileToolStripSeparator1 =
				new System.Windows.Forms.ToolStripSeparator();

			this.graphTab = new Manina.Windows.Forms.Tab();
			this.gridTab = new Manina.Windows.Forms.Tab();
			this.gridTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();

			this.helpAboutToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

			this.linkAddToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkAddWaveInToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkAddWaveOutToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkDefaultBitRateToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkDefaultBufferToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkDefaultChannelsToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkDefaultPrefillToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkDefaultResyncAtToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkDefaultSamplingRateToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkRemoveAllToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkRemoveToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkRemoveWaveInToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkRemoveWaveOutToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

			this.linkToolStripSeparator1 =
				new System.Windows.Forms.ToolStripSeparator();

			this.linkToolStripSeparator2 =
				new System.Windows.Forms.ToolStripSeparator();

			this.repeaterRestartAllToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.repeaterRestartToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.repeaterStartAllToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.repeaterStartToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.repeaterStopAllToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.repeaterStopToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.repeaterToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.repeaterToolStripSeparator1 =
				new System.Windows.Forms.ToolStripSeparator();

			this.repeaterToolStripSeparator2 =
				new System.Windows.Forms.ToolStripSeparator();

			this.tabControl1 = new Manina.Windows.Forms.TabControl();

			this.viewToggleDarkModeToolStripMenuItem =
				new System.Windows.Forms.ToolStripMenuItem();

			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.gridTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);

			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.deviceToolStripMenuItem,
				this.linkToolStripMenuItem,
				this.repeaterToolStripMenuItem,
				this.viewToolStripMenuItem,
				this.helpToolStripMenuItem
			});

			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(622, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// deviceToolStripMenuItem
			// 
			this.deviceToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.deviceReloadAllToolStripMenuItem,
					this.deviceToolStripSeparator1,
					this.deviceAddToolStripMenuItem,
					this.deviceToolStripSeparator2,
					this.deviceRemoveToolStripMenuItem,
					this.deviceRemoveAllToolStripMenuItem,
					this.deviceRemoveAllLinkedToolStripMenuItem,
					this.deviceRemoveAllUnlinkedToolStripMenuItem
				});

			this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";

			this.deviceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));

			this.deviceToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
			this.deviceToolStripMenuItem.Text = "Device";
			this.deviceToolStripMenuItem.DropDown.AutoClose = false;
			//
			// deviceAddToolStripMenuItem
			//
			this.deviceAddToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.deviceAddConfirmToolStripMenuItem,
					this.deviceAddSelectToolStripMenuItem,
					this.deviceAddSelectAllToolStripMenuItem,
				});

			this.deviceAddToolStripMenuItem.Name = "deviceAddToolStripMenuItem";
			this.deviceAddToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
			this.deviceAddToolStripMenuItem.Text = "Add...";
			//
			// deviceAddConfirmToolStripMenuItem
			//
			this.deviceAddConfirmToolStripMenuItem.Name =
				"deviceAddConfirmToolStripMenuItem";

			this.deviceAddConfirmToolStripMenuItem.Size =
				new System.Drawing.Size(230, 26);

			this.deviceAddConfirmToolStripMenuItem.Text = "Confirm Selected";

			this.deviceAddConfirmToolStripMenuItem.Click += new System.EventHandler
				(this.deviceAddConfirmToolStripMenuItem_Click);

			this.deviceAddConfirmToolStripMenuItem.Enabled =
				doAddSelectedWaveInOrWaveOutContainCheckedMenuItem;
			// 
			// deviceAddSelectToolStripMenuItem
			// 
			this.deviceAddSelectToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.deviceAddSelectWaveInToolStripMenuItem,
					this.deviceAddSelectWaveOutToolStripMenuItem
				});

			this.deviceAddSelectToolStripMenuItem.Name =
				"deviceAddSelectToolStripMenuItem";

			this.deviceAddSelectToolStripMenuItem.Size =
				new System.Drawing.Size(230, 26);

			this.deviceAddSelectToolStripMenuItem.Text = "Select...";
			// 
			// deviceAddSelectWaveInToolStripMenuItem
			// 
			this.deviceAddSelectWaveInToolStripMenuItem.Name =
				"deviceAddSelectWaveInToolStripMenuItem";

			this.deviceAddSelectWaveInToolStripMenuItem.Size =
				new System.Drawing.Size(156, 26);

			this.deviceAddSelectWaveInToolStripMenuItem.Tag = "";
			this.deviceAddSelectWaveInToolStripMenuItem.Text = WaveInAsString;
			// 
			// deviceAddSelectWaveOutToolStripMenuItem
			// 
			this.deviceAddSelectWaveOutToolStripMenuItem.Name =
				"deviceAddSelectWaveOutToolStripMenuItem";

			this.deviceAddSelectWaveOutToolStripMenuItem.Size =
				new System.Drawing.Size(156, 26);

			this.deviceAddSelectWaveOutToolStripMenuItem.Tag = "";
			this.deviceAddSelectWaveOutToolStripMenuItem.Text = WaveOutAsString;
			// 
			// deviceAddAllToolStripMenuItem
			// 
			this.deviceAddSelectAllToolStripMenuItem.Name =
				"deviceAddAllToolStripMenuItem";

			this.deviceAddSelectAllToolStripMenuItem.Size =
				new System.Drawing.Size(230, 26);

			this.deviceAddSelectAllToolStripMenuItem.Tag = "";
			this.deviceAddSelectAllToolStripMenuItem.Text = "Select All";

			this.deviceAddSelectAllToolStripMenuItem.Click += new System.EventHandler
				(this.deviceAddSelectAllToolStripMenuItem_Click);
			// 
			// deviceReloadAllToolStripMenuItem
			// 
			this.deviceReloadAllToolStripMenuItem.Name =
				"deviceReloadAllToolStripMenuItem";

			this.deviceReloadAllToolStripMenuItem.Size =
				new System.Drawing.Size(230, 26);

			this.deviceReloadAllToolStripMenuItem.Tag = "";
			this.deviceReloadAllToolStripMenuItem.Text = "Force Reload All";

			this.deviceReloadAllToolStripMenuItem.Click +=
				new System.EventHandler(this.deviceReloadAllToolStripMenuItem_Click);
			// 
			// deviceRemoveToolStripMenuItem
			// 
			this.deviceRemoveToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.deviceRemoveWaveInToolStripMenuItem,
					this.deviceRemoveWaveOutToolStripMenuItem
				});

			this.deviceRemoveToolStripMenuItem.Name = "deviceRemoveToolStripMenuItem";
			this.deviceRemoveToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
			this.deviceRemoveToolStripMenuItem.Text = "Remove...";
			// 
			// deviceRemoveAllToolStripMenuItem
			// 
			this.deviceRemoveAllToolStripMenuItem.Name =
				"deviceRemoveAllToolStripMenuItem";

			this.deviceRemoveAllToolStripMenuItem.Size =
				new System.Drawing.Size(230, 26);

			this.deviceRemoveAllToolStripMenuItem.Tag = "";
			this.deviceRemoveAllToolStripMenuItem.Text = "Remove All";
			// 
			// deviceRemoveAllLinkedToolStripMenuItem
			// 
			this.deviceRemoveAllLinkedToolStripMenuItem.Name =
				"deviceRemoveAllLinkedToolStripMenuItem";

			this.deviceRemoveAllLinkedToolStripMenuItem.Size =
				new System.Drawing.Size(230, 26);

			this.deviceRemoveAllLinkedToolStripMenuItem.Tag = "";
			this.deviceRemoveAllLinkedToolStripMenuItem.Text = "Remove All Linked";
			// 
			// deviceRemoveAllUnlinkedToolStripMenuItem
			// 
			this.deviceRemoveAllUnlinkedToolStripMenuItem.Name =
				"deviceRemoveAllUnlinkedToolStripMenuItem";

			this.deviceRemoveAllUnlinkedToolStripMenuItem.Size =
				new System.Drawing.Size(230, 26);

			this.deviceRemoveAllUnlinkedToolStripMenuItem.Tag = "";
			this.deviceRemoveAllUnlinkedToolStripMenuItem.Text = "Remove All Unlinked";
			// 
			// deviceRemoveWaveInToolStripMenuItem
			// 
			this.deviceRemoveWaveInToolStripMenuItem.Name =
				"deviceRemoveWaveInToolStripMenuItem";

			this.deviceRemoveWaveInToolStripMenuItem.Size =
				new System.Drawing.Size(156, 26);

			this.deviceRemoveWaveInToolStripMenuItem.Tag = "";
			this.deviceRemoveWaveInToolStripMenuItem.Text = WaveInAsString;
			// 
			// deviceRemoveWaveOutToolStripMenuItem
			// 
			this.deviceRemoveWaveOutToolStripMenuItem.Name =
				"deviceRemoveWaveOutToolStripMenuItem";

			this.deviceRemoveWaveOutToolStripMenuItem.Size =
				new System.Drawing.Size(156, 26);

			this.deviceRemoveWaveOutToolStripMenuItem.Tag = "";
			this.deviceRemoveWaveOutToolStripMenuItem.Text = WaveOutAsString;
			// 
			// deviceToolStripSeparator1
			// 
			this.deviceToolStripSeparator1.Name = "deviceToolStripSeparator1";
			this.deviceToolStripSeparator1.Size = new System.Drawing.Size(227, 6);
			// 
			// deviceToolStripSeparator2
			// 
			this.deviceToolStripSeparator2.Name = "deviceToolStripSeparator2";
			this.deviceToolStripSeparator2.Size = new System.Drawing.Size(227, 6);
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.fileNewToolStripMenuItem,
					this.fileOpenToolStripMenuItem,
					this.fileSaveToolStripMenuItem,
					this.fileSaveAsToolStripMenuItem,
					this.fileSaveACopyAsToolStripMenuItem,
					this.fileCloseToolStripMenuItem,
					this.fileToolStripSeparator1,
					this.fileExitToolStripMenuItem
				});

			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";

			this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));

			this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// fileCloseToolStripMenuItem
			// 
			this.fileCloseToolStripMenuItem.Name = "fileCloseToolStripMenuItem";

			this.fileCloseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));

			this.fileCloseToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
			this.fileCloseToolStripMenuItem.Text = "Close";
			// 
			// fileExitToolStripMenuItem
			// 
			this.fileExitToolStripMenuItem.Name = "fileExitToolStripMenuItem";

			this.fileExitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));

			this.fileExitToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
			this.fileExitToolStripMenuItem.Text = "Exit";

			this.fileExitToolStripMenuItem.Click +=
				new System.EventHandler(this.fileExitToolStripMenuItem_Click);
			// 
			// fileNewToolStripMenuItem
			// 
			this.fileNewToolStripMenuItem.Name = "fileNewToolStripMenuItem";

			this.fileNewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));

			this.fileNewToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
			this.fileNewToolStripMenuItem.Text = "New";
			// 
			// fileOpenToolStripMenuItem
			// 
			this.fileOpenToolStripMenuItem.Name = "fileOpenToolStripMenuItem";

			this.fileOpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));

			this.fileOpenToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
			this.fileOpenToolStripMenuItem.Text = "Open...";

			this.fileOpenToolStripMenuItem.Click +=
				new System.EventHandler(this.fileOpenToolStripMenuItem_Click);
			// 
			// fileSaveToolStripMenuItem
			// 
			this.fileSaveToolStripMenuItem.Name = "fileSaveToolStripMenuItem";

			this.fileSaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));

			this.fileSaveToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
			this.fileSaveToolStripMenuItem.Text = "Save";
			// 
			// fileSaveAsToolStripMenuItem
			// 
			this.fileSaveAsToolStripMenuItem.Name = "fileSaveAsToolStripMenuItem";

			this.fileSaveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
				| System.Windows.Forms.Keys.S)));

			this.fileSaveAsToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
			this.fileSaveAsToolStripMenuItem.Text = "Save As...";
			// 
			// fileSaveACopyAsToolStripMenuItem
			// 
			this.fileSaveACopyAsToolStripMenuItem.Name =
				"fileSaveACopyAsToolStripMenuItem";

			this.fileSaveACopyAsToolStripMenuItem.Size =
				new System.Drawing.Size(231, 26);

			this.fileSaveACopyAsToolStripMenuItem.Text = "Save a Copy As...";
			// 
			// fileToolStripSeparator1
			// 
			this.fileToolStripSeparator1.Name = "fileToolStripSeparator1";
			this.fileToolStripSeparator1.Size = new System.Drawing.Size(228, 6);
			// 
			// linkToolStripMenuItem
			// 
			this.linkToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.linkAddToolStripMenuItem,
					this.linkToolStripSeparator1,
					this.linkRemoveToolStripMenuItem,
					this.linkRemoveAllToolStripMenuItem,
					this.linkToolStripSeparator2,
					this.linkDefaultBitRateToolStripMenuItem,
					this.linkDefaultBufferToolStripMenuItem,
					this.linkDefaultChannelsToolStripMenuItem,
					this.linkDefaultPrefillToolStripMenuItem,
					this.linkDefaultResyncAtToolStripMenuItem,
					this.linkDefaultSamplingRateToolStripMenuItem
				});

			this.linkToolStripMenuItem.Name = "linkToolStripMenuItem";

			this.linkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));

			this.linkToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
			this.linkToolStripMenuItem.Text = "Link";
			// 
			// linkAddToolStripMenuItem
			// 
			this.linkAddToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.linkAddWaveInToolStripMenuItem,
					this.linkAddWaveOutToolStripMenuItem
				});

			this.linkAddToolStripMenuItem.Name = "linkAddToolStripMenuItem";
			this.linkAddToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
			this.linkAddToolStripMenuItem.Text = "Link...";
			// 
			// linkAddWaveInToolStripMenuItem
			// 
			this.linkAddWaveInToolStripMenuItem.Name = "linkAddWaveInToolStripMenuItem";
			this.linkAddWaveInToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
			this.linkAddWaveInToolStripMenuItem.Tag = "";
			this.linkAddWaveInToolStripMenuItem.Text = WaveInAsString;
			// 
			// linkAddWaveOutToolStripMenuItem
			// 
			this.linkAddWaveOutToolStripMenuItem.Name =
				"linkAddWaveOutToolStripMenuItem";

			this.linkAddWaveOutToolStripMenuItem.Size =
				new System.Drawing.Size(156, 26);

			this.linkAddWaveOutToolStripMenuItem.Tag = "";
			this.linkAddWaveOutToolStripMenuItem.Text = WaveOutAsString;
			// 
			// linkRemoveToolStripMenuItem
			// 
			this.linkRemoveToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.linkRemoveWaveInToolStripMenuItem,
					this.linkRemoveWaveOutToolStripMenuItem
				});

			this.linkRemoveToolStripMenuItem.Name = "linkRemoveToolStripMenuItem";
			this.linkRemoveToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
			this.linkRemoveToolStripMenuItem.Text = "Unlink...";
			// 
			// linkRemoveAllToolStripMenuItem
			// 
			this.linkRemoveAllToolStripMenuItem.Name = "linkRemoveAllToolStripMenuItem";
			this.linkRemoveAllToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
			this.linkRemoveAllToolStripMenuItem.Tag = "";
			this.linkRemoveAllToolStripMenuItem.Text = "Unlink All";
			// 
			// 
			// linkRemoveWaveInToolStripMenuItem
			// 
			this.linkRemoveWaveInToolStripMenuItem.Name =
				"linkRemoveWaveInToolStripMenuItem";

			this.linkRemoveWaveInToolStripMenuItem.Size =
				new System.Drawing.Size(156, 26);

			this.linkRemoveWaveInToolStripMenuItem.Tag = "";
			this.linkRemoveWaveInToolStripMenuItem.Text = WaveInAsString;
			// 
			// linkRemoveWaveOutToolStripMenuItem
			// 
			this.linkRemoveWaveOutToolStripMenuItem.Name =
				"linkRemoveWaveOutToolStripMenuItem";

			this.linkRemoveWaveOutToolStripMenuItem.Size =
				new System.Drawing.Size(156, 26);

			this.linkRemoveWaveOutToolStripMenuItem.Tag = "";
			this.linkRemoveWaveOutToolStripMenuItem.Text = WaveOutAsString;
			// 
			// linkDefaultBitRateToolStripMenuItem
			// 
			this.linkDefaultBitRateToolStripMenuItem.Name =
				"DefaultBitRateToolStripMenuItem";

			this.linkDefaultBitRateToolStripMenuItem.Size =
				new System.Drawing.Size(285, 26);

			this.linkDefaultBitRateToolStripMenuItem.Tag = "";
			this.linkDefaultBitRateToolStripMenuItem.Text =
				"Default Bit Rate (Bit/Sample)";
			// 
			// linkDefaultBufferToolStripMenuItem
			// 
			this.linkDefaultBufferToolStripMenuItem.Name =
				"DefaultBufferToolStripMenuItem";

			this.linkDefaultBufferToolStripMenuItem.Size =
				new System.Drawing.Size(285, 26);

			this.linkDefaultBufferToolStripMenuItem.Tag = "";
			this.linkDefaultBufferToolStripMenuItem.Text = "Default Buffer (ms)";
			// 
			// linkDefaultChannelsToolStripMenuItem
			// 
			this.linkDefaultChannelsToolStripMenuItem.Name =
				"DefaultChannelsToolStripMenuItem";

			this.linkDefaultChannelsToolStripMenuItem.Size =
				new System.Drawing.Size(285, 26);

			this.linkDefaultChannelsToolStripMenuItem.Tag = "";
			this.linkDefaultChannelsToolStripMenuItem.Text = "Default Channels";
			// 
			// linkDefaultPrefillToolStripMenuItem
			// 
			this.linkDefaultPrefillToolStripMenuItem.Name =
				"DefaultPrefillToolStripMenuItem";

			this.linkDefaultPrefillToolStripMenuItem.Size =
				new System.Drawing.Size(285, 26);

			this.linkDefaultPrefillToolStripMenuItem.Tag = "";
			this.linkDefaultPrefillToolStripMenuItem.Text = "Default Prefill (%)";
			// 
			// linkDefaultResyncAtToolStripMenuItem
			// 
			this.linkDefaultResyncAtToolStripMenuItem.Name =
				"DefaultResyncAtToolStripMenuItem";

			this.linkDefaultResyncAtToolStripMenuItem.Size =
				new System.Drawing.Size(285, 26);

			this.linkDefaultResyncAtToolStripMenuItem.Tag = "";

			this.linkDefaultResyncAtToolStripMenuItem.Text =
				"Default Resync At (%)";
			// 
			// linkDefaultSamplingRateToolStripMenuItem
			// 
			this.linkDefaultSamplingRateToolStripMenuItem.Name =
				"DefaultSamplingRateToolStripMenuItem";

			this.linkDefaultSamplingRateToolStripMenuItem.Size =
				new System.Drawing.Size(285, 26);

			this.linkDefaultSamplingRateToolStripMenuItem.Tag = "";

			this.linkDefaultSamplingRateToolStripMenuItem.Text =
				"Default Sampling Rate (Hz)";
			// 
			// linkToolStripSeparator1
			// 
			this.linkToolStripSeparator1.Name = "linkToolStripSeparator1";
			this.linkToolStripSeparator1.Size = new System.Drawing.Size(282, 6);
			// linkToolStripSeparator2
			// 
			this.linkToolStripSeparator2.Name = "linkToolStripSeparator2";
			this.linkToolStripSeparator2.Size = new System.Drawing.Size(282, 6);
			// 
			// repeaterToolStripMenuItem
			// 
			this.repeaterToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.repeaterRestartToolStripMenuItem,
					this.repeaterRestartAllToolStripMenuItem,
					this.repeaterToolStripSeparator1,
					this.repeaterStartToolStripMenuItem,
					this.repeaterStartAllToolStripMenuItem,
					this.repeaterToolStripSeparator2,
					this.repeaterStopToolStripMenuItem,
					this.repeaterStopAllToolStripMenuItem
				});

			this.repeaterToolStripMenuItem.Name = "repeaterToolStripMenuItem";

			this.repeaterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));

			this.repeaterToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
			this.repeaterToolStripMenuItem.Text = "Repeater";
			// 
			// repeaterRestartAllToolStripMenuItem
			// 
			this.repeaterRestartAllToolStripMenuItem.Name =
				"repeaterRestartAllToolStripMenuItem";

			this.repeaterRestartAllToolStripMenuItem.Size =
				new System.Drawing.Size(224, 26);

			this.repeaterRestartAllToolStripMenuItem.Tag = "";
			this.repeaterRestartAllToolStripMenuItem.Text = "Restart All";
			// 
			// repeaterRestartToolStripMenuItem
			// 
			this.repeaterRestartToolStripMenuItem.Name =
				"repeaterRestartToolStripMenuItem";

			this.repeaterRestartToolStripMenuItem.Size =
				new System.Drawing.Size(224, 26);

			this.repeaterRestartToolStripMenuItem.Tag = "";
			this.repeaterRestartToolStripMenuItem.Text = "Restart...";
			// 
			// repeaterStartAllToolStripMenuItem
			// 
			this.repeaterStartAllToolStripMenuItem.CheckOnClick = true;

			this.repeaterStartAllToolStripMenuItem.Name =
				"repeaterStartAllToolStripMenuItem";

			this.repeaterStartAllToolStripMenuItem.Size =
				new System.Drawing.Size(224, 26);

			this.repeaterStartAllToolStripMenuItem.Tag = "";
			this.repeaterStartAllToolStripMenuItem.Text = "Start All";
			// 
			// repeaterStartToolStripMenuItem
			// 
			this.repeaterStartToolStripMenuItem.CheckOnClick = true;
			this.repeaterStartToolStripMenuItem.Name = "repeaterStartToolStripMenuItem";
			this.repeaterStartToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
			this.repeaterStartToolStripMenuItem.Tag = "";
			this.repeaterStartToolStripMenuItem.Text = "Start...";
			// 
			// repeaterStopToolStripMenuItem
			// 
			this.repeaterStopToolStripMenuItem.CheckOnClick = true;

			this.repeaterStopToolStripMenuItem.Name = "repeaterStopToolStripMenuItem";

			this.repeaterStopToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
			this.repeaterStopToolStripMenuItem.Tag = "";
			this.repeaterStopToolStripMenuItem.Text = "Stop...";
			// 
			// repeaterStopAllToolStripMenuItem
			// 
			this.repeaterStopAllToolStripMenuItem.CheckOnClick = true;

			this.repeaterStopAllToolStripMenuItem.Name =
				"repeaterStopAllToolStripMenuItem";

			this.repeaterStopAllToolStripMenuItem.Size =
				new System.Drawing.Size(224, 26);

			this.repeaterStopAllToolStripMenuItem.Tag = "";
			this.repeaterStopAllToolStripMenuItem.Text = "Stop All";
			// 
			// repeaterToolStripSeparator1
			// 
			this.repeaterToolStripSeparator1.Name = "repeaterToolStripSeparator1";
			this.repeaterToolStripSeparator1.Size = new System.Drawing.Size(221, 6);
			// 
			// repeaterToolStripSeparator2
			// 
			this.repeaterToolStripSeparator2.Name = "repeaterToolStripSeparator2";
			this.repeaterToolStripSeparator2.Size = new System.Drawing.Size(221, 6);
			// 
			// graphTab
			// 
			this.graphTab.Location = new System.Drawing.Point(0, 0);
			this.graphTab.Name = "graphTab";
			this.graphTab.Size = new System.Drawing.Size(0, 0);
			this.graphTab.Text = "Graph";
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

			this.gridTableLayoutPanel.ColumnStyles.Add
				(new System.Windows.Forms.ColumnStyle
					(System.Windows.Forms.SizeType.Percent, 50F));

			this.gridTableLayoutPanel.ColumnStyles.Add
				(new System.Windows.Forms.ColumnStyle
					(System.Windows.Forms.SizeType.Percent, 50F));

			this.gridTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;

			this.gridTableLayoutPanel.GrowStyle =
				System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;

			this.gridTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.gridTableLayoutPanel.Name = "gridTableLayoutPanel";
			this.gridTableLayoutPanel.RowCount = 2;

			this.gridTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
				(System.Windows.Forms.SizeType.Percent, 50F));

			this.gridTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
				(System.Windows.Forms.SizeType.Percent, 50F));

			this.gridTableLayoutPanel.Size = new System.Drawing.Size(620, 385);
			this.gridTableLayoutPanel.TabIndex = 0;
			this.gridTableLayoutPanel.TabStop = true;
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.helpAboutToolStripMenuItem
				});

			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";

			this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));

			this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// helpAboutToolStripMenuItem
			// 
			this.helpAboutToolStripMenuItem.Name = "helpAboutToolStripMenuItem";
			this.helpAboutToolStripMenuItem.Size = new System.Drawing.Size(133, 26);

			this.helpAboutToolStripMenuItem.Text =
				$"About {Common.ApplicationNameAsAbbreviation}";

			this.helpAboutToolStripMenuItem.Click +=
				new System.EventHandler(this.helpAboutToolStripMenuItem_Click);
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
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems
				.AddRange(new System.Windows.Forms.ToolStripItem[]
				{
					this.viewToggleDarkModeToolStripMenuItem
				});

			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";

			this.viewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)
				((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));

			this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// viewToggleDarkModeToolStripMenuItem
			// 
			this.viewToggleDarkModeToolStripMenuItem.CheckOnClick = true;

			this.viewToggleDarkModeToolStripMenuItem.Name =
				"viewToggleDarkModeToolStripMenuItem";

			this.viewToggleDarkModeToolStripMenuItem.Size =
				new System.Drawing.Size(83, 26);

			this.viewToggleDarkModeToolStripMenuItem.Click +=
				new System.EventHandler(this.viewToggleDarkModeToolStripMenuItem_Click);
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
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.gridTab.ResumeLayout(false);
			this.gridTab.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		#region Logic

		/// <summary>
		/// Set the ability of the DeviceAdd and DeviceAddAll menu items.
		/// </summary>
		internal void DeviceAddMenuItemAbility()
		{
			bool isEnabled = deviceAddSelectWaveInToolStripMenuItem.Enabled
				|| deviceAddSelectWaveOutToolStripMenuItem.Enabled;
			deviceAddSelectToolStripMenuItem.Enabled = isEnabled;
			deviceAddSelectAllToolStripMenuItem.Enabled = isEnabled;
		}

		/// <summary>
		/// Set the ability of the DeviceRemove and DeviceRemoveAll menu items.
		/// </summary>
		internal void DeviceRemoveMenuItemAbility()
		{
			bool isEnabled = deviceRemoveWaveInToolStripMenuItem.Enabled
				|| deviceRemoveWaveOutToolStripMenuItem.Enabled;
			deviceRemoveToolStripMenuItem.Enabled = isEnabled;
			deviceRemoveAllToolStripMenuItem.Enabled = isEnabled;
		}

		/// <summary>
		/// Add all controls to list.
		/// </summary>
		internal void InitializeControlsList()                                          //NOTE: Append new control objects here!
		{
			controlList.Clear();
			controlList.Add(tabControl1);
			controlList.Add(graphTab);
			controlList.Add(gridTab);
		}

		/// <summary>
		/// Add all tool strip items to list.
		/// </summary>
		internal void InitializeToolStripItemList()                                     //NOTE: Append new tool strip item objects here!
		{
			toolStripItemList.Clear();
			toolStripItemList.Add(deviceAddConfirmToolStripMenuItem);
			toolStripItemList.Add(deviceAddSelectAllToolStripMenuItem);
			toolStripItemList.Add(deviceAddSelectToolStripMenuItem);
			toolStripItemList.Add(deviceAddSelectWaveInToolStripMenuItem);
			toolStripItemList.Add(deviceAddSelectWaveOutToolStripMenuItem);
			toolStripItemList.Add(deviceAddToolStripMenuItem);
			toolStripItemList.Add(deviceReloadAllToolStripMenuItem);
			toolStripItemList.Add(deviceRemoveAllLinkedToolStripMenuItem);
			toolStripItemList.Add(deviceRemoveAllToolStripMenuItem);
			toolStripItemList.Add(deviceRemoveAllUnlinkedToolStripMenuItem);
			toolStripItemList.Add(deviceRemoveToolStripMenuItem);
			toolStripItemList.Add(deviceRemoveWaveInToolStripMenuItem);
			toolStripItemList.Add(deviceRemoveWaveOutToolStripMenuItem);
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
			toolStripItemList.Add(linkAddToolStripMenuItem);
			toolStripItemList.Add(linkAddWaveInToolStripMenuItem);
			toolStripItemList.Add(linkAddWaveOutToolStripMenuItem);
			toolStripItemList.Add(linkDefaultBitRateToolStripMenuItem);
			toolStripItemList.Add(linkDefaultBufferToolStripMenuItem);
			toolStripItemList.Add(linkDefaultChannelsToolStripMenuItem);
			toolStripItemList.Add(linkDefaultPrefillToolStripMenuItem);
			toolStripItemList.Add(linkDefaultResyncAtToolStripMenuItem);
			toolStripItemList.Add(linkDefaultSamplingRateToolStripMenuItem);
			toolStripItemList.Add(linkRemoveAllToolStripMenuItem);
			toolStripItemList.Add(linkRemoveToolStripMenuItem);
			toolStripItemList.Add(linkRemoveWaveInToolStripMenuItem);
			toolStripItemList.Add(linkRemoveWaveOutToolStripMenuItem);
			toolStripItemList.Add(linkToolStripMenuItem);
			toolStripItemList.Add(linkToolStripSeparator1);
			toolStripItemList.Add(linkToolStripSeparator2);
			toolStripItemList.Add(repeaterRestartAllToolStripMenuItem);
			toolStripItemList.Add(repeaterRestartToolStripMenuItem);
			toolStripItemList.Add(repeaterStartAllToolStripMenuItem);
			toolStripItemList.Add(repeaterStartToolStripMenuItem);
			toolStripItemList.Add(repeaterStopAllToolStripMenuItem);
			toolStripItemList.Add(repeaterStopToolStripMenuItem);
			toolStripItemList.Add(repeaterToolStripMenuItem);
			toolStripItemList.Add(repeaterToolStripSeparator1);
			toolStripItemList.Add(repeaterToolStripSeparator2);
			toolStripItemList.Add(viewToggleDarkModeToolStripMenuItem);
			toolStripItemList.Add(viewToolStripMenuItem);
		}

		/// <summary>
		/// Initialize the device tool strip menu item.
		/// </summary>
		/// <param name="eventHandler">The event handler</param>
		/// <param name="mMDevice">The MMDevice</param>
		/// <param name="toolStripMenuItemList">The tool strip menu item list</param>
		internal void InitializeDeviceItem(EventHandler eventHandler,
			MMDevice mMDevice, ref List<ToolStripMenuItem> toolStripMenuItemList)
		{
			//if (mMDevice.State == DeviceState.NotPresent)
			//{
			//	return;
			//}

			if (mMDevice is null || toolStripMenuItemList is null)
			{
				return;
			}

			bool deviceIsEnabled = mMDevice.State != DeviceState.Disabled;
			string text = $"{mMDevice.FriendlyName} ";

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
				ToolTipText = mMDevice.FriendlyName,                                //NOTE: The ToolTipText property must contain the MMDevice.FriendlyName, so that the MenuItem as a sender object will be properly validated in DeviceList logic.
			};

			if (eventHandler != null)
			{
				toolStripMenuItem.Click += new System.EventHandler(eventHandler);
			}

			toolStripMenuItem.CheckOnClick = true;

			if (toolStripMenuItemList.Contains(toolStripMenuItem))
			{
				return;
			}

			toolStripMenuItemList.Add(toolStripMenuItem);
		}

		/// <summary>
		/// Initialize a device tool strip menu item collection by parsing the related
		/// device list.
		/// </summary>
		/// <param name="eventHandler">The event handler</param>
		/// <param name="parentToolStripMenuItem">The parent device tool strip menu item
		/// </param>
		/// <param name="mMDeviceList">The device list</param>
		internal void InitializeDeviceItemCollection(EventHandler eventHandler,
			ref ToolStripMenuItem parentToolStripMenuItem, List<MMDevice> mMDeviceList)
		{
			if (mMDeviceList is null || mMDeviceList.Count == 0)
			{
				return;
			}

			DataFlow dataFlow = mMDeviceList.FirstOrDefault().DataFlow;
			parentToolStripMenuItem.DropDownItems.Clear();

			List<ToolStripMenuItem> toolStripMenuItemList =
				new List<ToolStripMenuItem>();

			foreach (MMDevice mMDevice in mMDeviceList.ToList())
			{
				InitializeDeviceItem(eventHandler, mMDevice, ref toolStripMenuItemList);
			}

			toolStripMenuItemList.ForEach(toolStripMenuItem =>
			{
				InitializeDeviceItemText(dataFlow, ref toolStripMenuItem);
			});

			parentToolStripMenuItem.DropDownItems.AddRange
				(toolStripMenuItemList.ToArray());

			FormColorUpdater.SetColorsOfToolStripItem(parentToolStripMenuItem);

			SetMousePropertiesOfToolStripMenuItemDropDown
				(ref parentToolStripMenuItem);

			SetPropertiesOfToolStripMenuItemGivenItemCollectionIsEmptyOrNot
				(ref parentToolStripMenuItem);
		}

		/// <summary>
		/// Initialize the text of the device tool strip menu item.
		/// </summary>
		/// <param name="dataFlow">The data flow</param>
		/// <param name="toolStripMenuItem">The device tool strip menu item</param>
		internal void InitializeDeviceItemText
			(DataFlow dataFlow, ref ToolStripMenuItem toolStripMenuItem)
		{
			int index = deviceListModel.GetIndexOfMMDevice
				(dataFlow, toolStripMenuItem.ToolTipText);

			index++;
			string prefix = $"{index.ToString()}. ";

			toolStripMenuItem.Text = string.Format
				("{0,4} {1}", prefix, toolStripMenuItem.Text);
		}

        /// <summary>
        /// Set mouse properties of drop down tool strip menu item.
        /// </summary>
        /// <param name="toolStripMenuItem">The drop down tool strip menu item</param>
        internal void SetMousePropertiesOfToolStripMenuItemDropDown
			(ref ToolStripMenuItem toolStripMenuItem)
		{
			if (toolStripMenuItem is null)
			{
				return;
			}

			toolStripMenuItem.DropDown.MouseEnter +=
				new System.EventHandler
				(this.SetAutoClosePropertyOfToolStripDropDown_MouseEnter);

			toolStripMenuItem.DropDown.MouseLeave +=
				new System.EventHandler
				(this.SetAutoClosePropertyOfToolStripDropDown_MouseLeave);
		}

		/// <summary>
		/// Initialize device drop down collections.
		/// </summary>
		internal void InitializeDeviceDropDownCollections()
		{
			deviceAddSelectWaveInToolStripMenuItem.DropDownItems.Clear();
			deviceAddSelectWaveOutToolStripMenuItem.DropDownItems.Clear();
			deviceRemoveWaveInToolStripMenuItem.DropDownItems.Clear();
			deviceRemoveWaveOutToolStripMenuItem.DropDownItems.Clear();
			linkAddWaveInToolStripMenuItem.DropDownItems.Clear();
			linkAddWaveOutToolStripMenuItem.DropDownItems.Clear();
			linkRemoveWaveInToolStripMenuItem.DropDownItems.Clear();
			linkRemoveWaveOutToolStripMenuItem.DropDownItems.Clear();

			string text = deviceToolStripMenuItem.Text;
			deviceToolStripMenuItem.Text = "Loading...";
			deviceToolStripMenuItem.Enabled = false;
			Refresh();

			InitializeDeviceItemCollection
				(deviceAddConfirmToolStripMenuItemEnabled_Toggle,
				ref deviceAddSelectWaveInToolStripMenuItem,
				deviceListModel.UnselectedWaveInMMDeviceList);

			InitializeDeviceItemCollection
				(deviceAddConfirmToolStripMenuItemEnabled_Toggle,
				ref deviceAddSelectWaveOutToolStripMenuItem,
				deviceListModel.UnselectedWaveOutMMDeviceList);

			InitializeDeviceItemCollection
				(deviceRemoveWaveInToolStripMenuItemDropDown_Click,
				ref deviceRemoveWaveInToolStripMenuItem,
				deviceListModel.SelectedWaveInMMDeviceList);

			InitializeDeviceItemCollection
				(deviceRemoveWaveOutToolStripMenuItemDropDown_Click,
				ref deviceRemoveWaveOutToolStripMenuItem,
				deviceListModel.SelectedWaveOutMMDeviceList);

			InitializeDeviceItemCollection
				(linkAddWaveInToolStripMenuItem_Click,
				ref linkAddWaveInToolStripMenuItem,
				deviceListModel.SelectedWaveInMMDeviceList);

			InitializeDeviceItemCollection
				(linkAddWaveOutToolStripMenuItem_Click,
				ref linkAddWaveOutToolStripMenuItem,
				deviceListModel.SelectedWaveOutMMDeviceList);

			InitializeDeviceItemCollection
				(linkRemoveWaveInToolStripMenuItem_Click,
				ref linkRemoveWaveInToolStripMenuItem,
				repeaterDataModel.LinkWaveInMMDeviceList);

			InitializeDeviceItemCollection
				(linkRemoveWaveOutToolStripMenuItem_Click,
				ref linkRemoveWaveOutToolStripMenuItem,
				repeaterDataModel.LinkWaveOutMMDeviceList);

			deviceToolStripMenuItem.Text = text;
			deviceToolStripMenuItem.Enabled = true;
			Refresh();
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
		/// Logic to execute before InitializeLists.
		/// </summary>
		internal void ModifyListItemsBeforeInitialization()
		{
			InitializeDeviceDropDownCollections();
			DeviceAddMenuItemAbility();
			DeviceRemoveMenuItemAbility();
		}

		/// <summary>
		/// Code to run after generated code.
		/// </summary>
		internal void PostInitializeComponent()
		{
			SetRepeaterDataModel();
			ModifyListItemsBeforeInitialization();
			InitializeLists();
			SetInitialChanges();
			SetColorTheme();

			if (aboutForm != null)
			{
				aboutForm.SetColorTheme();
			}
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
			viewToggleDarkModeToolStripMenuItem.Text = darkModeText;

			FormColorUpdater.SetColorsOfConstructor(this);
			FormColorUpdater.SetColorsOfControlCollection(Controls);
			FormColorUpdater.SetColorsOfControlList(controlList);
			FormColorUpdater.SetColorsOfToolStripItemList(toolStripItemList);

			Invalidate();
			Refresh();
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

		#endregion
	}
}