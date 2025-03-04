namespace VACARM.GUI.Controllers
{
  internal partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    internal static ToolStripMenuItem DefaultToolStripMenuItem { get; set; } =
      new ToolStripMenuItem()
      {
        AutoToolTip = false,
        CheckOnClick = true,
        DisplayStyle = ToolStripItemDisplayStyle.Text,
        Size = DefaultSize
      };

    internal static ToolStripMenuItem DefaultSelectToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      AutoToolTip = DefaultToolStripMenuItem.AutoToolTip,
      CheckOnClick = DefaultToolStripMenuItem.CheckOnClick,
      DisplayStyle = DefaultToolStripMenuItem.DisplayStyle,
      Name = SelectString,
      Size = DefaultToolStripMenuItem.Size
    };

    internal static ToolStripMenuItem DefaultSelectRangeToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      AutoToolTip = DefaultToolStripMenuItem.AutoToolTip,
      CheckOnClick = DefaultToolStripMenuItem.CheckOnClick,
      DisplayStyle = DefaultToolStripMenuItem.DisplayStyle,
      Name = SelectRangeString,
      Size = DefaultToolStripMenuItem.Size
    };

    internal static Size DefaultSize { get; set; } = new Size(0, 0);

    internal virtual string DefaultName
    {
      get
      {
        return string.Empty;
      }
    }

    private readonly static string SelectString = "Select";
    private readonly static string SelectRangeString = string.Format
      (
        "{0} {1}",
        SelectString,
        "All"
      );

    #endregion

    #region Logic

    private void SetDefaultToolStripMenuItems()
    {
      DefaultToolStripMenuItem.CheckedChanged +=
        this.CheckedChangedEventHandler;

      DefaultSelectToolStripMenuItem.CheckedChanged +=
        this.CheckedChangedEventHandler;

      DefaultSelectRangeToolStripMenuItem.CheckedChanged +=
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