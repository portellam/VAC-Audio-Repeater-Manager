namespace VACARM.GUI.ViewModels
{
  internal partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    internal static ToolStripMenuItem ToolStripMenuItem { get; set; } =
      new ToolStripMenuItem()
      {
        AutoToolTip = false,
        CheckOnClick = true,
        DisplayStyle = ToolStripItemDisplayStyle.Text,
        Size = DefaultSize
      };

    internal static ToolStripMenuItem SelectToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      AutoToolTip = ToolStripMenuItem.AutoToolTip,
      CheckOnClick = ToolStripMenuItem.CheckOnClick,
      DisplayStyle = ToolStripMenuItem.DisplayStyle,
      Name = SelectString,
      Size = ToolStripMenuItem.Size
    };

    internal static ToolStripMenuItem SelectRangeToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      AutoToolTip = ToolStripMenuItem.AutoToolTip,
      CheckOnClick = ToolStripMenuItem.CheckOnClick,
      DisplayStyle = ToolStripMenuItem.DisplayStyle,
      Name = SelectRangeString,
      Size = ToolStripMenuItem.Size
    };

    internal static Size DefaultSize { get; set; } = new Size
      (
        0, 
        0
      );

    internal virtual string DefaultName
    {
      get
      {
        return string.Empty;
      }
    }

    private readonly static string SelectString = "Select {0}";

    private readonly static string SelectRangeString = string.Format
      (
        SelectString,
        "All"
      );

    #endregion

    #region Logic

    private void SetDefaultToolStripMenuItems()
    {
      ToolStripMenuItem.CheckedChanged +=
        this.CheckedChangedEventHandler;

      SelectToolStripMenuItem.CheckedChanged +=
        this.CheckedChangedEventHandler;

      SelectRangeToolStripMenuItem.CheckedChanged +=
        this.RangeCheckedChangedEventHandler
        (
          this.GroupService
            .SelectedRepository
            .DeselectedIdEnumerable,
          false
        )
        + this.RangeCheckedChangedEventHandler
        (
          this.GroupService
            .SelectedRepository
            .SelectedIdHashSet,
          true
        );
    }

    #endregion
  }
}