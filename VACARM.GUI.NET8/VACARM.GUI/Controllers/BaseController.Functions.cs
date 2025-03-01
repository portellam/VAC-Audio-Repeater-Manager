namespace VACARM.GUI.Controllers
{
  public partial class BaseController
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    internal readonly static Func<ToolStripMenuItem, string> IdFunc =
      (ToolStripMenuItem x) => x.ToolTipText;

    internal readonly static Func<ToolStripMenuItem, string> NameFunc =
      (ToolStripMenuItem x) => x.Name;

    internal readonly static Func<ToolStripMenuItem, bool> SelectedFunc =
      (ToolStripMenuItem x) => x.Checked;


    #endregion

    #region Logic

    internal static Func<ToolStripMenuItem, bool> ContainsId(uint id)
    {
      return (ToolStripMenuItem x) => IdFunc(x) == id.ToString();
    }

    #endregion
  }
}