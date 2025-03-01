namespace VACARM.GUI.Controllers
{
  internal partial class BaseController
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    internal readonly static Func<ToolStripMenuItem, string> IdFunc =
      (ToolStripMenuItem x) => x.ToolTipText;

    internal readonly static Func<ToolStripMenuItem, bool> SelectedFunc =
      (ToolStripMenuItem x) => x.Checked;

    internal virtual Func<TBaseModel, string> NameFunc
    {
      get
      {
        return (TBaseModel x) => DefaultName;
      }
    }

    #endregion

    #region Logic

    internal static Func<ToolStripMenuItem, bool> ContainsId(uint id)
    {
      return (ToolStripMenuItem x) => IdFunc(x) == id.ToString();
    }

    #endregion
  }
}