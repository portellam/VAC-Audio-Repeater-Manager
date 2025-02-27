using VACARM.Domain.Models;

namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    internal HashSet<uint> SelectedDeviceIdHashSet
    {
      get
      {
        return this.selectedDeviceIdHashSet;
      }
      set
      {
        this.selectedDeviceIdHashSet = value;
        this.SetDeviceConfirmSelectAbility();
        this.OnPropertyChanged(nameof(this.SelectedDeviceIdHashSet));
      }
    }

    private bool? deviceAbility
    {
      set
      {
        if (value is null)
        {
          return;
        }

        bool result = value.Value;

        this.deviceConfirmSelectToolStripMenuItem
          .Enabled = result;

        this.deviceDisableToolStripMenuItem
          .Enabled = result;

        this.deviceEnableToolStripMenuItem
          .Enabled = result;

        this.deviceSetAsDefaultToolStripMenuItem
          .Enabled = result;

        this.deviceExportToClipboardToolStripMenuItem
          .Enabled = result;

        this.deviceExportToXMLToolStripMenuItem
          .Enabled = result;

        this.deviceRedoToolStripMenuItem
          .Enabled = result;

        this.deviceUndoToolStripMenuItem
          .Enabled = result;

        this.deviceFindToolStripMenuItem
          .Enabled = result;

        this.deviceSelectAllToolStripMenuItem
          .Enabled = result;

        this.deviceSelectAllDisabledToolStripMenuItem
          .Enabled = result;

        this.deviceSelectAllDuplexToolStripMenuItem
          .Enabled = result;

        this.deviceSelectAllEnabledToolStripMenuItem
          .Enabled = result;

        this.deviceSelectAllOutputsToolStripMenuItem
          .Enabled = result;

        this.deviceSelectAllInputsToolStripMenuItem
          .Enabled = result;

        this.deviceSelectDefaultInputToolStripMenuItem
          .Enabled = result;

        this.deviceSelectDefaultOutputToolStripMenuItem
          .Enabled = result;

        this.deviceSelectToolStripMenuItemDropDown
          .Enabled = result;
      }
    }

    private HashSet<uint> selectedDeviceIdHashSet { get; set; } =
      new HashSet<uint>();

    #endregion

    #region Presentation logic

    private ToolStripMenuItem GetDeviceComponent(DeviceModel deviceModel)
    {
      int maxIdLength = 7;

      string idWhiteSpace = new string
        (
          ' ',
          maxIdLength - deviceModel.Id
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
        deviceModel.Id,
        nameWhiteSpace,
        deviceModel.Name
      );

      return new ToolStripMenuItem(text)
      {
        CheckOnClick = true,

        ToolTipText = deviceModel.Id
          .ToString(),
      };
    }

    private void SetDeviceComponents()
    {
      this.DeviceGroupService
        .GetAllCapture()
        .ToList()
        .ForEach
        (
          x =>
          {
            var toolStripMenuItem = GetDeviceComponent(x);

            toolStripMenuItem.CheckedChanged +=
              deviceConfirmSelectToolStripMenuItem_CheckedChanged;

            toolStripMenuItem.CheckState =
              deviceSelectInputToolStripMenuItem.CheckState;

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
            var toolStripMenuItem = GetDeviceComponent(x);

            toolStripMenuItem.CheckedChanged +=
              deviceConfirmSelectToolStripMenuItem_CheckedChanged;

            toolStripMenuItem.CheckState =
              deviceSelectOutputToolStripMenuItem.CheckState;

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
            var toolStripMenuItem = GetDeviceComponent(x);

            toolStripMenuItem.CheckedChanged +=
              deviceConfirmSelectToolStripMenuItem_CheckedChanged;

            toolStripMenuItem.CheckState =
              deviceSelectDuplexToolStripMenuItem.CheckState;

            deviceSelectDuplexToolStripMenuItem
              .DropDownItems
              .Add(toolStripMenuItem);
          }
        );
    }

    private void SetDeviceAbility()
    {
      this.deviceAbility = false;

      if
      (
        this.DeviceGroupService
          .GetAll()
          .Count() == 0
      )
      {
        return;
      }

      this.deviceAbility = true;

      if
      (
        this.DeviceGroupService
          .GetAllDuplex()
          .Count() == 0
      )
      {
        this.deviceSelectAllDuplexToolStripMenuItem.Enabled = false;
        this.deviceSelectDuplexToolStripMenuItem.Enabled = false;
      }

      if
      (
        this.DeviceGroupService
          .GetAllCapture()
          .Count() == 0
      )
      {
        this.deviceSelectAllInputsToolStripMenuItem.Enabled = false;
        this.deviceSelectInputToolStripMenuItem.Enabled = false;
      }

      if
      (
        this.DeviceGroupService
          .GetAllRender()
          .Count() == 0
      )
      {
        this.deviceSelectAllOutputsToolStripMenuItem.Enabled = false;
        this.deviceSelectOutputToolStripMenuItem.Enabled = false;
      }
    }

    private void SetDeviceConfirmSelectAbility()
    {
      var result = this.selectedDeviceIdHashSet
          .Count() > 0;

      this.deviceConfirmSelectToolStripMenuItem
        .Enabled = result;
    }

    #endregion

    #region Interaction Logic


    private void deviceConfirmSelectToolStripMenuItem_CheckedChanged
    (
      object? sender,
      EventArgs e
    )
    {
      if (sender == null)
      {
        return;
      }

      if (sender.GetType() != typeof(ToolStripMenuItem))
      {
        return;
      }

      var toolTipText = (sender as ToolStripMenuItem).ToolTipText;

      if (toolTipText.GetType() != typeof(uint))
      {
        return;
      }

      uint id;

      var result = uint.TryParse
        (
          toolTipText,
          out id
        );

      if (!result)
      {
        return;
      }

      this.SelectedDeviceIdHashSet
        .Add(id);
    }

    private void deviceSelectInputToolStripMenuItem_CheckState
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

    }

    private void deviceSelectOutputToolStripMenuItem_CheckState
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

    }

    private void deviceSelectDuplexToolStripMenuItem_CheckState
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      var enumerable = this.deviceSelectDuplexToolStripMenuItem
        .DropDownItems;

      this.deviceSelectDuplexToolStripMenuItem
        .DropDownItems.Clear();

      foreach (var item in enumerable)
      {
        if (item == null)
        {
          continue;
        }

        if (item.GetType() != typeof(ToolStripMenuItem))
        {
          return;
        }

        ToolStripMenuItem newItem = item as ToolStripMenuItem;
        newItem.Checked = true;

        this.deviceSelectDuplexToolStripMenuItem
          .DropDownItems
          .Add(newItem);
      }

    }

    private void deviceConfirmSelectToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      var result = deviceSelectInputToolStripMenuItem.DropDownItems;
    }

    private void deviceDisableToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceEnableToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceExportToClipboardToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceExportToXMLToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceFindToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      //new DeviceFindForm(selectedDeviceRepository)
      //  .ShowDialog();

      new DeviceFindForm().ShowDialog();
    }

    private void deviceImportFromClipboardToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceImportFromXMLToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

    }

    private void deviceRedoToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceSelectAllToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      var enumerable = this.DeviceGroupService
        .SelectedService
        .GetAllId();

      foreach (var item in enumerable)
      {
        this.SelectedDeviceIdHashSet
          .Add(item);
      }
    }

    private void deviceRefreshToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceSetAsDefaultToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceSelectAllDisabledToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      var enumerable = this.DeviceGroupService
        .GetAllDisabled()
        .Select(x => x.Id);

      foreach (var item in enumerable)
      {
        //TODO: check disabled devices.

        this.SelectedDeviceIdHashSet
          .Add(item);
      }
    }

    private void deviceSelectAllDuplexToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      var enumerable = this.DeviceGroupService
        .GetAllDuplex()
        .Select(x => x.Id);

      foreach (var item in enumerable)
      {
        this.SelectedDeviceIdHashSet
          .Add(item);
      }
    }

    private void deviceSelectAllEnabledToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      var enumerable = this.DeviceGroupService
        .GetAllEnabled()
        .Select(x => x.Id);

      foreach (var item in enumerable)
      {
        //TODO: check enabled devices.

        this.SelectedDeviceIdHashSet
          .Add(item);
      }
    }

    private void deviceSelectAllInputsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      var enumerable = this.DeviceGroupService
        .GetAllCapture()
        .Select(x => x.Id);

      foreach (var item in enumerable)
      {
        this.SelectedDeviceIdHashSet
          .Add(item);
      }
    }

    private void deviceSelectAllOutputsToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }

      var enumerable = this.DeviceGroupService
        .GetAllRender()
        .Select(x => x.Id);

      foreach (var item in enumerable)
      {
        this.SelectedDeviceIdHashSet
          .Add(item);
      }
    }

    private void deviceSelectDefaultInputToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceSelectDefaultOutputToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    private void deviceUndoToolStripMenuItem_Click
    (
      object sender,
      EventArgs eventArgs
    )
    {
      if (sender == null)
      {
        return;
      }


    }

    #endregion
  }
}