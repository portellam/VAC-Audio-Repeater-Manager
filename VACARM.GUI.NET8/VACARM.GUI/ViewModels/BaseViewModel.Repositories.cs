using System.Xml.Linq;
using VACARM.Infrastructure.Repositories;

namespace VACARM.GUI.ViewModels
{
  internal partial class BaseViewModel
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

    /// <summary>
    /// Get a new <typeparamref name="ToolStripMenuItem"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    internal static ToolStripMenuItem GetNew
    (
      uint id,
      string name
    )
    {
      ToolStripMenuItem toolStripMenuItem = SelectToolStripMenuItem;
      int maxIdLength = 7;

      string idWhiteSpace = new string
        (
          ' ',
          maxIdLength - id
            .ToString()
            .Length
        );

      string nameWhiteSpace = new string
      (
        ' ',
        maxIdLength
      );

      string text = string.Format
      (
        "ID:{0}{1},{2}Name: {3}",
        idWhiteSpace,
        id,
        nameWhiteSpace,
        name
      );

      toolStripMenuItem.Text = text;

      toolStripMenuItem.ToolTipText = id
        .ToString();

      return toolStripMenuItem;
    }

    /// <summary>
    /// Get a new range of <typeparamref name="ToolStripMenuItem"/>.
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    internal ToolStripMenuItem GetAllNew()
    {
      var toolStripMenuItem = SelectRangeToolStripMenuItem;

      toolStripMenuItem.Name = string.Format
        (
          toolStripMenuItem.Name,
          "All"
        );

      var idEnumerable = this.GroupService
        .SelectedRepository
        .GetAll()
        .Select(x => x.Id);

      if
      (
        idEnumerable == null
        || idEnumerable.Count() == 0
      )
      {
        toolStripMenuItem.Enabled = false;
      }

      else
      {
        toolStripMenuItem.CheckedChanged +=
          this.RangeCheckedChangedEventHandler
          (
            idEnumerable,
            true
          );
      }

      return toolStripMenuItem;
    }

    /// <summary>
    /// Get a new range of <typeparamref name="ToolStripMenuItem"/>.
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    internal ToolStripMenuItem GetRangeNew
    (
      Func<TBaseModel, bool> func,
      string name
    )
    {
      var toolStripMenuItem = SelectRangeToolStripMenuItem;

      toolStripMenuItem.Name = string.Format
        (
          toolStripMenuItem.Name,
          name
        );

      if (func == null)
      {
        toolStripMenuItem.Enabled = false;
        return toolStripMenuItem;
      }

      var idEnumerable = this.GroupService
        .SelectedRepository
        .GetRange(func)
        .Select(x => x.Id);

      if
      (
        idEnumerable == null
        || idEnumerable.Count() == 0
      )
      {
        toolStripMenuItem.Enabled = false;
      }

      else
      {
        toolStripMenuItem.CheckedChanged +=
          this.RangeCheckedChangedEventHandler
          (
            idEnumerable,
            true
          );
      }

      return toolStripMenuItem;
    }

    /// <summary>
    /// Get a new <typeparamref name="ToolStripMenuItem"/> with drop down items.
    /// </summary>
    /// <param name="modelEnumerable">The enumerable of model(s)</param>
    /// <param name="modelFunc">The function</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    internal ToolStripMenuItem GetNewWithDropDownItems
    (
      IEnumerable<TBaseModel> modelEnumerable,
      Func<TBaseModel, bool> modelFunc,
      string name
    )
    {
      var idEnumerable = modelEnumerable
        .Where(modelFunc)
        .Select(x => x.Id);

      return this.GetNewWithDropDownItems
        (
          idEnumerable,
          name
        );
    }

    /// <summary>
    /// Get a new <typeparamref name="ToolStripMenuItem"/> with drop down items.
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    internal ToolStripMenuItem GetNewWithDropDownItems
    (
      IEnumerable<uint> idEnumerable,
      string name
    )
    {
      var toolStripMenuItem = SelectToolStripMenuItem;

      toolStripMenuItem.Name = string.Format
        (
          toolStripMenuItem.Name,
          name
        );

      var array = this.GetRange(idEnumerable)
        .ToArray();

      toolStripMenuItem.DropDownItems.AddRange(array);
      return toolStripMenuItem;
    }

    #endregion
  }
}