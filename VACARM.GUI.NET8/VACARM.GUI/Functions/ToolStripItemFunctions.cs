namespace VACARM.GUI.Functions
{
  internal static class ToolStripItemFunctions
  {
    /// <summary>
    /// Match a <typeparamref name="ToolStripItem"/>.
    /// </summary>
    /// <param name="toolStripItem">The tool strip item</param>
    /// <returns>The function.</returns>
    internal static Func<ToolStripItem, bool> ContainsToolTipText
    (ToolStripItem toolStripItem)
      => (ToolStripItem x) =>
        x.Name == toolStripItem.Name
        || x.ToolTipText == toolStripItem.ToolTipText;

    #region Logic

    /// <summary>
    /// Match a <typeparamref name="ToolStripItem"/>.
    /// </summary>
    /// <param name="toolTipText">The tool tip text</param>
    /// <returns>The function.</returns>
    internal static Func<ToolStripItem, bool> ContainsId(string toolTipText)
    {
      return (ToolStripItem x) => x.ToolTipText == toolTipText;
    }

    /// <summary>
    /// Match an enumerable of <typeparamref name="ToolStripItem"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of tool tip text(s)</param>
    /// <returns>The function.</returns>
    internal static Func<ToolStripItem, bool> ContainsIdEnumerable
    (IEnumerable<string> idEnumerable)
    {
      return (ToolStripItem x) => idEnumerable.Contains(x.ToolTipText);
    }

    #endregion
  }
}