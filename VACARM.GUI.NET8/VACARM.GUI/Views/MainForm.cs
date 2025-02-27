using System.Diagnostics.CodeAnalysis;
using VACARM.Common;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Views
{
  public partial class MainForm :
    Form
  {
    #region Parameters

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

    #endregion

    #region Presentation logic

    /// <summary>
    /// Constructor.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MainForm()
    {
      this.InitializeComponent();

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

      this.PostInitializeComponent();

      this.windowWindowToolStripDropDownButton  //NOTE: this is a test.
        .DropDownItems
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
      this.SetDeviceComponents();
      this.SetComponentsAbility();
      this.SetComponentsText();
    }

    private void SetComponentsAbility()
    {
      this.SetDeviceAbility();
      this.SetDeviceConfirmSelectAbility();

      if (Environment.OSVersion.Version.Major < 6)
      {
        this.viewPreferSystemThemeToolStripMenuItem
          .Enabled = false;
      }

      if (!Environment.Is64BitOperatingSystem)
      {
        this.settingsPreferModernApplicationToolStripMenuItem
          .Enabled = false;

        this.settingsPreferLegacyApplicationToolStripMenuItem
          .Enabled = false;

        this.settingsPreferLegacyApplication = true;
      }
    }

    private void SetComponentsText()
    {
      this.Text = Info.ApplicationPartialAbbreviatedName;

      this.helpAboutToolStripMenuItem.Text = string.Format
        (
          "About {0}",
          Info.ApplicationPartialAbbreviatedName
        );

      this.helpApplicationWebsiteToolStripMenuItem.Text = string.Format
        (
          "{0} Website",
          Info.ReferencedApplicationName
        );

      this.helpWebsiteToolStripMenuItem.Text = string.Format
        (
          "{0} Website",
          Info.ApplicationPartialAbbreviatedName
        );

      string featureNotAvailableMessage = "N/A: ";

      if
      (
        !this.settingsPreferModernApplicationToolStripMenuItem
          .Enabled
      )
      {
        this.settingsPreferModernApplicationToolStripMenuItem
          .Text = featureNotAvailableMessage
            + this.settingsPreferModernApplicationToolStripMenuItem;
      }

      if
      (
        !this.viewPreferSystemThemeToolStripMenuItem
          .Enabled
      )
      {
        this.viewPreferSystemThemeToolStripMenuItem
          .Text = featureNotAvailableMessage
            + this.viewPreferSystemThemeToolStripMenuItem;
      }
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

    #region Logic

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

    #endregion
  }
}