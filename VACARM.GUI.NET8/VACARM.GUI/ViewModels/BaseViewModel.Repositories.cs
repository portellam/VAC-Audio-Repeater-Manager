using System.Linq;
using System.Reflection;
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

    private readonly string TextToReplace = "{0}...";

    /// <summary>
    /// The analog of <typeparamref name="TBaseGroupService"/>.
    /// </summary>
    public ReadonlyRepository<ToolStripMenuItem> ToolStripMenuItemRepository
    { get; set; } = new ReadonlyRepository<ToolStripMenuItem>();

    #endregion

    #region Logic

    /// <summary>
    /// Get a modified <typeparamref name="ToolStripMenuItem"/>.
    /// </summary>
    /// <param name="toolStripMenuItem">The tool strip menu item</param>
    /// <param name="array">
    /// The array of <typeparamref name="ToolStripItem"/>(s)
    /// </param>
    /// <param name="text">The text</param>
    /// <returns>The modified <typeparamref name="ToolStripMenuItem"/>.</returns>
    private ToolStripMenuItem GetModified
    (
      ToolStripMenuItem toolStripMenuItem,
      ToolStripItem[] array,
      string text
    )
    {
      if (toolStripMenuItem == null)
      {
        throw new ArgumentNullException(nameof(toolStripMenuItem));
      }

      if (string.IsNullOrWhiteSpace(text))
      {
        throw new ArgumentNullException(nameof(text));
      }

      toolStripMenuItem.Text = text;
      toolStripMenuItem.Size = this.DefaultSize;
      toolStripMenuItem.ToolTipText = string.Empty;
      toolStripMenuItem.Enabled = array.Any(x => x.Enabled);

      toolStripMenuItem.DropDownItems
        .AddRange(array);

      return toolStripMenuItem;
    }

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
        ToolStripMenuItem toolStripMenuItem = this.ToolStripMenuItemRepository
          .Get(ContainsId(item));

        if (toolStripMenuItem == null)
        {
          continue;
        }

        toolStripMenuItem.Owner = null;
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
      ToolStripMenuItem toolStripMenuItem = this.ToolStripMenuItemRepository
        .Get(ContainsId(id));

      if (toolStripMenuItem == null)
      {
        return null;
      }

      toolStripMenuItem.Owner = null;
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
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      ToolStripMenuItem toolStripMenuItem =
        DefaultBaseViewModel.SelectToolStripMenuItem;

      string text = string.Format
        (
          TextToReplace,
          name
        );

      toolStripMenuItem.Text = text;
      toolStripMenuItem.ToolTipText = string.Empty;

      if (func == null)
      {
        toolStripMenuItem.Enabled = false;
        return toolStripMenuItem;
      }

      var idEnumerable = this.GroupService
        .SelectedRepository
        .GetRange(func)
        .Select(x => x.Id);

      if (idEnumerable.IsNullOrEmpty())
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
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      ToolStripMenuItem toolStripMenuItem =
        DefaultBaseViewModel.SelectToolStripMenuItem;

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

      string text = string.Format
      (
        "ID:{0}{1},{2}Name: {3}",
        idWhiteSpace,
        id,
        nameWhiteSpace,
        name
      );

      toolStripMenuItem.Text = text;

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
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

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
    /// <param name="text">The text</param>
    /// <returns>The tool strip menu item.</returns>
    public ToolStripMenuItem GetNewWithDropDownItems
    (
      IEnumerable<uint> idEnumerable,
      string text
    )
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        throw new ArgumentNullException(nameof(text));
      }

      ToolStripMenuItem toolStripMenuItem = 
        DefaultBaseViewModel.SelectToolStripMenuItem;

      toolStripMenuItem.Text = string.Format
        (
          TextToReplace,
          text
        );

      var array = this.GetRange(idEnumerable)
        .ToArray();

      if (array.Length == 0)
      {
        toolStripMenuItem.Enabled = false;
      }

      else
      {
        toolStripMenuItem.DropDownItems
          .AddRange(array);
      }

      return toolStripMenuItem;
    }

    #endregion
  }
}