using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;
using VACARM.GUI.ViewModels;

namespace VACARM.GUI.Views
{
  public partial class MainForm :
    Form,
    INotifyPropertyChanged
  {
    #region Parameters

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Presentation logic

    /// <summary>
    /// Constructor.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MainForm()
    {
      this.InitializeComponent();

      this.DeviceViewModel = new DeviceViewModel
      <
        DeviceGroupService
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
        >,
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

      //this.DeviceViewModel.Update();          //TODO: place this under "Refresh"
      this.Refresh();

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

    public override void Refresh()
    {
      this.SetComponentsAbility();
      this.SetComponentsText();
      this.SetDeviceComponents();
      this.SetHelpComponents();
      base.Refresh();
    }

    private void SetComponentsAbility()
    {
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
      this.Text = Common.Info
        .ApplicationPartialAbbreviatedName;

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

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal void OnPropertyChanged(string propertyName)
    {
      this.PropertyChanged?
        .Invoke
        (
          this,
          new PropertyChangedEventArgs(propertyName)
        );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

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