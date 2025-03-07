﻿using VACARM.Domain.Models;
using VACARM.GUI.ViewModels;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Views
{
  public partial class MainForm
  {
    #region Parameters

    internal RepeaterViewModel
      <
        RepeaterGroupService
        <
          ReadonlyRepository
          <
            BaseService
            <
              BaseRepository<RepeaterModel>,
              RepeaterModel
            >
          >,
          BaseService
          <
            BaseRepository<RepeaterModel>,
            RepeaterModel
          >,
          BaseRepository<RepeaterModel>,
          RepeaterModel
        >,
        RepeaterModel
      > RepeaterViewModel
    { get; set; }

    #endregion

    #region Presentation Logic

    private void SetRepeaterComponents()
    {
      this.repeaterSelectAllToolStripMenuItem = this.RepeaterViewModel
        .SelectAllToolStripMenuItem;

      this.repeaterSelectRangeToolStripMenuItem = this.RepeaterViewModel
        .SelectRangeToolStripMenuItem;

      this.repeaterSelectToolStripMenuItem = this.RepeaterViewModel
        .SelectToolStripMenuItem;
    }

    #endregion

    #region Interaction Logic

    private void repeaterExportToClipboardToolStripMenuItem_Click
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

    private void repeaterExportToScriptToolStripMenuItem_Click
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

    private void repeaterExportToXMLToolStripMenuItem_Click
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

    private void repeaterImportFromClipboardToolStripMenuItem_Click
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

    private void repeaterImportFromScriptToolStripMenuItem_Click
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

    private void repeaterImportFromXMLToolStripMenuItem_Click
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

    private void repeaterRedoToolStripMenuItem_Click
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

    private void repeaterSelectToolStripMenuItem_Click
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

    private void repeaterSelectAllToolStripMenuItem_Click
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

    private void repeaterRestartToolStripMenuItem_Click
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

    private void repeaterSelectAllWithAbsentDevicesToolStripMenuItem_Click
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

    private void repeaterSelectAllWithDisabledDevicesToolStripMenuItem_Click
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

    private void repeaterSelectAllWithEnabledDevicesToolStripMenuItem_Click
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

    private void repeaterSelectAllWithPresentDevicesToolStripMenuItem_Click
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

    private void repeaterSelectDevicesToolStripMenuItem_Click
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

    private void repeaterSelectInputDeviceToolStripMenuItem_Click
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

    private void repeaterSelectOutputDeviceToolStripMenuItem_Click
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

    private void repeaterStartToolStripMenuItem_Click
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

    private void repeaterStopToolStripMenuItem_Click
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

    private void repeaterUndoToolStripMenuItem_Click
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