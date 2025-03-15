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
    /// <param name="name">The name an text</param>
    /// <returns>The modified <typeparamref name="ToolStripMenuItem"/>.</returns>
    private ToolStripMenuItem GetModified
    (
      ToolStripMenuItem toolStripMenuItem,
      ToolStripItem[] array,
      string name
    )
    {
      if (toolStripMenuItem == null)
      {
        throw new ArgumentNullException(nameof(toolStripMenuItem));
      }

      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      toolStripMenuItem.Name = name;
      toolStripMenuItem.Text = name;
      toolStripMenuItem.Size = this.DefaultSize;
      var anyEnabled = false;
      var newEnumerable = Array.Empty<ToolStripItem>();

      foreach (var item in array)
      {
        item.Owner = null;

        if (item.Enabled)
        {
          anyEnabled = true;
        }

        newEnumerable.Append(item);
      }

      toolStripMenuItem.Enabled = anyEnabled;

      toolStripMenuItem.DropDownItems
          .AddRange(newEnumerable.ToArray());

      return toolStripMenuItem;
    }

    /// <summary>
    /// Get a clone <typeparamref name="ToolStripMenuItem"/>.
    /// Useful for when more than one <typeparamref name="ToolStripMenuItem"/>
    /// reference the same <typeparamref name="ToolStripMenuItem"/> object.
    /// </summary>
    /// <param name="original">
    /// The original <typeparamref name="ToolStripMenuItem"/>
    /// </param>
    /// <returns>The clone <typeparamref name="ToolStripMenuItem"/></returns>
    protected static ToolStripMenuItem GetClone(ToolStripMenuItem original)
    {
      ToolStripMenuItem clone = new ToolStripMenuItem();

      PropertyInfo[] propertyInfoArray = typeof(ToolStripMenuItem).GetProperties
        (
          BindingFlags.Public
          | BindingFlags.Instance
        );

      foreach (PropertyInfo property in propertyInfoArray)
      {
        if (!property.CanWrite)
        {
          continue;
        }

        object value = property.GetValue(original);

        property.SetValue
          (
            clone,
            value
          );
      }

      return clone;
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

      name = " " + name;

      ToolStripMenuItem toolStripMenuItem =
        DefaultBaseViewModel.SelectToolStripMenuItem;

      string text = string.Format
        (
          toolStripMenuItem.Name,
          name
        );

      toolStripMenuItem.Name = text;
      toolStripMenuItem.Text = text;

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

      toolStripMenuItem.Name = text;
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
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    public ToolStripMenuItem GetNewWithDropDownItems
    (
      IEnumerable<uint> idEnumerable,
      string name
    )
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      ToolStripMenuItem toolStripMenuItem =
        DefaultBaseViewModel.SelectToolStripMenuItem;

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
        toolStripMenuItem.DropDownItems
          .AddRange(array);
      }

      return toolStripMenuItem;
    }

    #endregion
  }
}