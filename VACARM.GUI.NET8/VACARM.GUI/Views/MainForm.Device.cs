using VACARM.Domain.Models;

namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    private bool? deviceToolStripMenuItemsAbility
    {
      set
      {
        if (value is null)
        {
          return;
        }

        bool result = value.Value;

        deviceDisableToolStripMenuItem.Enabled = result;
        deviceEnableToolStripMenuItem.Enabled = result;
        deviceSetAsDefaultToolStripMenuItem.Enabled = result;

        deviceExportToClipboardToolStripMenuItem.Enabled = result;
        deviceExportToXMLToolStripMenuItem.Enabled = result;

        deviceRedoToolStripMenuItem.Enabled = result;
        deviceUndoToolStripMenuItem.Enabled = result;

        deviceFindToolStripMenuItem.Enabled = result;
        deviceSelectAllToolStripMenuItem.Enabled = result;
        deviceSelectAllDisabledToolStripMenuItem.Enabled = result;
        deviceSelectAllDuplexToolStripMenuItem.Enabled = result;
        deviceSelectAllEnabledToolStripMenuItem.Enabled = result;
        deviceSelectAllOutputsToolStripMenuItem.Enabled = result;
        deviceSelectAllInputsToolStripMenuItem.Enabled = result;
        deviceSelectDefaultInputToolStripMenuItem.Enabled = result;
        deviceSelectDefaultOutputToolStripMenuItem.Enabled = result;
        deviceSelectToolStripMenuItem.Enabled = result;
        deviceSelectToolStripMenuItemDropDown.Enabled = result;
      }
    }

    #endregion

    #region Presentation logic

    private ToolStripMenuItem GetDeviceComponentItem(DeviceModel deviceModel)
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

    private void SetDeviceComponentsAbilityProperties()
    {
      deviceToolStripMenuItemsAbility = false;

      //if
      //(
      //  deviceRepositoryHashSet is null
      //  || deviceRepositoryHashSet.Count == 0
      //  || selectedDeviceRepository is null
      //  || selectedDeviceRepository.GetAll().Count == 0
      //)
      //{
      //  return;
      //}

      if
      (
        this.DeviceGroupService
          .GetAll()
          .Count() == 0
      )
      {
        return;
      }


      deviceToolStripMenuItemsAbility = true;

      if
      (
        this.DeviceGroupService
          .GetAllDuplex()
          .Count() == 0
      )
      {
        deviceSelectAllDuplexToolStripMenuItem.Enabled = false;
        deviceSelectDuplexToolStripMenuItem.Enabled = false;
      }

      if
      (
        this.DeviceGroupService
          .GetAllCapture()
          .Count() == 0
      )
      {
        deviceSelectAllInputsToolStripMenuItem.Enabled = false;
        deviceSelectInputToolStripMenuItem.Enabled = false;
      }

      if
      (
        this.DeviceGroupService
          .GetAllRender()
          .Count() == 0
      )
      {
        deviceSelectAllOutputsToolStripMenuItem.Enabled = false;
        deviceSelectOutputToolStripMenuItem.Enabled = false;
      }
    }

    #endregion

    #region Interaction Logic

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

    private void deviceSelectToolStripMenuItem_Click
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

      new DeviceFindForm()
        .ShowDialog();
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

    private void undoToolStripMenuItem_Click
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
