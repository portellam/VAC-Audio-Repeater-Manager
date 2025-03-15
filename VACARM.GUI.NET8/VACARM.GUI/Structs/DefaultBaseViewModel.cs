namespace VACARM.GUI.Structs
{
  internal readonly struct DefaultBaseViewModel
  {
    #region Parameters

    internal static string SelectName = "Select...";
    internal static string SelectRangeName = "Select All...";

    internal static Size Size = new Size
      (
        209,
        22
      );

    internal static ToolStripMenuItem ToolStripMenuItem =
      new ToolStripMenuItem()
      {
        AutoToolTip = false,
        CheckOnClick = true,
        DisplayStyle = ToolStripItemDisplayStyle.Text,
        Enabled = true,
        Name = string.Empty,
        Owner = null,
        Size = Size,
        Text = string.Empty,
        ToolTipText = string.Empty,
      };

    internal static ToolStripMenuItem SelectAllToolStripMenuItem = 
      new ToolStripMenuItem()
      {
        AutoToolTip = ToolStripMenuItem.AutoToolTip,
        CheckOnClick = ToolStripMenuItem.CheckOnClick,
        DisplayStyle = ToolStripMenuItem.DisplayStyle,
        Enabled = ToolStripMenuItem.Enabled,
        Name = "Select All",
        Owner = null,
        Size = ToolStripMenuItem.Size,
        Text = "Select All",
        ToolTipText = string.Empty,
      };

    internal static ToolStripMenuItem SelectRangeToolStripMenuItem =
      new ToolStripMenuItem()
      {
        AutoToolTip = ToolStripMenuItem.AutoToolTip,
        CheckOnClick = ToolStripMenuItem.CheckOnClick,
        DisplayStyle = ToolStripMenuItem.DisplayStyle,
        Enabled = ToolStripMenuItem.Enabled,
        Name = string.Empty,
        Owner = null,
        Size = ToolStripMenuItem.Size,
        Text = string.Empty,
        ToolTipText = string.Empty,
      };

    internal static ToolStripMenuItem SelectToolStripMenuItem =
      new ToolStripMenuItem()
      {
        AutoToolTip = ToolStripMenuItem.AutoToolTip,
        CheckOnClick = ToolStripMenuItem.CheckOnClick,
        DisplayStyle = ToolStripMenuItem.DisplayStyle,
        Enabled = ToolStripMenuItem.Enabled,
        Name = "{0}...",
        Owner = null,
        Size = ToolStripMenuItem.Size,
        Text = "{0}...",
        ToolTipText = string.Empty,
      };

    #endregion
  }
}