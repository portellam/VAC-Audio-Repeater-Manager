namespace VACARM.GUI.Structs
{
  internal struct DefaultBaseViewModel
  {
    #region Parameters

    internal readonly static Size Size = new Size
      (
        209,
        22
      );

    internal readonly static ToolStripMenuItem SelectAllToolStripMenuItem = 
      new ToolStripMenuItem()
      {
        AutoToolTip = ToolStripMenuItem.AutoToolTip,
        CheckOnClick = ToolStripMenuItem.CheckOnClick,
        DisplayStyle = ToolStripMenuItem.DisplayStyle,
        Enabled = ToolStripMenuItem.Enabled,
        Name = "Select All",
        Size = ToolStripMenuItem.Size
      };

    internal readonly static ToolStripMenuItem SelectRangeToolStripMenuItem =
      new ToolStripMenuItem()
      {
      AutoToolTip = ToolStripMenuItem.AutoToolTip,
      CheckOnClick = ToolStripMenuItem.CheckOnClick,
      DisplayStyle = ToolStripMenuItem.DisplayStyle,
      Enabled = ToolStripMenuItem.Enabled,
      Name = "Select All...",
      Size = ToolStripMenuItem.Size
    };

    internal readonly static ToolStripMenuItem SelectToolStripMenuItem =
      new ToolStripMenuItem()
      {
      AutoToolTip = ToolStripMenuItem.AutoToolTip,
      CheckOnClick = ToolStripMenuItem.CheckOnClick,
      DisplayStyle = ToolStripMenuItem.DisplayStyle,
      Enabled = ToolStripMenuItem.Enabled,
      Name = "Select...",
      Size = ToolStripMenuItem.Size
    };

    internal readonly static ToolStripMenuItem ToolStripMenuItem =
      new ToolStripMenuItem()
      {
        AutoToolTip = false,
        CheckOnClick = true,
        DisplayStyle = ToolStripItemDisplayStyle.Text,
        Enabled = true,
        Size = Size
      };

    #endregion
  }
}
