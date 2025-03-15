namespace VACARM.GUI.ViewModels
{
  public partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    /// <summary>
    /// Get a <typeparamref name="ToolStripMenuItem"/> ID.
    /// </summary>
    /// <returns>The function.</returns>
    public readonly static Func<ToolStripMenuItem, string> IdFunc =
      (ToolStripMenuItem x) => x.ToolTipText;

    /// <summary>
    /// Get the selected state of a <typeparamref name="ToolStripMenuItem"/>.
    /// </summary>
    /// <returns>The function.</returns>
    public readonly static Func<ToolStripMenuItem, bool> SelectedFunc =
      (ToolStripMenuItem x) => x.Checked;

    /// <summary>
    /// Get the default text.
    /// </summary>
    /// <returns>The function.</returns>
    public virtual Func<TBaseModel, string> TextFunc
    {
      get
      {
        return (TBaseModel x) => SelectDefaultText;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Match a <typeparamref name="ToolStripMenuItem"/> ID.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The function.</returns>
    public static Func<ToolStripMenuItem, bool> ContainsId(uint id)
    {
      return (ToolStripMenuItem x) => IdFunc(x) == id.ToString();
    }

    #endregion
  }
}