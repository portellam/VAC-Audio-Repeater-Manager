using VACARM.Infrastructure.Repositories;

namespace VACARM.GUI.Controllers
{
  public partial class BaseController
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    internal ReadonlyRepository<ToolStripMenuItem> ToolStripMenuItemRepository
    { get; set; } = new ReadonlyRepository<ToolStripMenuItem>();

    #endregion

    #region Logic

    /// <summary>
    /// Get an enumerable of some <typeparamref name="ToolStripMenuItem"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    internal IEnumerable<ToolStripMenuItem> GetRange
    (IEnumerable<uint> idEnumerable)
    {
      if (idEnumerable == null)
      {
        yield break;
      }

      foreach (var item in idEnumerable)
      {
        var toolStripMenuItem = this.ToolStripMenuItemRepository
          .Get(ContainsId(item));

        if (toolStripMenuItem == null)
        {
          continue;
        }

        yield return toolStripMenuItem;
      }
    }

    /// <summary>
    /// Get a <typeparamref name="ToolStripMenuItem"/>(s).
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The tool strip menu item.</returns>
    internal ToolStripMenuItem? Get(uint id)
    {
      var toolStripMenuItem = this.ToolStripMenuItemRepository
        .Get(ContainsId(id));

      if (toolStripMenuItem == null)
      {
        return null;
      }

      return toolStripMenuItem;
    }

    #endregion
  }
}