﻿namespace VACARM.GUI.Controllers
{
  internal partial class BaseController
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    internal static ToolStripMenuItem DefaultToolStripMenuItem { get; set; } =
      new ToolStripMenuItem()
      {
        CheckOnClick = true,
        DisplayStyle = ToolStripItemDisplayStyle.Text,
      };

    internal static ToolStripMenuItem DefaultSelectToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      CheckOnClick = true,
      DisplayStyle = ToolStripItemDisplayStyle.Text,
      Name = SelectString
    };

    internal static ToolStripMenuItem DefaultSelectRangeToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      CheckOnClick = true,
      DisplayStyle = ToolStripItemDisplayStyle.Text,
      Name = SelectRangeString
    };

    private readonly static bool DefaultIsChecked = false;
    private readonly static string SelectString = "Select";
    private readonly static string SelectRangeString = string.Format
      (
        "{0} {1}",
        SelectString,
        "All"
      );

    internal virtual string DefaultName
    {
      get
      {
        return string.Empty;
      }
    }

    #endregion

    #region Logic

    private void SetDefaultToolStripMenuItems()
    {
      DefaultToolStripMenuItem.CheckedChanged += this.CheckedChangedEventHandler;
      DefaultSelectToolStripMenuItem.CheckedChanged += this.CheckedChangedEventHandler;

      DefaultSelectRangeToolStripMenuItem.CheckedChanged +=
        this.RangeCheckedChangedEventHandler
        (
          this.GroupService
            .SelectedService
            .GetAllId(),
          DefaultIsChecked
        );
    }

    #endregion
  }
}