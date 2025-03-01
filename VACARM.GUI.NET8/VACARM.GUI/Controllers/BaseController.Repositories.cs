using VACARM.Infrastructure.Repositories;

namespace VACARM.GUI.Controllers
{
  internal partial class BaseController
    <
      TBaseGroupService,
      TBaseModel
    >
  {
    #region Parameters

    /// <summary>
    /// The analog of <typeparamref name="TBaseGroupService"/>.
    /// </summary>
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

    internal ToolStripMenuItem GetToolStripMenuItemWithDropDownItems
    (
      IEnumerable<TBaseModel> modelEnumerable,
      Func<TBaseModel, bool> modelFunc,
      string name
    )
    {
      var toolStripMenuItem = DefaultSelectRangeToolStripMenuItem;
      toolStripMenuItem.Name += " " + name;

      var idEnumerable = modelEnumerable
        .Where(modelFunc)
        .Select(x => x.Id);

      var array = this.GetRange(idEnumerable)
        .ToArray();

      toolStripMenuItem.DropDownItems.AddRange(array);
      return toolStripMenuItem;
    }

    internal ToolStripMenuItem GetToolStripMenuItemWithDropDownItems
    (
      IEnumerable<uint> idEnumerable,
      string name
    )
    {
      var toolStripMenuItem = DefaultSelectRangeToolStripMenuItem;
      toolStripMenuItem.Name += " " + name;

      var array = this.GetRange(idEnumerable)
        .ToArray();

      toolStripMenuItem.DropDownItems.AddRange(array);
      return toolStripMenuItem;
    }

    #endregion
  }
}