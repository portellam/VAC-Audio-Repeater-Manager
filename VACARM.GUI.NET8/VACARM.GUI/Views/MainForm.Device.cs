using VACARM.Domain.Models;
using VACARM.GUI.ViewModels;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    internal DeviceViewModel
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
        DeviceModel
      > DeviceViewModel
    { get; set; }

    #endregion

    #region Presentation logic

    private IEnumerable<ToolStripMenuItem>
    GetDeviceModelEnumerableAsToolStripMenuItemEnumerable
    (
      IEnumerable<DeviceModel> modelEnumerable
    )
    {
      if (modelEnumerable == null)
      {
        yield break;
      }

      foreach (var item in modelEnumerable)
      {
        yield return this.GetDeviceModelAsToolStripMenuItem(item);
      }
    }

    private ToolStripMenuItem GetDeviceModelAsToolStripMenuItem
    (DeviceModel deviceModel)
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

    #endregion

    #region Delegate logic

    private void OnUncheckOfSelectInputUncheckAll()
    {
      if (this.deviceSelectInputToolStripMenuItem == null)
      {
        return;
      }

      deviceSelectInputToolStripMenuItem
        .DropDownItemClicked +=
        (
          sender,
          eventArgs
        ) =>
        {
          var anyNotChecked = this.deviceSelectInputToolStripMenuItem
            .DropDownItems
            .Cast<ToolStripMenuItem>()
            .Any(x => !x.Checked);

          if (!anyNotChecked)
          {
            return;
          }

          this.deviceSelectAllDisabledToolStripMenuItem
            .Checked = false;

          this.deviceSelectAllEnabledToolStripMenuItem
            .Checked = false;

          this.deviceSelectAllInputsToolStripMenuItem
            .Checked = false;

          this.deviceSelectAllOutputsToolStripMenuItem
            .Checked = false;

        };
    }

    private void OnUncheckOfSelectOutputUncheckAll()
    {
      if (this.deviceSelectOutputToolStripMenuItem == null)
      {
        return;
      }

      deviceSelectOutputToolStripMenuItem
        .DropDownItemClicked +=
        (
          sender,
          eventArgs
        ) =>
        {
          var anyNotChecked = this.deviceSelectOutputToolStripMenuItem
            .DropDownItems
            .Cast<ToolStripMenuItem>()
            .Any(x => !x.Checked);

          if (!anyNotChecked)
          {
            return;
          }

          this.deviceSelectAllDisabledToolStripMenuItem
            .Checked = false;

          this.deviceSelectAllEnabledToolStripMenuItem
            .Checked = false;

          this.deviceSelectAllInputsToolStripMenuItem
            .Checked = false;

          this.deviceSelectAllOutputsToolStripMenuItem
            .Checked = false;

        };
    }

    private void OnCheckOfSelectAllInputCheckAllInput()
    {
      if (this.deviceSelectInputToolStripMenuItem == null)
      {
        return;
      }

      deviceSelectAllInputsToolStripMenuItem
        .CheckedChanged +=
        (
          sender,
          eventArgs
        ) =>
        {
          var enumerable = this.deviceSelectInputToolStripMenuItem
            .DropDownItems
            .Cast<ToolStripMenuItem>();

          this.deviceSelectInputToolStripMenuItem
            .DropDownItems
            .Clear();

          foreach (var item in enumerable)
          {
            item.Enabled = deviceSelectAllInputsToolStripMenuItem.Enabled;

            this.deviceSelectInputToolStripMenuItem
              .DropDownItems.Add(item);
          }
        };
    }

    private void OnCheckOfSelectAllOutputCheckAllOutput()
    {
      if (this.deviceSelectOutputToolStripMenuItem == null)
      {
        return;
      }

      deviceSelectAllOutputsToolStripMenuItem
        .CheckedChanged +=
        (
          sender,
          eventArgs
        ) =>
        {
          var enumerable = this.deviceSelectOutputToolStripMenuItem
            .DropDownItems
            .Cast<ToolStripMenuItem>();

          this.deviceSelectOutputToolStripMenuItem
            .DropDownItems
            .Clear();

          foreach (var item in enumerable)
          {
            item.Enabled = deviceSelectAllOutputsToolStripMenuItem.Enabled;

            this.deviceSelectOutputToolStripMenuItem
              .DropDownItems.Add(item);
          }
        };
    }

    #endregion

    #region Interaction Logic

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