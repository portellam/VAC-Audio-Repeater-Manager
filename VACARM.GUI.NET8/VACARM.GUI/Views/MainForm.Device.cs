﻿using VACARM.Domain.Models;
using VACARM.GUI.Controllers;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    internal DeviceController
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
      > DeviceController
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

    private void SetDeviceComponents()
    {
      var modelEnumerable = this.DeviceGroupService                                     //FIXME: selectedService is not getting anything here.
        .GetAllCapture();

      var enumerable = this.GetDeviceModelEnumerableAsToolStripMenuItemEnumerable
        (modelEnumerable);

      this.SetDeviceSelectToolStripItemCollection
        (
          ref deviceSelectInputToolStripMenuItem,
          enumerable
        );

      modelEnumerable = this.DeviceGroupService
        .GetAllRender();

      enumerable = this.GetDeviceModelEnumerableAsToolStripMenuItemEnumerable
        (modelEnumerable);

      this.SetDeviceSelectToolStripItemCollection
        (
          ref deviceSelectOutputToolStripMenuItem,
          enumerable
        );

      this.OnCheckOfSelectAllDisabledCheckAllDisabled();
      this.OnCheckOfSelectAllEnabledCheckAllEnabled();
      this.OnCheckOfSelectAllInputCheckAllInput();
      this.OnCheckOfSelectAllOutputCheckAllOutput();
      this.OnUncheckOfSelectInputUncheckAll();
      this.OnUncheckOfSelectOutputUncheckAll();
    }

    private void SetDeviceSelectToolStripItemCollection
    (
      ref ToolStripMenuItem deviceSelectDirectionToolStripMenuItem,
      IEnumerable<ToolStripMenuItem> enumerable
    )
    {
      if (deviceSelectDirectionToolStripMenuItem == null)
      {
        return;
      }

      deviceSelectDirectionToolStripMenuItem.DropDownItems
        .Clear();

      deviceSelectDirectionToolStripMenuItem.DropDownItemClicked +=
        deviceConfirmSelectToolStripMenuItem_CheckedChanged;

      if (deviceSelectDirectionToolStripMenuItem.Owner != null)
      {
        ToolStripItemCollection toolStripItemCollection =
          new ToolStripItemCollection
          (
            deviceSelectDirectionToolStripMenuItem.Owner,
            enumerable.ToArray()
          );
      }

      deviceSelectDirectionToolStripMenuItem.Enabled =
        deviceSelectDirectionToolStripMenuItem.HasDropDownItems;
    }

    private void SetDeviceConfirmSelectAbility()
    {
      var result = this.selectedDeviceIdHashSet
          .Count() > 0;

      this.deviceConfirmSelectToolStripMenuItem
        .Enabled = result;
    }

    private void SetSelectedDeviceComponents
    (
      bool append,
      uint id
    )
    {
      if (append)
      {
        this.SelectedDeviceIdHashSet
          .Add(id);
      }

      else
      {
        this.SelectedDeviceIdHashSet
          .Remove(id);
      }

      this.SetDeviceConfirmSelectAbility();
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

    private void OnCheckOfSelectAllDisabledCheckAllDisabled()
    {
      if (this.deviceSelectInputToolStripMenuItem != null)
      {
        deviceSelectAllDisabledToolStripMenuItem
          .CheckedChanged +=
          (
            sender,
            eventArgs
          ) =>
          {
            var enumerable = this.deviceSelectInputToolStripMenuItem
              .DropDownItems
              .Cast<ToolStripMenuItem>()
              .Where
              (
                x =>
                this.GetAllDisabledId
                  .Contains(x.ToolTipText)
              );

            foreach (var item in enumerable)
            {
              this.deviceSelectInputToolStripMenuItem
                .DropDownItems
                .Remove(item);

              item.Enabled = deviceSelectAllDisabledToolStripMenuItem.Enabled;

              this.deviceSelectInputToolStripMenuItem
                .DropDownItems
                .Add(item);
            };
          };
      }

      if (this.deviceSelectOutputToolStripMenuItem != null)
      {
        deviceSelectAllDisabledToolStripMenuItem
          .CheckedChanged +=
          (
            sender,
            eventArgs
          ) =>
          {
            var enumerable = this.deviceSelectInputToolStripMenuItem
              .DropDownItems
              .Cast<ToolStripMenuItem>()
              .Where
              (
                x =>
                this.GetAllDisabledId
                  .Contains(x.ToolTipText)
              );

            foreach (var item in enumerable)
            {
              this.deviceSelectOutputToolStripMenuItem
                .DropDownItems
                .Remove(item);

              item.Enabled = deviceSelectAllDisabledToolStripMenuItem.Enabled;

              this.deviceSelectOutputToolStripMenuItem
                .DropDownItems
                .Add(item);
            }
          };
      }
    }

    private void OnCheckOfSelectAllEnabledCheckAllEnabled()
    {
      if (this.deviceSelectInputToolStripMenuItem != null)
      {
        deviceSelectAllEnabledToolStripMenuItem
          .CheckedChanged +=
          (
            sender,
            eventArgs
          ) =>
          {
            var enumerable = this.deviceSelectInputToolStripMenuItem
              .DropDownItems
              .Cast<ToolStripMenuItem>()
              .Where
              (
                x =>
                this.GetAllEnabledId
                  .Contains(x.ToolTipText)
              );

            foreach (var item in enumerable)
            {
              this.deviceSelectInputToolStripMenuItem
                .DropDownItems
                .Remove(item);

              item.Enabled = deviceSelectAllEnabledToolStripMenuItem.Enabled;

              this.deviceSelectInputToolStripMenuItem
                .DropDownItems
                .Add(item);
            };
          };
      }

      if (this.deviceSelectOutputToolStripMenuItem != null)
      {
        deviceSelectAllEnabledToolStripMenuItem
          .CheckedChanged +=
          (
            sender,
            eventArgs
          ) =>
          {
            var enumerable = this.deviceSelectInputToolStripMenuItem
              .DropDownItems
              .Cast<ToolStripMenuItem>()
              .Where
              (
                x =>
                this.GetAllEnabledId
                  .Contains(x.ToolTipText)
              );

            foreach (var item in enumerable)
            {
              this.deviceSelectOutputToolStripMenuItem
                .DropDownItems
                .Remove(item);

              item.Enabled = deviceSelectAllEnabledToolStripMenuItem.Enabled;

              this.deviceSelectOutputToolStripMenuItem
                .DropDownItems
                .Add(item);
            }
          };
      }
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

    private void deviceConfirmSelectToolStripMenuItem_CheckedChanged
    (
      object? sender,
      EventArgs eventArgs
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

      var toolStripMenuItem = sender as ToolStripMenuItem;
      uint id;

      var result = uint.TryParse
        (
          toolStripMenuItem.ToolTipText,
          out id
        );

      if (!result)
      {
        return;
      }

      this.SetSelectedDeviceComponents
        (
          toolStripMenuItem.Checked,
          id
        );
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

      this.SetDeviceComponents();
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

      this.SetDeviceComponents();
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

      if (sender.GetType() != typeof(ToolStripMenuItem))
      {
        return;
      }

      var toolStripMenuItem = sender as ToolStripMenuItem;

      var enumerable = this.DeviceGroupService
        .GetAllDuplex()
        .Select(x => x.Id);

      foreach (var item in enumerable)
      {
        this.SetSelectedDeviceComponents
          (
            toolStripMenuItem.Checked,
            item
          );
      }

      this.SetDeviceComponents();
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

      this.SetDeviceComponents();
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

      if (sender.GetType() != typeof(ToolStripMenuItem))
      {
        return;
      }

      var toolStripMenuItem = sender as ToolStripMenuItem;

      var enumerable = this.DeviceGroupService
        .GetAllCapture()
        .Select(x => x.Id);

      foreach (var item in enumerable)
      {
        this.SetSelectedDeviceComponents
          (
            toolStripMenuItem.Checked,
            item
          );
      }

      this.SetDeviceComponents();
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

      if (sender.GetType() != typeof(ToolStripMenuItem))
      {
        return;
      }

      var toolStripMenuItem = sender as ToolStripMenuItem;

      var enumerable = this.DeviceGroupService
        .GetAllRender()
        .Select(x => x.Id);

      foreach (var item in enumerable)
      {
        this.SetSelectedDeviceComponents
          (
            toolStripMenuItem.Checked,
            item
          );
      }

      this.SetDeviceComponents();
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