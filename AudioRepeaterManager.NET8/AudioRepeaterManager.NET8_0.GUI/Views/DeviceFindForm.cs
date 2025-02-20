using AudioRepeaterManager.NET8_0.Domain.Models;
using AudioRepeaterManager.NET8_0.GUI.Extensions;
using AudioRepeaterManager.NET8_0.Infrastructure.Repositories;
using System.Text;

namespace AudioRepeaterManager.NET8_0.GUI.Forms
{
  public partial class DeviceFindForm : Form
  {
    #region Parameters

    private bool areDeviceFindArrowButtonsEnabled
    {
      get
      {
        return deviceFindDirectionArrowCheckBox.Checked;
      }

      set
      {
        if ((bool?)value is null)
        {
          return;
        }

        deviceFindNextArrowButton.Visible = !value;
        deviceFindNextButton.Visible = value;
        deviceFindPreviousArrowButton.Visible = !value;
      }
    }

    private bool doSelectAny
    {
      get
      {
        return
          !doSelectAnyDuplex
          && !doSelectAnyEnabled
          && !doSelectAnyInput
          && !doSelectAnyOutput
          && !doSelectAnyPresent;
      }
    }

    private bool doSelectAnyDuplex
    {
      get
      {
        return deviceFindDuplexCheckBox.Checked;
      }
    }

    private bool doSelectAnyEnabled
    {
      get
      {
        return deviceFindEnabledCheckBox.Checked;
      }
    }

    private bool doSelectAnyInput
    {
      get
      {
        return deviceFindInputCheckBox.Checked;
      }
    }

    private bool doSelectAnyOutput
    {
      get
      {
        return deviceFindOutputCheckBox.Checked;
      }
    }

    private bool doSelectAnyPresent
    {
      get
      {
        return deviceFindPresentCheckBox.Checked;
      }
    }

    private int deviceFindComboBoxWidth
    {
      get
      {
        return deviceFindComboBox.Width;
      }
      set
      {
        deviceFindComboBox.Width = value;
      }
    }

    private DeviceRepository deviceRepository;
    private List<uint> selectedDeviceIdList;

    /// <summary>
    /// List of tuple (device ID, is Duplex, is Enabled, is Input, is Output,
    /// and is Present).
    /// </summary>
    private List<Tuple<uint, bool, bool, bool, bool, bool>> deviceTupleList;

    private readonly int maxHeight = 212;

    #endregion

    #region Presentation Logic

    /// <summary>
    /// The constructor.
    /// </summary>
    public DeviceFindForm(DeviceRepository deviceRepository)
    {
      this.deviceRepository = deviceRepository;
      this.deviceTupleList = new List<Tuple<uint, bool, bool, bool, bool, bool>>();
      this.selectedDeviceIdList = new List<uint>();
      PreInitializeComponent();
      InitializeComponent();
      PostInitializeComponent();
    }

    /// <summary>
    /// Is this device ComboBox item visible.
    /// </summary>
    /// <param name="isDuplex">True/false is device duplex</param>
    /// <param name="isEnabled">True/false is device enabled</param>
    /// <param name="isInput">True/false is device input</param>
    /// <param name="isOutput">True/false is device output</param>
    /// <param name="isPresent">True/false is device present</param>
    /// <returns>True/false is visible</returns>
    private bool IsThisDeviceComboBoxItemVisible
    (
      bool isDuplex,
      bool isEnabled,
      bool isInput,
      bool isOutput,
      bool isPresent
    )
    {
      return
        doSelectAny
        || (
          doSelectAnyDuplex
          && doSelectAnyDuplex == isDuplex
        ) || (
          doSelectAnyEnabled
          && doSelectAnyEnabled == isEnabled
        ) || (
          doSelectAnyInput
          && doSelectAnyInput == isInput
        ) || (
          doSelectAnyOutput
          && doSelectAnyOutput == isOutput
        ) || (
          doSelectAnyPresent
          && doSelectAnyPresent == isPresent
        );
    }

    private void PostInitializeComponent()
    {
      SetComponentsAbilityProperties();
      SetComponentsTextProperties();
    }
    private void PreInitializeComponent()
    {
      SetFormMaxSize();

      deviceRepository
        .GetAllEnabled()
        .ForEach
        (
          x =>
          {
            {
              Tuple<uint, bool, bool, bool, bool, bool> deviceTuple =
                new Tuple<uint, bool, bool, bool, bool, bool>
                (
                  x.Id,
                  x.IsDuplex,
                  true,
                  x.IsInput,
                  x.IsOutput,
                  x.IsPresent
                );

              deviceTupleList.Add(deviceTuple);
            }
          }
        );

      deviceRepository
        .GetAllDisabled()
        .ForEach
        (
          x =>
          {
            {
              Tuple<uint, bool, bool, bool, bool, bool> deviceTuple =
                new Tuple<uint, bool, bool, bool, bool, bool>
                (
                  x.Id,
                  x.IsDuplex,
                  false,
                  x.IsInput,
                  x.IsOutput,
                  x.IsPresent
                );

              deviceTupleList.Add(deviceTuple);
            }
          }
        );
    }

    private void SetComponentsAbilityProperties()
    {
      areDeviceFindArrowButtonsEnabled = true;
    }

    private void SetComponentsTextProperties()
    {
      if
      (
        deviceFindComboBox
          .Items
          .Count > 0
      )
      {
        deviceFindComboBox
          .Items
          .Clear();
      }

      deviceTupleList
        .ForEach
        (
          x =>
          {
            AppendDeviceFindComboBoxItem
              (
                x.Item1,
                x.Item2,
                x.Item3,
                x.Item4,
                x.Item5,
                x.Item6
              );
          }
        );

      if (deviceFindComboBox.DropDownWidth == 0)
      {
        deviceFindComboBox.DropDownWidth = ComboBoxExtension
          .DropDownWidth(deviceFindComboBox);
      }

      this.Refresh();
    }

    private void SetFormMaxSize()
    {
      this.MaximumSize = new Size(Int32.MaxValue, maxHeight);
    }

    /// <summary>
    /// Add or remove deviceFindComboBox Items if device matches.
    /// </summary>
    /// <param name="deviceId">The device ID</param>
    /// <param name="isDuplex">True/false is device duplex</param>
    /// <param name="isEnabled">True/false is device enabled</param>
    /// <param name="isInput">True/false is device input</param>
    /// <param name="isOutput">True/false is device output</param>
    /// <param name="isPresent">True/false is device present</param>
    private void AppendDeviceFindComboBoxItem
    (
      uint deviceId,
      bool isDuplex,
      bool isEnabled,
      bool isInput,
      bool isOutput,
      bool isPresent
    )
    {
      DeviceModel deviceModel = deviceRepository.Get(deviceId);

      if (deviceModel is null)
      {
        return;
      }

      int id = deviceFindComboBox
        .Items
        .Count;

      int maxIdLength = 7;

      string idWhiteSpace = new string
        (
          ' ',
          maxIdLength - deviceId
            .ToString()
            .Length
        );

      string nameWhiteSpace = new string
      (
        ' ',
        maxIdLength
      );

      string text = string.Format
      (
        "ID:{0}{1},{2}Name: {3}",
        idWhiteSpace,
        deviceId,
        nameWhiteSpace,
        deviceModel.Name
      );

      bool isVisible = IsThisDeviceComboBoxItemVisible
        (
          isDuplex,
          isEnabled,
          isInput,
          isOutput,
          isPresent
        );

      if (!isVisible)
      {
        return;
      }

      bool isSelectable =
        isDuplex != doSelectAnyDuplex
        && isEnabled != doSelectAnyEnabled
        && isInput != doSelectAnyInput
        && isOutput != doSelectAnyOutput
        && isPresent != doSelectAnyPresent;

      if
      (
        deviceFindComboBox
          .Items
          .Contains(text)
      )
      {
        return;
      }

      deviceFindComboBox
        .Items
        .Add(text);
    }

    #endregion

    #region Find logic

    private void deviceFindForm_Load
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceFindCloseButton_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      Close();
    }

    private void deviceFindComboBox_SelectedIndexChanged
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceFindCountButton_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceFindDirectionArrowCheckBox_CheckedChanged(
      object sender,
      EventArgs eventArgs
    )
    {
      areDeviceFindArrowButtonsEnabled = !areDeviceFindArrowButtonsEnabled;
    }

    private void deviceFindDuplexCheckBox_CheckedChanged
    (
      object sender,
      EventArgs eventArgs
    )
    {
      SetComponentsTextProperties();
    }

    private void deviceFindEnabledCheckBox_CheckedChanged
    (
      object sender,
      EventArgs eventArgs
    )
    {
      SetComponentsTextProperties();
    }

    private void deviceFindInputCheckBox_CheckedChanged
    (
      object sender,
      EventArgs eventArgs
    )
    {
      SetComponentsTextProperties();
    }

    private void deviceFindNextArrowButton_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceFindNextButton_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceFindOutputCheckBox_CheckedChanged
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    private void deviceFindInSelectionCheckBox_CheckedChanged
    (
      object sender,
      EventArgs eventArgs
    )
    {
      SetComponentsTextProperties();
    }

    private void deviceFindPresentCheckBox_CheckedChanged
    (
      object sender,
      EventArgs eventArgs
    )
    {
      SetComponentsTextProperties();
    }

    private void deviceFindPreviousArrowButton_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {

    }

    #endregion
  }
}
