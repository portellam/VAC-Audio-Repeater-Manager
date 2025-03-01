namespace VACARM.GUI.Controllers
{
  public partial class BaseController
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    private readonly static Func<ToolStripMenuItem, string> IdFunc =
      (ToolStripMenuItem x) => x.ToolTipText;

    private readonly static Func<ToolStripMenuItem, string> NameFunc =
      (ToolStripMenuItem x) => x.Name;

    private readonly static Func<ToolStripMenuItem, bool> SelectedFunc =
      (ToolStripMenuItem x) => x.Checked;


    #endregion

    #region Logic

    private static Func<ToolStripMenuItem, bool> ContainsId(uint id)
    {
      return (ToolStripMenuItem x) => IdFunc(x) == id.ToString();
    }

    #endregion
  }
}