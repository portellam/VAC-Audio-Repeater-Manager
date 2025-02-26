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