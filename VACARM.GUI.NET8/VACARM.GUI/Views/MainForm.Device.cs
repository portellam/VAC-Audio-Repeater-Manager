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

    private void SetDeviceComponents()
    {
      //this.DeviceViewModel
      //  .Deconstruct
      //  (
      //    ref deviceSelectAllToolStripMenuItem,
      //    ref deviceSelectAllAbsentToolStripMenuItem,
      //    ref deviceSelectAllCaptureToolStripMenuItem,
      //    ref deviceSelectAllCommunicationsToolStripMenuItem,
      //    ref deviceSelectAllConsoleToolStripMenuItem,
      //    ref deviceSelectAllDisabledToolStripMenuItem,
      //    ref deviceSelectAllEnabledToolStripMenuItem,
      //    ref deviceSelectAllMultimediaToolStripMenuItem,
      //    ref deviceSelectAllMutedToolStripMenuItem,
      //    ref deviceSelectAllPresentToolStripMenuItem,
      //    ref deviceSelectAllRenderToolStripMenuItem,
      //    ref deviceSelectAllUnmutedToolStripMenuItem,
      //    ref deviceSelectCaptureToolStripMenuItem,
      //    ref deviceSelectDefaultToolStripMenuItem,
      //    ref deviceSelectRenderToolStripMenuItem,
      //  );

      // TODO: run update before SetDeviceComponents() ?
      //this.DeviceViewModel
      //  .Update();
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