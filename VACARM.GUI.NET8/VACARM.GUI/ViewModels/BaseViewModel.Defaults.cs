﻿using VACARM.GUI.Structs;

namespace VACARM.GUI.ViewModels
{
  public partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    public virtual Size DefaultSize { get; set; } = DefaultBaseViewModel.Size;
    public virtual string DefaultName { get; set; } = string.Empty;

    #endregion

    #region Logic

    // TODO: remove?
    private void SetDefaultToolStripMenuItems()
    {
      SelectRangeToolStripMenuItem.CheckedChanged +=
        this.SelectRangeCheckedChangedEventHandler
        (
          this.GroupService
            .SelectedRepository
            .DeselectedIdEnumerable,
          false
        )
        + this.SelectRangeCheckedChangedEventHandler
        (
          this.GroupService
            .SelectedRepository
            .SelectedIdHashSet,
          true
        );

      SelectToolStripMenuItem.CheckedChanged +=
        this.CheckedChangedEventHandler;
    }

    #endregion
  }
}