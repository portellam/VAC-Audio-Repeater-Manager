using VACARM.GUI.Helpers;
using VACARM.Infrastructure.Repositories;
using VACARM.Common;
using VACARM.Application.Services;
using VACARM.Domain.Models;

namespace VACARM.GUI.Views
{
  public partial class MainForm :
    Form
  {
    #region Parameters

    private bool preferX64
    {
      get
      {
        return settingsPreferModernApplicationToolStripMenuItem.Checked;
      }
      set
      {
        settingsPreferModernApplicationToolStripMenuItem.Checked = value;
        settingsPreferLegacyApplicationToolStripMenuItem.Checked = !value;
      }
    }

    private bool preferX86
    {
      get
      {
        return settingsPreferLegacyApplicationToolStripMenuItem.Checked;
      }
      set
      {
        settingsPreferLegacyApplicationToolStripMenuItem.Checked = value;
        settingsPreferModernApplicationToolStripMenuItem.Checked = !value;
      }
    }

    private bool preferDarkTheme
    {
      get
      {
        return viewPreferDarkThemeToolStripMenuItem.Checked;
      }
      set
      {
        viewPreferDarkThemeToolStripMenuItem.Checked = value;
        viewPreferSystemThemeToolStripMenuItem.Checked = !value;
      }
    }

    private bool preferSystemTheme
    {
      get
      {
        return viewPreferSystemThemeToolStripMenuItem.Checked;
      }
      set
      {

        viewPreferSystemThemeToolStripMenuItem.Checked = value;
        viewPreferDarkThemeToolStripMenuItem.Checked = !value;
      }
    }

    //NOTE: this also appears in DeviceFindForm. TODO: find one space to put this!
    private DeviceGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<DeviceModel>,
          DeviceModel
        >
      >,
      BaseService
      <
        BaseRepository<DeviceModel>,
        DeviceModel
      >,
      BaseRepository<DeviceModel>,
      DeviceModel
    > DeviceGroupService
    { get; set; }

    //private DeviceRepository selectedDeviceRepository
    //{
    //  get
    //  {
    //    return deviceRepositoryHashSet
    //      .ElementAtOrDefault(selectedDeviceRepositoryindex);
    //  }
    //}

    //private HashSet<DeviceRepository> deviceRepositoryHashSet;

    #endregion

    #region Presentation logic

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainForm()
    {
      InitializeComponent();
      SetDeviceRepositories();
      PostInitializeComponent();

      windowWindowToolStripDropDownButton.DropDownItems //note: this is a test.
        .Add
        (
          new ToolStripMenuItem()
          {
            Text = "1: Test Window"
          }
        );
    }


    private void PostInitializeComponent()
    {
      SetComponentsItemLists();
      SetComponentsAbilityProperties();
      SetComponentsTextProperties();
    }

    private void SetComponentsAbilityProperties()
    {
      SetDeviceComponentsAbilityProperties();

      if (Environment.OSVersion.Version.Major < 6)
      {
        viewPreferSystemThemeToolStripMenuItem.Enabled = false;
      }

      if (!Environment.Is64BitOperatingSystem)
      {
        settingsPreferModernApplicationToolStripMenuItem.Enabled = false;
        settingsPreferLegacyApplicationToolStripMenuItem.Enabled = false;
        preferX86 = true;
      }
    }

    private void SetComponentsTextProperties()
    {
      Text = Info.ApplicationPartialAbbreviatedName;

      helpAboutToolStripMenuItem.Text = string.Format
        (
          "About {0}",
          Info.ApplicationPartialAbbreviatedName
        );

      helpApplicationWebsiteToolStripMenuItem.Text = string.Format
        (
          "{0} Website",
          Info.ReferencedApplicationName
        );


      helpWebsiteToolStripMenuItem.Text = string.Format
        (
          "{0} Website",
          Info.ApplicationPartialAbbreviatedName
        );

      string featureNotAvailableMessage = "N/A: ";

      if (!settingsPreferModernApplicationToolStripMenuItem.Enabled)
      {
        settingsPreferModernApplicationToolStripMenuItem.Text =
          featureNotAvailableMessage
          + settingsPreferModernApplicationToolStripMenuItem;
      }

      if (!viewPreferSystemThemeToolStripMenuItem.Enabled)
      {
        viewPreferSystemThemeToolStripMenuItem.Text =
          featureNotAvailableMessage
          + viewPreferSystemThemeToolStripMenuItem;
      }
    }

    /// <summary>
    /// The selected device repository index.
    /// Import device repository by system or text file.
    /// </summary>
    private int selectedDeviceRepositoryindex = 0; //NOTE: 2025-02-21, I totally forgot I had this idea until I just made DeviceGroupService.

    private void SetComponentsItemLists()
    {
      SetDeviceRepositories();

      this.DeviceGroupService
        .GetAllCapture()
        .ToList()
        .ForEach
        (
          x =>
          {
            ToolStripMenuItem toolStripMenuItem = GetDeviceComponentItem(x);

            deviceSelectInputToolStripMenuItem
              .DropDownItems
              .Add(toolStripMenuItem);
          }
        );

      this.DeviceGroupService
        .GetAllRender()
        .ToList()
        .ForEach
        (
          x =>
          {
            ToolStripMenuItem toolStripMenuItem = GetDeviceComponentItem(x);

            deviceSelectOutputToolStripMenuItem
              .DropDownItems
              .Add(toolStripMenuItem);
          }
        );

      this.DeviceGroupService
        .GetAllDuplex()
        .ToList()
        .ForEach
        (
          x =>
          {
            ToolStripMenuItem toolStripMenuItem = GetDeviceComponentItem(x);

            deviceSelectDuplexToolStripMenuItem
              .DropDownItems
              .Add(toolStripMenuItem);
          }
        );
    }


    private void SetDeviceRepositories()
    {
      //if
      //(
      //  deviceRepositoryHashSet is null
      //  || deviceRepositoryHashSet.Count == 0
      //)
      //{
      //  //deviceRepositoryHashSet = new HashSet<DeviceRepository>();                    //TODO: update.
      //}

      this.DeviceGroupService =
        new DeviceGroupService
        <
          ReadonlyRepository
          <
            BaseService
            <
              BaseRepository<DeviceModel>,
              DeviceModel
            >
          >,
          BaseService
          <
            BaseRepository<DeviceModel>,
            DeviceModel
          >,
          BaseRepository<DeviceModel>,
          DeviceModel
        >();
    }

    #endregion

    #region Main logic

    private void MainForm_Load
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion

    #region File logic

    private void fileCloseAllToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileCloseMultipleToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileCloseToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }


    private void fileExitToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      // check if it is safe to exit.

      Environment.Exit(0);
    }

    private void fileNewToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileOpenToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      OpenFileDialog openFileDialog = new OpenFileDialog()
      {
        AddExtension = true,
        DefaultExt = ".vacarm",
        CheckFileExists = true,
        CheckPathExists = true,
        InitialDirectory = "C:\\",
        Multiselect = true,
        OkRequiresInteraction = true,
        ShowPreview = true,

      };

      openFileDialog.ShowDialog();

      string fileName = openFileDialog.FileName;
      //send to FileController?
      //file controller populates an instance of the repositories?

      openFileDialog.AddToRecent = true;
      openFileDialog.ShowPreview = true;
    }

    private void fileOpenContainingFolderToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileSaveToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileSaveAsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileSaveACopyAsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void fileSaveAllToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion

    #region Device logic

    private void deviceExportToClipboardToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceExportToXMLToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceImportFromClipboardToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceImportFromXMLToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceRedoToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectAllToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceDisableToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceEnableToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceFindToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      //new DeviceFindForm(selectedDeviceRepository)
      //  .ShowDialog();

      new DeviceFindForm()
        .ShowDialog();
    }

    private void deviceRefreshToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSetAsDefaultToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectAllDisabledToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectAllDuplexToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectAllEnabledToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectAllInputsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectAllOutputsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectDefaultInputToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceSelectDefaultOutputToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void undoToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion

    #region Repeaters logic

    private void repeaterExportToClipboardToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterExportToScriptToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterExportToXMLToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterImportFromClipboardToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterImportFromScriptToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterImportFromXMLToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterRedoToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterSelectToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterSelectAllToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterUndoToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterRestartToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterSelectAllWithAbsentDevicesToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void selectAllWithDisabledDevicesToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void selectAllWithEnabledDevicesToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterSelectAllWithPresentDevicesToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterSelectDevicesToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterSelectInputDeviceToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterSelectOutputDeviceToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterStartToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void repeaterStopToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion

    #region View logic

    private void viewAlwaysOnTopToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }
    private void viewPreferSystemThemeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender is null)
      {
        return;
      }

      if (Environment.OSVersion.Version.Major < 6)
      {
        viewPreferSystemThemeToolStripMenuItem.Enabled = false;
      }

      preferSystemTheme = true;
    }

    private void viewPreferDarkThemeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void viewToggleFullScreenModeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion

    #region Settings logic

    private void settingsPreferModernApplicationToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if
      (
        !Environment.Is64BitOperatingSystem
        || sender is null
      )
      {
        return;
      }

      preferX64 = true;
    }

    private void settingsPreferLegacyApplicationToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if
      (
        !Environment.Is64BitOperatingSystem
        || sender is null
      )
      {
        return;
      }

      preferX86 = true;
    }

    private void settingsSetApplicationPathToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void settingsStartAllRepeatersOnLoadToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void settingsToggleBogusModeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void settingsToggleSafeModeToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion

    #region Help logic

    private void helpAboutToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      new AboutForm()
        .ShowDialog();
    }
    private void helpCommandLineArgumentsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void helpApplicationWebsiteToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      try
      {
        UrlRedirectHelper.GoToSite("https://vac.muzychenko.net");
      }
      catch
      { }
    }

    private void helpWebsiteToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      string projectName = "vac-audio-repeater-manager";

      try
      {
        UrlRedirectHelper
          .GoToSite
          (
            string.Format
            (
              "https://www.github.com/portellam/{0}",
              projectName
            )
          );

        return;
      }
      catch
      { }

      try
      {
        UrlRedirectHelper
          .GoToSite
          (
            string.Format
            (
              "https://www.codeberg.org/portellam/{0}",
              projectName
            )
          );

        return;
      }
      catch
      { }
    }

    #endregion

    #region Window logic

    private void windowSortByToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void windowWindowsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void windowToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion

    public static DialogResult InputBox
    (
      string title,
      string promptText,
      ref string value
    )
    {
      Button buttonOk = new Button()
      {
        DialogResult = DialogResult.OK,
        Text = "OK"
      };

      buttonOk.SetBounds(228, 160, 160, 60);

      Button buttonCancel = new Button()
      {
        DialogResult = DialogResult.Cancel,
        Text = "Cancel"
      };

      buttonCancel.SetBounds(400, 160, 160, 60);

      Form form = new Form()
      {
        AcceptButton = buttonOk,
        CancelButton = buttonCancel,
        ClientSize = new Size(796, 307),
        FormBorderStyle = FormBorderStyle.FixedDialog,
        MinimizeBox = false,
        MaximizeBox = false,
        StartPosition = FormStartPosition.CenterScreen,
        Text = title
      };

      Label label = new Label()
      {
        AutoSize = true,
        Text = promptText
      };

      label.SetBounds(36, 36, 372, 13);

      TextBox textBox = new TextBox();
      textBox.SetBounds(36, 86, 700, 20);

      form
        .Controls
        .AddRange
        (
          new Control[]
          {
            label,
            textBox,
            buttonOk,
            buttonCancel
          }
        );

      DialogResult dialogResult = form.ShowDialog();
      value = textBox.Text;
      return dialogResult;
    }
  }
}