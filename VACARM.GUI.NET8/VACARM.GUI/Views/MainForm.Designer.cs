namespace VACARM.GUI.Views
{
  partial class MainForm
  {
    #region Parameters

    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion

    #region Logic

    /// <summary>
    /// Dispose of unmanaged objects and true/false dispose of managed objects.
    /// </summary>
    /// <param name="isDisposed">True/false</param>
    protected override void Dispose(bool isDisposed)
    {
      if
      (
        isDisposed
        && this.components != null
      )
      {
        this.components
          .Dispose();
      }

      base.Dispose(isDisposed);
    }

    #endregion

    #region Windows Form Designer generated logic

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      toolStrip1 = new ToolStrip();
      deviceDisableToolStripMenuItem = new ToolStripMenuItem();
      deviceEnableToolStripMenuItem = new ToolStripMenuItem();
      deviceExportToClipboardToolStripMenuItem = new ToolStripMenuItem();
      deviceExportToXMLToolStripMenuItem = new ToolStripMenuItem();
      deviceFindToolStripMenuItem = new ToolStripMenuItem();
      deviceImportFromClipboardToolStripMenuItem = new ToolStripMenuItem();
      deviceImportFromXMLToolStripMenuItem = new ToolStripMenuItem();
      deviceRedoToolStripMenuItem = new ToolStripMenuItem();
      deviceRefreshToolStripMenuItem = new ToolStripMenuItem();
      deviceSelectAllToolStripMenuItem = new ToolStripMenuItem();
      deviceSelectRangeToolStripMenuItem = new ToolStripMenuItem();
      deviceSelectToolStripMenuItem = new ToolStripMenuItem();
      deviceSetAsDefaultToolStripMenuItem = new ToolStripMenuItem();
      deviceToolStripLabel = new ToolStripDropDownButton();
      deviceToolStripSeparator1 = new ToolStripSeparator();
      deviceToolStripSeparator2 = new ToolStripSeparator();
      deviceToolStripSeparator3 = new ToolStripSeparator();
      deviceUndoToolStripMenuItem = new ToolStripMenuItem();
      fileCloseAllToolStripMenuItem = new ToolStripMenuItem();
      fileCloseMultipleToolStripMenuItem = new ToolStripMenuItem();
      fileCloseToolStripMenuItem = new ToolStripMenuItem();
      fileExitToolStripMenuItem = new ToolStripMenuItem();
      fileNewToolStripMenuItem = new ToolStripMenuItem();
      fileOpenContainingFolderToolStripMenuItem = new ToolStripMenuItem();
      fileOpenToolStripMenuItem = new ToolStripMenuItem();
      fileSaveACopyAsToolStripMenuItem = new ToolStripMenuItem();
      fileSaveAllToolStripMenuItem = new ToolStripMenuItem();
      fileSaveAsToolStripMenuItem = new ToolStripMenuItem();
      fileSaveToolStripMenuItem = new ToolStripMenuItem();
      fileToolStripLabel = new ToolStripDropDownButton();
      fileToolStripSeparator1 = new ToolStripSeparator();
      helpAboutToolStripMenuItem = new ToolStripMenuItem();
      helpApplicationWebsiteToolStripMenuItem = new ToolStripMenuItem();
      helpCommandLineArgumentsToolStripMenuItem = new ToolStripMenuItem();
      helpToolStripDropDownButton = new ToolStripDropDownButton();
      helpToolStripSeparator1 = new ToolStripSeparator();
      helpToolStripSeparator2 = new ToolStripSeparator();
      helpWebsiteToolStripMenuItem = new ToolStripMenuItem();
      repeaterExportToClipboardToolStripMenuItem = new ToolStripMenuItem();
      repeaterExportToScriptToolStripMenuItem = new ToolStripMenuItem();
      repeaterExportToXMLToolStripMenuItem = new ToolStripMenuItem();
      repeaterFindToolStripMenuItemDropDown = new ToolStripMenuItem();
      repeaterImportFromClipboardToolStripMenuItem = new ToolStripMenuItem();
      repeaterImportFromScriptToolStripMenuItem = new ToolStripMenuItem();
      repeaterImportFromXMLToolStripMenuItem = new ToolStripMenuItem();
      repeaterRedoToolStripMenuItem = new ToolStripMenuItem();
      repeaterRestartToolStripMenuItem = new ToolStripMenuItem();
      repeaterSelectAllToolStripMenuItem = new ToolStripMenuItem();
      repeaterSelectToolStripMenuItemDropDown = new ToolStripMenuItem();
      repeaterStartToolStripMenuItem = new ToolStripMenuItem();
      repeaterStopToolStripMenuItem = new ToolStripMenuItem();
      repeaterToolStripDropDownButton = new ToolStripDropDownButton();
      repeaterToolStripSeparator1 = new ToolStripSeparator();
      repeaterToolStripSeparator2 = new ToolStripSeparator();
      repeaterToolStripSeparator3 = new ToolStripSeparator();
      repeaterUndoToolStripMenuItem = new ToolStripMenuItem();
      settingsPreferLegacyApplicationToolStripMenuItem = new ToolStripMenuItem();
      settingsPreferModernApplicationToolStripMenuItem = new ToolStripMenuItem();
      settingsSetApplicationPathToolStripMenuItem = new ToolStripMenuItem();
      settingsStartAllRepeatersOnLoadToolStripMenuItem = new ToolStripMenuItem();
      settingsToggleBogusModeToolStripMenuItem = new ToolStripMenuItem();
      settingsToggleSafeModeToolStripMenuItem = new ToolStripMenuItem();
      settingsToolStripButton = new ToolStripDropDownButton();
      settingsToolStripSeparator1 = new ToolStripSeparator();
      settingsToolStripSeparator2 = new ToolStripSeparator();
      toolStripMenuItem1 = new ToolStripMenuItem();
      viewAlwaysOnTopToolStripMenuItem = new ToolStripMenuItem();
      viewPreferDarkThemeToolStripMenuItem = new ToolStripMenuItem();
      viewPreferSystemThemeToolStripMenuItem = new ToolStripMenuItem();
      viewToggleFullScreenModeToolStripMenuItem = new ToolStripMenuItem();
      viewToolStripLabel = new ToolStripDropDownButton();
      viewToolStripSeparator1 = new ToolStripSeparator();
      windowSortByToolStripMenuItem = new ToolStripMenuItem();
      windowToolStripSeparator1 = new ToolStripSeparator();
      windowWindowsToolStripMenuItem = new ToolStripMenuItem();
      windowWindowToolStripDropDownButton = new ToolStripDropDownButton();
      toolStrip1.SuspendLayout();
      SuspendLayout();
      // 
      // toolStrip1
      // 
      toolStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripLabel, deviceToolStripLabel, repeaterToolStripDropDownButton, viewToolStripLabel, settingsToolStripButton, windowWindowToolStripDropDownButton, helpToolStripDropDownButton });
      toolStrip1.Location = new Point(0, 0);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Size = new Size(686, 25);
      toolStrip1.TabIndex = 0;
      // 
      // fileToolStripLabel
      // 
      fileToolStripLabel.AutoToolTip = false;
      fileToolStripLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileToolStripLabel.DropDownItems.AddRange(new ToolStripItem[] { fileNewToolStripMenuItem, fileOpenToolStripMenuItem, fileOpenContainingFolderToolStripMenuItem, fileSaveToolStripMenuItem, fileSaveAsToolStripMenuItem, fileSaveACopyAsToolStripMenuItem, fileSaveAllToolStripMenuItem, fileCloseToolStripMenuItem, fileCloseAllToolStripMenuItem, fileCloseMultipleToolStripMenuItem, fileToolStripSeparator1, fileExitToolStripMenuItem });
      fileToolStripLabel.Name = "fileToolStripLabel";
      fileToolStripLabel.Size = new Size(38, 22);
      fileToolStripLabel.Text = "File";
      // 
      // fileNewToolStripMenuItem
      // 
      fileNewToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileNewToolStripMenuItem.Name = "fileNewToolStripMenuItem";
      fileNewToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
      fileNewToolStripMenuItem.Size = new Size(201, 22);
      fileNewToolStripMenuItem.Text = "New";
      fileNewToolStripMenuItem.Click += fileNewToolStripMenuItem_Click;
      // 
      // fileOpenToolStripMenuItem
      // 
      fileOpenToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileOpenToolStripMenuItem.Name = "fileOpenToolStripMenuItem";
      fileOpenToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
      fileOpenToolStripMenuItem.Size = new Size(201, 22);
      fileOpenToolStripMenuItem.Text = "Open...";
      fileOpenToolStripMenuItem.Click += fileOpenToolStripMenuItem_Click;
      // 
      // fileOpenContainingFolderToolStripMenuItem
      // 
      fileOpenContainingFolderToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileOpenContainingFolderToolStripMenuItem.Name = "fileOpenContainingFolderToolStripMenuItem";
      fileOpenContainingFolderToolStripMenuItem.Size = new Size(201, 22);
      fileOpenContainingFolderToolStripMenuItem.Text = "Open Containing Folder";
      fileOpenContainingFolderToolStripMenuItem.Click += fileOpenContainingFolderToolStripMenuItem_Click;
      // 
      // fileSaveToolStripMenuItem
      // 
      fileSaveToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileSaveToolStripMenuItem.Name = "fileSaveToolStripMenuItem";
      fileSaveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
      fileSaveToolStripMenuItem.Size = new Size(201, 22);
      fileSaveToolStripMenuItem.Text = "Save";
      fileSaveToolStripMenuItem.Click += fileSaveToolStripMenuItem_Click;
      // 
      // fileSaveAsToolStripMenuItem
      // 
      fileSaveAsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileSaveAsToolStripMenuItem.Name = "fileSaveAsToolStripMenuItem";
      fileSaveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.S;
      fileSaveAsToolStripMenuItem.Size = new Size(201, 22);
      fileSaveAsToolStripMenuItem.Text = "Save As...";
      fileSaveAsToolStripMenuItem.Click += fileSaveAsToolStripMenuItem_Click;
      // 
      // fileSaveACopyAsToolStripMenuItem
      // 
      fileSaveACopyAsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileSaveACopyAsToolStripMenuItem.Name = "fileSaveACopyAsToolStripMenuItem";
      fileSaveACopyAsToolStripMenuItem.Size = new Size(201, 22);
      fileSaveACopyAsToolStripMenuItem.Text = "Save a Copy As...";
      fileSaveACopyAsToolStripMenuItem.Click += fileSaveACopyAsToolStripMenuItem_Click;
      // 
      // fileSaveAllToolStripMenuItem
      // 
      fileSaveAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileSaveAllToolStripMenuItem.Name = "fileSaveAllToolStripMenuItem";
      fileSaveAllToolStripMenuItem.Size = new Size(201, 22);
      fileSaveAllToolStripMenuItem.Text = "Save All";
      fileSaveAllToolStripMenuItem.Click += fileSaveAllToolStripMenuItem_Click;
      // 
      // fileCloseToolStripMenuItem
      // 
      fileCloseToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileCloseToolStripMenuItem.Name = "fileCloseToolStripMenuItem";
      fileCloseToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
      fileCloseToolStripMenuItem.Size = new Size(201, 22);
      fileCloseToolStripMenuItem.Text = "Close";
      fileCloseToolStripMenuItem.Click += fileCloseToolStripMenuItem_Click;
      // 
      // fileCloseAllToolStripMenuItem
      // 
      fileCloseAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileCloseAllToolStripMenuItem.Name = "fileCloseAllToolStripMenuItem";
      fileCloseAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.W;
      fileCloseAllToolStripMenuItem.Size = new Size(201, 22);
      fileCloseAllToolStripMenuItem.Text = "Close All";
      fileCloseAllToolStripMenuItem.Click += fileCloseAllToolStripMenuItem_Click;
      // 
      // fileCloseMultipleToolStripMenuItem
      // 
      fileCloseMultipleToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileCloseMultipleToolStripMenuItem.Name = "fileCloseMultipleToolStripMenuItem";
      fileCloseMultipleToolStripMenuItem.Size = new Size(201, 22);
      fileCloseMultipleToolStripMenuItem.Text = "Close Multiple...";
      fileCloseMultipleToolStripMenuItem.Click += fileCloseMultipleToolStripMenuItem_Click;
      // 
      // fileToolStripSeparator1
      // 
      fileToolStripSeparator1.Name = "fileToolStripSeparator1";
      fileToolStripSeparator1.Size = new Size(198, 6);
      // 
      // fileExitToolStripMenuItem
      // 
      fileExitToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      fileExitToolStripMenuItem.Name = "fileExitToolStripMenuItem";
      fileExitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
      fileExitToolStripMenuItem.Size = new Size(201, 22);
      fileExitToolStripMenuItem.Text = "Exit";
      fileExitToolStripMenuItem.Click += fileExitToolStripMenuItem_Click;
      // 
      // deviceToolStripLabel
      // 
      deviceToolStripLabel.AutoToolTip = false;
      deviceToolStripLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceToolStripLabel.DropDownItems.AddRange(new ToolStripItem[] { deviceUndoToolStripMenuItem, deviceRedoToolStripMenuItem, deviceToolStripSeparator1, deviceSetAsDefaultToolStripMenuItem, deviceEnableToolStripMenuItem, deviceDisableToolStripMenuItem, deviceRefreshToolStripMenuItem, deviceToolStripSeparator2, deviceFindToolStripMenuItem, deviceSelectToolStripMenuItem, deviceSelectAllToolStripMenuItem, deviceSelectRangeToolStripMenuItem, deviceToolStripSeparator3, deviceImportFromClipboardToolStripMenuItem, deviceImportFromXMLToolStripMenuItem, deviceExportToClipboardToolStripMenuItem, deviceExportToXMLToolStripMenuItem });
      deviceToolStripLabel.Name = "deviceToolStripLabel";
      deviceToolStripLabel.Size = new Size(55, 22);
      deviceToolStripLabel.Text = "Device";
      // 
      // deviceUndoToolStripMenuItem
      // 
      deviceUndoToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceUndoToolStripMenuItem.Name = "deviceUndoToolStripMenuItem";
      deviceUndoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
      deviceUndoToolStripMenuItem.Size = new Size(192, 22);
      deviceUndoToolStripMenuItem.Text = "Undo";
      deviceUndoToolStripMenuItem.Click += deviceUndoToolStripMenuItem_Click;
      // 
      // deviceRedoToolStripMenuItem
      // 
      deviceRedoToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceRedoToolStripMenuItem.Name = "deviceRedoToolStripMenuItem";
      deviceRedoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
      deviceRedoToolStripMenuItem.Size = new Size(192, 22);
      deviceRedoToolStripMenuItem.Text = "Redo";
      deviceRedoToolStripMenuItem.Click += deviceRedoToolStripMenuItem_Click;
      // 
      // deviceToolStripSeparator1
      // 
      deviceToolStripSeparator1.Name = "deviceToolStripSeparator1";
      deviceToolStripSeparator1.Size = new Size(189, 6);
      // 
      // deviceSetAsDefaultToolStripMenuItem
      // 
      deviceSetAsDefaultToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceSetAsDefaultToolStripMenuItem.Name = "deviceSetAsDefaultToolStripMenuItem";
      deviceSetAsDefaultToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
      deviceSetAsDefaultToolStripMenuItem.Size = new Size(192, 22);
      deviceSetAsDefaultToolStripMenuItem.Text = "Set As Default";
      deviceSetAsDefaultToolStripMenuItem.Click += deviceSetAsDefaultToolStripMenuItem_Click;
      // 
      // deviceEnableToolStripMenuItem
      // 
      deviceEnableToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceEnableToolStripMenuItem.Name = "deviceEnableToolStripMenuItem";
      deviceEnableToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F3;
      deviceEnableToolStripMenuItem.Size = new Size(192, 22);
      deviceEnableToolStripMenuItem.Text = "Enable";
      deviceEnableToolStripMenuItem.Click += deviceEnableToolStripMenuItem_Click;
      // 
      // deviceDisableToolStripMenuItem
      // 
      deviceDisableToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceDisableToolStripMenuItem.Name = "deviceDisableToolStripMenuItem";
      deviceDisableToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F4;
      deviceDisableToolStripMenuItem.Size = new Size(192, 22);
      deviceDisableToolStripMenuItem.Text = "Disable";
      deviceDisableToolStripMenuItem.Click += deviceDisableToolStripMenuItem_Click;
      // 
      // deviceRefreshToolStripMenuItem
      // 
      deviceRefreshToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceRefreshToolStripMenuItem.Name = "deviceRefreshToolStripMenuItem";
      deviceRefreshToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F5;
      deviceRefreshToolStripMenuItem.Size = new Size(192, 22);
      deviceRefreshToolStripMenuItem.Text = "Refresh";
      deviceRefreshToolStripMenuItem.Click += deviceRefreshToolStripMenuItem_Click;
      // 
      // deviceToolStripSeparator2
      // 
      deviceToolStripSeparator2.Name = "deviceToolStripSeparator2";
      deviceToolStripSeparator2.Size = new Size(189, 6);
      // 
      // deviceFindToolStripMenuItem
      // 
      deviceFindToolStripMenuItem.Name = "deviceFindToolStripMenuItem";
      deviceFindToolStripMenuItem.Size = new Size(192, 22);
      deviceFindToolStripMenuItem.Text = "Find...";
      deviceFindToolStripMenuItem.ToolTipText = "Find Device";
      deviceFindToolStripMenuItem.Click += deviceFindToolStripMenuItem_Click;
      // 
      // deviceSelectToolStripMenuItem
      // 
      deviceSelectToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceSelectToolStripMenuItem.Name = "deviceSelectToolStripMenuItem";
      deviceSelectToolStripMenuItem.Size = new Size(192, 22);
      deviceSelectToolStripMenuItem.Text = "Select...";
      // 
      // deviceSelectAllToolStripMenuItem
      // 
      deviceSelectAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceSelectAllToolStripMenuItem.Name = "deviceSelectAllToolStripMenuItem";
      deviceSelectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
      deviceSelectAllToolStripMenuItem.Size = new Size(192, 22);
      deviceSelectAllToolStripMenuItem.Text = "Select All";
      // 
      // deviceSelectRangeToolStripMenuItem
      // 
      deviceSelectRangeToolStripMenuItem.Name = "deviceSelectRangeToolStripMenuItem";
      deviceSelectRangeToolStripMenuItem.Size = new Size(192, 22);
      deviceSelectRangeToolStripMenuItem.Text = "Select All...";
      // 
      // deviceToolStripSeparator3
      // 
      deviceToolStripSeparator3.Name = "deviceToolStripSeparator3";
      deviceToolStripSeparator3.Size = new Size(189, 6);
      // 
      // deviceImportFromClipboardToolStripMenuItem
      // 
      deviceImportFromClipboardToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceImportFromClipboardToolStripMenuItem.Name = "deviceImportFromClipboardToolStripMenuItem";
      deviceImportFromClipboardToolStripMenuItem.Size = new Size(192, 22);
      deviceImportFromClipboardToolStripMenuItem.Text = "Import from clipboard";
      deviceImportFromClipboardToolStripMenuItem.Click += deviceImportFromClipboardToolStripMenuItem_Click;
      // 
      // deviceImportFromXMLToolStripMenuItem
      // 
      deviceImportFromXMLToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceImportFromXMLToolStripMenuItem.Name = "deviceImportFromXMLToolStripMenuItem";
      deviceImportFromXMLToolStripMenuItem.Size = new Size(192, 22);
      deviceImportFromXMLToolStripMenuItem.Text = "Import from XML";
      deviceImportFromXMLToolStripMenuItem.Click += deviceImportFromXMLToolStripMenuItem_Click;
      // 
      // deviceExportToClipboardToolStripMenuItem
      // 
      deviceExportToClipboardToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceExportToClipboardToolStripMenuItem.Name = "deviceExportToClipboardToolStripMenuItem";
      deviceExportToClipboardToolStripMenuItem.Size = new Size(192, 22);
      deviceExportToClipboardToolStripMenuItem.Text = "Export to clipboard";
      deviceExportToClipboardToolStripMenuItem.Click += deviceExportToClipboardToolStripMenuItem_Click;
      // 
      // deviceExportToXMLToolStripMenuItem
      // 
      deviceExportToXMLToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      deviceExportToXMLToolStripMenuItem.Name = "deviceExportToXMLToolStripMenuItem";
      deviceExportToXMLToolStripMenuItem.Size = new Size(192, 22);
      deviceExportToXMLToolStripMenuItem.Text = "Export to XML";
      deviceExportToXMLToolStripMenuItem.Click += deviceExportToXMLToolStripMenuItem_Click;
      // 
      // repeaterToolStripDropDownButton
      // 
      repeaterToolStripDropDownButton.AutoToolTip = false;
      repeaterToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { repeaterUndoToolStripMenuItem, repeaterRedoToolStripMenuItem, repeaterToolStripSeparator1, repeaterFindToolStripMenuItemDropDown, repeaterSelectToolStripMenuItemDropDown, repeaterSelectAllToolStripMenuItem, toolStripMenuItem1, repeaterToolStripSeparator2, repeaterStartToolStripMenuItem, repeaterStopToolStripMenuItem, repeaterRestartToolStripMenuItem, repeaterToolStripSeparator3, repeaterImportFromClipboardToolStripMenuItem, repeaterImportFromScriptToolStripMenuItem, repeaterImportFromXMLToolStripMenuItem, repeaterExportToClipboardToolStripMenuItem, repeaterExportToScriptToolStripMenuItem, repeaterExportToXMLToolStripMenuItem });
      repeaterToolStripDropDownButton.Name = "repeaterToolStripDropDownButton";
      repeaterToolStripDropDownButton.Size = new Size(66, 22);
      repeaterToolStripDropDownButton.Text = "Repeater";
      // 
      // repeaterUndoToolStripMenuItem
      // 
      repeaterUndoToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterUndoToolStripMenuItem.Name = "repeaterUndoToolStripMenuItem";
      repeaterUndoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Z;
      repeaterUndoToolStripMenuItem.Size = new Size(196, 22);
      repeaterUndoToolStripMenuItem.Text = "Undo";
      repeaterUndoToolStripMenuItem.Click += repeaterUndoToolStripMenuItem_Click;
      // 
      // repeaterRedoToolStripMenuItem
      // 
      repeaterRedoToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterRedoToolStripMenuItem.Name = "repeaterRedoToolStripMenuItem";
      repeaterRedoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Y;
      repeaterRedoToolStripMenuItem.Size = new Size(196, 22);
      repeaterRedoToolStripMenuItem.Text = "Redo";
      repeaterRedoToolStripMenuItem.Click += repeaterRedoToolStripMenuItem_Click;
      // 
      // repeaterToolStripSeparator1
      // 
      repeaterToolStripSeparator1.Name = "repeaterToolStripSeparator1";
      repeaterToolStripSeparator1.Size = new Size(193, 6);
      // 
      // repeaterFindToolStripMenuItemDropDown
      // 
      repeaterFindToolStripMenuItemDropDown.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterFindToolStripMenuItemDropDown.Name = "repeaterFindToolStripMenuItemDropDown";
      repeaterFindToolStripMenuItemDropDown.ShortcutKeys = Keys.Control | Keys.Shift | Keys.F;
      repeaterFindToolStripMenuItemDropDown.Size = new Size(196, 22);
      repeaterFindToolStripMenuItemDropDown.Text = "Find...";
      // 
      // repeaterSelectToolStripMenuItemDropDown
      // 
      repeaterSelectToolStripMenuItemDropDown.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterSelectToolStripMenuItemDropDown.Name = "repeaterSelectToolStripMenuItemDropDown";
      repeaterSelectToolStripMenuItemDropDown.Size = new Size(196, 22);
      repeaterSelectToolStripMenuItemDropDown.Text = "Select...";
      // 
      // repeaterSelectAllToolStripMenuItem
      // 
      repeaterSelectAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterSelectAllToolStripMenuItem.Name = "repeaterSelectAllToolStripMenuItem";
      repeaterSelectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.A;
      repeaterSelectAllToolStripMenuItem.Size = new Size(196, 22);
      repeaterSelectAllToolStripMenuItem.Text = "Select All";
      repeaterSelectAllToolStripMenuItem.Click += repeaterSelectAllToolStripMenuItem_Click;
      // 
      // toolStripMenuItem1
      // 
      toolStripMenuItem1.Name = "toolStripMenuItem1";
      toolStripMenuItem1.Size = new Size(196, 22);
      toolStripMenuItem1.Text = "Select All...";
      // 
      // repeaterToolStripSeparator2
      // 
      repeaterToolStripSeparator2.Name = "repeaterToolStripSeparator2";
      repeaterToolStripSeparator2.Size = new Size(193, 6);
      // 
      // repeaterStartToolStripMenuItem
      // 
      repeaterStartToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterStartToolStripMenuItem.Name = "repeaterStartToolStripMenuItem";
      repeaterStartToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.F3;
      repeaterStartToolStripMenuItem.Size = new Size(196, 22);
      repeaterStartToolStripMenuItem.Text = "Start";
      repeaterStartToolStripMenuItem.Click += repeaterStartToolStripMenuItem_Click;
      // 
      // repeaterStopToolStripMenuItem
      // 
      repeaterStopToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterStopToolStripMenuItem.Name = "repeaterStopToolStripMenuItem";
      repeaterStopToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.F4;
      repeaterStopToolStripMenuItem.Size = new Size(196, 22);
      repeaterStopToolStripMenuItem.Text = "Stop";
      repeaterStopToolStripMenuItem.Click += repeaterStopToolStripMenuItem_Click;
      // 
      // repeaterRestartToolStripMenuItem
      // 
      repeaterRestartToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterRestartToolStripMenuItem.Name = "repeaterRestartToolStripMenuItem";
      repeaterRestartToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.F5;
      repeaterRestartToolStripMenuItem.Size = new Size(196, 22);
      repeaterRestartToolStripMenuItem.Text = "Restart";
      repeaterRestartToolStripMenuItem.Click += repeaterRestartToolStripMenuItem_Click;
      // 
      // repeaterToolStripSeparator3
      // 
      repeaterToolStripSeparator3.Name = "repeaterToolStripSeparator3";
      repeaterToolStripSeparator3.Size = new Size(193, 6);
      // 
      // repeaterImportFromClipboardToolStripMenuItem
      // 
      repeaterImportFromClipboardToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterImportFromClipboardToolStripMenuItem.Name = "repeaterImportFromClipboardToolStripMenuItem";
      repeaterImportFromClipboardToolStripMenuItem.Size = new Size(196, 22);
      repeaterImportFromClipboardToolStripMenuItem.Text = "Import from clipboard";
      repeaterImportFromClipboardToolStripMenuItem.Click += repeaterImportFromClipboardToolStripMenuItem_Click;
      // 
      // repeaterImportFromScriptToolStripMenuItem
      // 
      repeaterImportFromScriptToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterImportFromScriptToolStripMenuItem.Name = "repeaterImportFromScriptToolStripMenuItem";
      repeaterImportFromScriptToolStripMenuItem.Size = new Size(196, 22);
      repeaterImportFromScriptToolStripMenuItem.Text = "Import from script";
      repeaterImportFromScriptToolStripMenuItem.Click += repeaterImportFromScriptToolStripMenuItem_Click;
      // 
      // repeaterImportFromXMLToolStripMenuItem
      // 
      repeaterImportFromXMLToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterImportFromXMLToolStripMenuItem.Name = "repeaterImportFromXMLToolStripMenuItem";
      repeaterImportFromXMLToolStripMenuItem.Size = new Size(196, 22);
      repeaterImportFromXMLToolStripMenuItem.Text = "Import from XML";
      repeaterImportFromXMLToolStripMenuItem.Click += repeaterImportFromXMLToolStripMenuItem_Click;
      // 
      // repeaterExportToClipboardToolStripMenuItem
      // 
      repeaterExportToClipboardToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterExportToClipboardToolStripMenuItem.Name = "repeaterExportToClipboardToolStripMenuItem";
      repeaterExportToClipboardToolStripMenuItem.Size = new Size(196, 22);
      repeaterExportToClipboardToolStripMenuItem.Text = "Export to clipboard";
      repeaterExportToClipboardToolStripMenuItem.Click += repeaterExportToClipboardToolStripMenuItem_Click;
      // 
      // repeaterExportToScriptToolStripMenuItem
      // 
      repeaterExportToScriptToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterExportToScriptToolStripMenuItem.Name = "repeaterExportToScriptToolStripMenuItem";
      repeaterExportToScriptToolStripMenuItem.Size = new Size(196, 22);
      repeaterExportToScriptToolStripMenuItem.Text = "Export to script";
      repeaterExportToScriptToolStripMenuItem.Click += repeaterExportToScriptToolStripMenuItem_Click;
      // 
      // repeaterExportToXMLToolStripMenuItem
      // 
      repeaterExportToXMLToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      repeaterExportToXMLToolStripMenuItem.Name = "repeaterExportToXMLToolStripMenuItem";
      repeaterExportToXMLToolStripMenuItem.Size = new Size(196, 22);
      repeaterExportToXMLToolStripMenuItem.Text = "Export to XML";
      repeaterExportToXMLToolStripMenuItem.Click += repeaterExportToXMLToolStripMenuItem_Click;
      // 
      // viewToolStripLabel
      // 
      viewToolStripLabel.AutoToolTip = false;
      viewToolStripLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
      viewToolStripLabel.DropDownItems.AddRange(new ToolStripItem[] { viewAlwaysOnTopToolStripMenuItem, viewToggleFullScreenModeToolStripMenuItem, viewToolStripSeparator1, viewPreferDarkThemeToolStripMenuItem, viewPreferSystemThemeToolStripMenuItem });
      viewToolStripLabel.Name = "viewToolStripLabel";
      viewToolStripLabel.Size = new Size(45, 22);
      viewToolStripLabel.Text = "View";
      // 
      // viewAlwaysOnTopToolStripMenuItem
      // 
      viewAlwaysOnTopToolStripMenuItem.CheckOnClick = true;
      viewAlwaysOnTopToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      viewAlwaysOnTopToolStripMenuItem.Name = "viewAlwaysOnTopToolStripMenuItem";
      viewAlwaysOnTopToolStripMenuItem.Size = new Size(228, 22);
      viewAlwaysOnTopToolStripMenuItem.Text = "Always on Top";
      viewAlwaysOnTopToolStripMenuItem.Click += viewAlwaysOnTopToolStripMenuItem_Click;
      // 
      // viewToggleFullScreenModeToolStripMenuItem
      // 
      viewToggleFullScreenModeToolStripMenuItem.CheckOnClick = true;
      viewToggleFullScreenModeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      viewToggleFullScreenModeToolStripMenuItem.Name = "viewToggleFullScreenModeToolStripMenuItem";
      viewToggleFullScreenModeToolStripMenuItem.ShortcutKeys = Keys.F11;
      viewToggleFullScreenModeToolStripMenuItem.Size = new Size(228, 22);
      viewToggleFullScreenModeToolStripMenuItem.Text = "Toggle Full Screen Mode";
      viewToggleFullScreenModeToolStripMenuItem.Click += viewToggleFullScreenModeToolStripMenuItem_Click;
      // 
      // viewToolStripSeparator1
      // 
      viewToolStripSeparator1.Name = "viewToolStripSeparator1";
      viewToolStripSeparator1.Size = new Size(225, 6);
      // 
      // viewPreferDarkThemeToolStripMenuItem
      // 
      viewPreferDarkThemeToolStripMenuItem.CheckOnClick = true;
      viewPreferDarkThemeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      viewPreferDarkThemeToolStripMenuItem.Name = "viewPreferDarkThemeToolStripMenuItem";
      viewPreferDarkThemeToolStripMenuItem.Size = new Size(228, 22);
      viewPreferDarkThemeToolStripMenuItem.Text = "Prefer Dark Theme";
      viewPreferDarkThemeToolStripMenuItem.Click += viewPreferDarkThemeToolStripMenuItem_Click;
      // 
      // viewPreferSystemThemeToolStripMenuItem
      // 
      viewPreferSystemThemeToolStripMenuItem.CheckOnClick = true;
      viewPreferSystemThemeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      viewPreferSystemThemeToolStripMenuItem.Name = "viewPreferSystemThemeToolStripMenuItem";
      viewPreferSystemThemeToolStripMenuItem.Size = new Size(228, 22);
      viewPreferSystemThemeToolStripMenuItem.Text = "Prefer System Theme";
      viewPreferSystemThemeToolStripMenuItem.Click += viewPreferSystemThemeToolStripMenuItem_Click;
      // 
      // settingsToolStripButton
      // 
      settingsToolStripButton.AutoToolTip = false;
      settingsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
      settingsToolStripButton.DropDownItems.AddRange(new ToolStripItem[] { settingsStartAllRepeatersOnLoadToolStripMenuItem, settingsToolStripSeparator1, settingsPreferModernApplicationToolStripMenuItem, settingsPreferLegacyApplicationToolStripMenuItem, settingsSetApplicationPathToolStripMenuItem, settingsToolStripSeparator2, settingsToggleBogusModeToolStripMenuItem, settingsToggleSafeModeToolStripMenuItem });
      settingsToolStripButton.ImageTransparentColor = Color.Magenta;
      settingsToolStripButton.Name = "settingsToolStripButton";
      settingsToolStripButton.Size = new Size(62, 22);
      settingsToolStripButton.Text = "Settings";
      // 
      // settingsStartAllRepeatersOnLoadToolStripMenuItem
      // 
      settingsStartAllRepeatersOnLoadToolStripMenuItem.CheckOnClick = true;
      settingsStartAllRepeatersOnLoadToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      settingsStartAllRepeatersOnLoadToolStripMenuItem.Name = "settingsStartAllRepeatersOnLoadToolStripMenuItem";
      settingsStartAllRepeatersOnLoadToolStripMenuItem.Size = new Size(262, 22);
      settingsStartAllRepeatersOnLoadToolStripMenuItem.Text = "Start All Audio Repeaters on Startup";
      settingsStartAllRepeatersOnLoadToolStripMenuItem.Click += settingsStartAllRepeatersOnLoadToolStripMenuItem_Click;
      // 
      // settingsToolStripSeparator1
      // 
      settingsToolStripSeparator1.Name = "settingsToolStripSeparator1";
      settingsToolStripSeparator1.Size = new Size(259, 6);
      // 
      // settingsPreferModernApplicationToolStripMenuItem
      // 
      settingsPreferModernApplicationToolStripMenuItem.CheckOnClick = true;
      settingsPreferModernApplicationToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      settingsPreferModernApplicationToolStripMenuItem.Name = "settingsPreferModernApplicationToolStripMenuItem";
      settingsPreferModernApplicationToolStripMenuItem.Size = new Size(262, 22);
      settingsPreferModernApplicationToolStripMenuItem.Text = "Prefer Modern Audio Repeater";
      settingsPreferModernApplicationToolStripMenuItem.ToolTipText = "Uses Kernel Streaming (KS)";
      settingsPreferModernApplicationToolStripMenuItem.Click += settingsPreferModernApplicationToolStripMenuItem_Click;
      // 
      // settingsPreferLegacyApplicationToolStripMenuItem
      // 
      settingsPreferLegacyApplicationToolStripMenuItem.CheckOnClick = true;
      settingsPreferLegacyApplicationToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      settingsPreferLegacyApplicationToolStripMenuItem.Name = "settingsPreferLegacyApplicationToolStripMenuItem";
      settingsPreferLegacyApplicationToolStripMenuItem.Size = new Size(262, 22);
      settingsPreferLegacyApplicationToolStripMenuItem.Text = "Prefer Legacy Audio Repeater";
      settingsPreferLegacyApplicationToolStripMenuItem.ToolTipText = "Uses Multimedia Extensions (MME)";
      settingsPreferLegacyApplicationToolStripMenuItem.Click += settingsPreferLegacyApplicationToolStripMenuItem_Click;
      // 
      // settingsSetApplicationPathToolStripMenuItem
      // 
      settingsSetApplicationPathToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      settingsSetApplicationPathToolStripMenuItem.Name = "settingsSetApplicationPathToolStripMenuItem";
      settingsSetApplicationPathToolStripMenuItem.Size = new Size(262, 22);
      settingsSetApplicationPathToolStripMenuItem.Text = "Set Audio Repeater Executable Path";
      settingsSetApplicationPathToolStripMenuItem.Click += settingsSetApplicationPathToolStripMenuItem_Click;
      // 
      // settingsToolStripSeparator2
      // 
      settingsToolStripSeparator2.Name = "settingsToolStripSeparator2";
      settingsToolStripSeparator2.Size = new Size(259, 6);
      // 
      // settingsToggleBogusModeToolStripMenuItem
      // 
      settingsToggleBogusModeToolStripMenuItem.CheckOnClick = true;
      settingsToggleBogusModeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      settingsToggleBogusModeToolStripMenuItem.Name = "settingsToggleBogusModeToolStripMenuItem";
      settingsToggleBogusModeToolStripMenuItem.Size = new Size(262, 22);
      settingsToggleBogusModeToolStripMenuItem.Text = "Toggle Bogus Mode";
      settingsToggleBogusModeToolStripMenuItem.Click += settingsToggleBogusModeToolStripMenuItem_Click;
      // 
      // settingsToggleSafeModeToolStripMenuItem
      // 
      settingsToggleSafeModeToolStripMenuItem.Checked = true;
      settingsToggleSafeModeToolStripMenuItem.CheckOnClick = true;
      settingsToggleSafeModeToolStripMenuItem.CheckState = CheckState.Checked;
      settingsToggleSafeModeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      settingsToggleSafeModeToolStripMenuItem.Name = "settingsToggleSafeModeToolStripMenuItem";
      settingsToggleSafeModeToolStripMenuItem.Size = new Size(262, 22);
      settingsToggleSafeModeToolStripMenuItem.Text = "Toggle Safe Mode";
      settingsToggleSafeModeToolStripMenuItem.Click += settingsToggleSafeModeToolStripMenuItem_Click;
      // 
      // windowWindowToolStripDropDownButton
      // 
      windowWindowToolStripDropDownButton.AutoToolTip = false;
      windowWindowToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
      windowWindowToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { windowSortByToolStripMenuItem, windowWindowsToolStripMenuItem, windowToolStripSeparator1 });
      windowWindowToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
      windowWindowToolStripDropDownButton.Name = "windowWindowToolStripDropDownButton";
      windowWindowToolStripDropDownButton.Size = new Size(64, 22);
      windowWindowToolStripDropDownButton.Text = "Window";
      // 
      // windowSortByToolStripMenuItem
      // 
      windowSortByToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      windowSortByToolStripMenuItem.Name = "windowSortByToolStripMenuItem";
      windowSortByToolStripMenuItem.Size = new Size(132, 22);
      windowSortByToolStripMenuItem.Text = "Sort By";
      windowSortByToolStripMenuItem.Click += windowSortByToolStripMenuItem_Click;
      // 
      // windowWindowsToolStripMenuItem
      // 
      windowWindowsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      windowWindowsToolStripMenuItem.Name = "windowWindowsToolStripMenuItem";
      windowWindowsToolStripMenuItem.Size = new Size(132, 22);
      windowWindowsToolStripMenuItem.Text = "Windows...";
      windowWindowsToolStripMenuItem.Click += windowWindowsToolStripMenuItem_Click;
      // 
      // windowToolStripSeparator1
      // 
      windowToolStripSeparator1.Name = "windowToolStripSeparator1";
      windowToolStripSeparator1.Size = new Size(129, 6);
      // 
      // helpToolStripDropDownButton
      // 
      helpToolStripDropDownButton.AutoToolTip = false;
      helpToolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
      helpToolStripDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { helpCommandLineArgumentsToolStripMenuItem, helpToolStripSeparator1, helpWebsiteToolStripMenuItem, helpApplicationWebsiteToolStripMenuItem, helpToolStripSeparator2, helpAboutToolStripMenuItem });
      helpToolStripDropDownButton.ImageTransparentColor = Color.Magenta;
      helpToolStripDropDownButton.Name = "helpToolStripDropDownButton";
      helpToolStripDropDownButton.Size = new Size(45, 22);
      helpToolStripDropDownButton.Text = "Help";
      // 
      // helpCommandLineArgumentsToolStripMenuItem
      // 
      helpCommandLineArgumentsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      helpCommandLineArgumentsToolStripMenuItem.Name = "helpCommandLineArgumentsToolStripMenuItem";
      helpCommandLineArgumentsToolStripMenuItem.Size = new Size(218, 22);
      helpCommandLineArgumentsToolStripMenuItem.Text = "Command Line Arguments";
      helpCommandLineArgumentsToolStripMenuItem.Click += helpCommandLineArgumentsToolStripMenuItem_Click;
      // 
      // helpToolStripSeparator1
      // 
      helpToolStripSeparator1.Name = "helpToolStripSeparator1";
      helpToolStripSeparator1.Size = new Size(215, 6);
      // 
      // helpWebsiteToolStripMenuItem
      // 
      helpWebsiteToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      helpWebsiteToolStripMenuItem.Name = "helpWebsiteToolStripMenuItem";
      helpWebsiteToolStripMenuItem.Size = new Size(218, 22);
      helpWebsiteToolStripMenuItem.Text = "{0} Website";
      helpWebsiteToolStripMenuItem.Click += helpWebsiteToolStripMenuItem_Click;
      // 
      // helpApplicationWebsiteToolStripMenuItem
      // 
      helpApplicationWebsiteToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      helpApplicationWebsiteToolStripMenuItem.Name = "helpApplicationWebsiteToolStripMenuItem";
      helpApplicationWebsiteToolStripMenuItem.Size = new Size(218, 22);
      helpApplicationWebsiteToolStripMenuItem.Text = "{0} Website";
      helpApplicationWebsiteToolStripMenuItem.Click += helpApplicationWebsiteToolStripMenuItem_Click;
      // 
      // helpToolStripSeparator2
      // 
      helpToolStripSeparator2.Name = "helpToolStripSeparator2";
      helpToolStripSeparator2.Size = new Size(215, 6);
      // 
      // helpAboutToolStripMenuItem
      // 
      helpAboutToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      helpAboutToolStripMenuItem.Name = "helpAboutToolStripMenuItem";
      helpAboutToolStripMenuItem.ShortcutKeys = Keys.F1;
      helpAboutToolStripMenuItem.Size = new Size(218, 22);
      helpAboutToolStripMenuItem.Text = "About {0}";
      helpAboutToolStripMenuItem.Click += helpAboutToolStripMenuItem_Click;
      // 
      // MainForm
      // 
      AutoScaleDimensions = new SizeF(6F, 13F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(686, 390);
      Controls.Add(toolStrip1);
      DoubleBuffered = true;
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "MainForm";
      Text = "MainForm";
      Load += MainForm_Load;
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private ToolStrip toolStrip1;
    private ToolStripDropDownButton deviceToolStripLabel;
    private ToolStripDropDownButton fileToolStripLabel;
    private ToolStripDropDownButton helpToolStripDropDownButton;
    private ToolStripDropDownButton repeaterToolStripDropDownButton;
    private ToolStripDropDownButton settingsToolStripButton;
    private ToolStripDropDownButton viewToolStripLabel;
    private ToolStripDropDownButton windowWindowToolStripDropDownButton;
    private ToolStripMenuItem deviceDisableToolStripMenuItem;
    private ToolStripMenuItem deviceEnableToolStripMenuItem;
    private ToolStripMenuItem deviceExportToClipboardToolStripMenuItem;
    private ToolStripMenuItem deviceExportToXMLToolStripMenuItem;
    private ToolStripMenuItem deviceFindToolStripMenuItem;
    private ToolStripMenuItem deviceImportFromClipboardToolStripMenuItem;
    private ToolStripMenuItem deviceImportFromXMLToolStripMenuItem;
    private ToolStripMenuItem deviceRedoToolStripMenuItem;
    private ToolStripMenuItem deviceRefreshToolStripMenuItem;
    private ToolStripMenuItem deviceSelectAllToolStripMenuItem;
    private ToolStripMenuItem deviceSelectRangeToolStripMenuItem;
    private ToolStripMenuItem deviceSelectToolStripMenuItem;
    private ToolStripMenuItem deviceSetAsDefaultToolStripMenuItem;
    private ToolStripMenuItem deviceUndoToolStripMenuItem;
    private ToolStripMenuItem fileCloseAllToolStripMenuItem;
    private ToolStripMenuItem fileCloseMultipleToolStripMenuItem;
    private ToolStripMenuItem fileCloseToolStripMenuItem;
    private ToolStripMenuItem fileExitToolStripMenuItem;
    private ToolStripMenuItem fileNewToolStripMenuItem;
    private ToolStripMenuItem fileOpenContainingFolderToolStripMenuItem;
    private ToolStripMenuItem fileOpenToolStripMenuItem;
    private ToolStripMenuItem fileSaveACopyAsToolStripMenuItem;
    private ToolStripMenuItem fileSaveAllToolStripMenuItem;
    private ToolStripMenuItem fileSaveAsToolStripMenuItem;
    private ToolStripMenuItem fileSaveToolStripMenuItem;
    private ToolStripMenuItem helpAboutToolStripMenuItem;
    private ToolStripMenuItem helpApplicationWebsiteToolStripMenuItem;
    private ToolStripMenuItem helpCommandLineArgumentsToolStripMenuItem;
    private ToolStripMenuItem helpWebsiteToolStripMenuItem;
    private ToolStripMenuItem repeaterExportToClipboardToolStripMenuItem;
    private ToolStripMenuItem repeaterExportToScriptToolStripMenuItem;
    private ToolStripMenuItem repeaterExportToXMLToolStripMenuItem;
    private ToolStripMenuItem repeaterFindToolStripMenuItemDropDown;
    private ToolStripMenuItem repeaterImportFromClipboardToolStripMenuItem;
    private ToolStripMenuItem repeaterImportFromScriptToolStripMenuItem;
    private ToolStripMenuItem repeaterImportFromXMLToolStripMenuItem;
    private ToolStripMenuItem repeaterRedoToolStripMenuItem;
    private ToolStripMenuItem repeaterRestartToolStripMenuItem;
    private ToolStripMenuItem repeaterSelectAllToolStripMenuItem;
    private ToolStripMenuItem repeaterSelectToolStripMenuItemDropDown;
    private ToolStripMenuItem repeaterStartToolStripMenuItem;
    private ToolStripMenuItem repeaterStopToolStripMenuItem;
    private ToolStripMenuItem repeaterUndoToolStripMenuItem;
    private ToolStripMenuItem settingsPreferLegacyApplicationToolStripMenuItem;
    private ToolStripMenuItem settingsPreferModernApplicationToolStripMenuItem;
    private ToolStripMenuItem settingsSetApplicationPathToolStripMenuItem;
    private ToolStripMenuItem settingsStartAllRepeatersOnLoadToolStripMenuItem;
    private ToolStripMenuItem settingsToggleBogusModeToolStripMenuItem;
    private ToolStripMenuItem settingsToggleSafeModeToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem viewAlwaysOnTopToolStripMenuItem;
    private ToolStripMenuItem viewPreferDarkThemeToolStripMenuItem;
    private ToolStripMenuItem viewPreferSystemThemeToolStripMenuItem;
    private ToolStripMenuItem viewToggleFullScreenModeToolStripMenuItem;
    private ToolStripMenuItem windowSortByToolStripMenuItem;
    private ToolStripMenuItem windowWindowsToolStripMenuItem;
    private ToolStripSeparator deviceToolStripSeparator1;
    private ToolStripSeparator deviceToolStripSeparator2;
    private ToolStripSeparator deviceToolStripSeparator3;
    private ToolStripSeparator fileToolStripSeparator1;
    private ToolStripSeparator helpToolStripSeparator1;
    private ToolStripSeparator helpToolStripSeparator2;
    private ToolStripSeparator repeaterToolStripSeparator1;
    private ToolStripSeparator repeaterToolStripSeparator2;
    private ToolStripSeparator repeaterToolStripSeparator3;
    private ToolStripSeparator settingsToolStripSeparator1;
    private ToolStripSeparator settingsToolStripSeparator2;
    private ToolStripSeparator viewToolStripSeparator1;
    private ToolStripSeparator windowToolStripSeparator1;
  }
}