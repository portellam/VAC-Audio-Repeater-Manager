using VACARM.GUI.Structs;
using VACARM.Infrastructure.Repositories;

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
    /// The analog of <typeparamref name="TBaseGroupService"/>.
    /// </summary>
    public ReadonlyRepository<ToolStripMenuItem> ToolStripMenuItemRepository
    { get; set; } = new ReadonlyRepository<ToolStripMenuItem>();

    #endregion

    #region Logic

    /// <summary>
    /// Get an enumerable of some <typeparamref name="ToolStripMenuItem"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    public new IEnumerable<ToolStripMenuItem> GetRange
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
    public new ToolStripMenuItem? Get(uint id)
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
    /// <param name="func">The function</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    public ToolStripMenuItem GetNew
    (
      Func<TBaseModel, bool> func,
      string name
    )
    {
      var toolStripMenuItem = DefaultBaseViewModel.SelectToolStripMenuItem;

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
          this.SelectRangeCheckedChangedEventHandler
          (
            idEnumerable,
            true
          );
      }

      return toolStripMenuItem;
    }

    /// <summary>
    /// Get a new drop down <typeparamref name="ToolStripMenuItem"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    public static ToolStripMenuItem GetNewDropDownItem
    (
      uint id,
      string name
    )
    {
      var toolStripMenuItem = DefaultBaseViewModel.SelectToolStripMenuItem;

      int idLength = id.ToString()
        .Length;

      int maxIdLength = int.MaxValue
        .ToString()
        .Length;

      int idLengthDifference = maxIdLength - idLength;

      string idWhiteSpace = new string
        (
          ' ',
          idLengthDifference
        );

      string nameWhiteSpace = new string
      (
        ' ',
        maxIdLength
      );

      toolStripMenuItem.Text = string.Format
      (
        "ID:{0}{1},{2}Name: {3}",
        idWhiteSpace,
        id,
        nameWhiteSpace,
        name
      );

      toolStripMenuItem.ToolTipText = id.ToString();
      return toolStripMenuItem;
    }

    /// <summary>
    /// Get a new <typeparamref name="ToolStripMenuItem"/> with drop down items.
    /// </summary>
    /// <param name="modelEnumerable">The enumerable of model(s)</param>
    /// <param name="modelFunc">The function</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    public ToolStripMenuItem GetNewWithDropDownItems
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
    public virtual ToolStripMenuItem GetNewWithDropDownItems
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

      if (array.Length == 0)
      {
        toolStripMenuItem.Enabled = false;
      }

      else
      {
        toolStripMenuItem.DropDownItems.AddRange(array);
      }

      return toolStripMenuItem;
    }

    #endregion
  }
}