using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  /// <summary>
  /// The controller of <typeparamref name="TBaseGroupService"/>.
  /// </summary>
  public partial class BaseController
    <
      TBaseGroupService,
      TBaseModel
    > :
    BaseGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TBaseModel>,
          TBaseModel
        >
      >,
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >,
      BaseRepository<TBaseModel>,
      TBaseModel
    >,
    IDisposable,
    INotifyPropertyChanged
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    internal BaseGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TBaseModel>,
          TBaseModel
        >
      >,
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >,
      BaseRepository<TBaseModel>,
      TBaseModel
    > GroupService
    { get; set; }

    /// <summary>
    /// Select/Deselect the corresponding <typeparamref name="TBaseModel"/>.
    /// </summary>
    internal EventHandler? CheckedChangedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            if (sender == null)
            {
              return;
            }

            if (sender.GetType() != typeof(ToolStripMenuItem))
            {
              return;
            }

            var toolStripMenuItem = sender as ToolStripMenuItem;
            string idString = IdFunc(toolStripMenuItem);

            uint id;

            var result = uint.TryParse
              (
                idString,
                out id
              );

            if (!result)
            {
              return;
            }

            var isChecked = SelectedFunc(toolStripMenuItem);

            this.SelectOnCheck
              (
                id,
                isChecked
              );
          };
      }
    }

    internal ReadonlyRepository<ToolStripMenuItem> ToolStripMenuItemRepository
    { get; set; } = new ReadonlyRepository<ToolStripMenuItem>();

    internal static ToolStripMenuItem DefaultToolStripMenuItem { get; set; } =
    new ToolStripMenuItem()
    {
      CheckOnClick = true,
      DisplayStyle = ToolStripItemDisplayStyle.Text,
    };

    internal static ToolStripMenuItem DefaultSelectToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      CheckOnClick = true,
      DisplayStyle = ToolStripItemDisplayStyle.Text,
      Name = SelectString
    };

    internal static ToolStripMenuItem DefaultSelectRangeToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      CheckOnClick = true,
      DisplayStyle = ToolStripItemDisplayStyle.Text,
      Name = SelectRangeString
    };

    private readonly static bool DefaultIsChecked = false;
    private readonly static string DefaultName = string.Empty;

    private readonly static Func<ToolStripMenuItem, string> IdFunc =
      (ToolStripMenuItem x) => x.ToolTipText;

    private readonly static Func<ToolStripMenuItem, string> NameFunc =
      (ToolStripMenuItem x) => x.Name;

    private readonly static Func<ToolStripMenuItem, bool> SelectedFunc =
      (ToolStripMenuItem x) => x.Checked;

    private readonly static string SelectString = "Select";

    private readonly static string SelectRangeString = string.Format
      (
        "{0} {1}",
        SelectString,
        "All"
      );


    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal void OnPropertyChanged(string propertyName)
    {
      this.PropertyChanged?
        .Invoke
        (
          this,
          new PropertyChangedEventArgs(propertyName)
        );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    /// <summary>
    /// Select/Deselect the corresponding enumerable of all
    /// <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="isChecked">True/false</param>
    internal EventHandler? AllCheckedChangedEventHandler(bool isChecked)
    {
      return
        (
          sender,
          eventArgs
        ) =>
        {
          this.SelectAllOnCheck(isChecked);
        };
    }

    /// <summary>
    /// Select/Deselect the corresponding enumerable of some
    /// <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="isChecked">True/false</param>
    internal EventHandler? RangeCheckedChangedEventHandler
    (
      IEnumerable<uint> idEnumerable,
      bool isChecked
    )
    {
      return
        (
          sender,
          eventArgs
        ) =>
        {
          this.SelectRangeOnCheck
          (
            idEnumerable,
            isChecked
          );
        };
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
      ToolStripMenuItem toolStripMenuItem = DefaultToolStripMenuItem;
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

    private static Func<ToolStripMenuItem, bool> ContainsId(uint id)
    {
      return (ToolStripMenuItem x) => IdFunc(x) == id.ToString();
    }

    /// <summary>
    /// Select/Deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="isChecked">True/false</param>
    private void SelectAllOnCheck(bool isChecked)
    {
      if (isChecked)
      {
        this.GroupService
          .SelectedRepository
          .SelectAll();
      }

      else
      {
        this.GroupService
          .SelectedRepository
          .DeselectAll();
      }
    }

    /// <summary>
    /// Select/Deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="isChecked">True/false</param>
    private void SelectOnCheck
    (
      uint id,
      bool isChecked
    )
    {
      if (isChecked)
      {
        this.GroupService
          .SelectedService
          .Select(id);
      }

      else
      {
        this.GroupService
          .SelectedService
          .Deselect(id);
      }
    }

    /// <summary>
    /// Select/Deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="isChecked">True/false</param>
    private void SelectRangeOnCheck
    (
      IEnumerable<uint> idEnumerable,
      bool isChecked
    )
    {
      if (isChecked)
      {
        this.GroupService
          .SelectedService
          .SelectRange(idEnumerable);
      }

      else
      {
        this.GroupService
          .SelectedService
          .DeselectRange(idEnumerable);
      }
    }

    private void SetDefaultToolStripMenuItems()
    {
      DefaultToolStripMenuItem.CheckedChanged += this.CheckedChangedEventHandler;
      DefaultSelectToolStripMenuItem.CheckedChanged += this.CheckedChangedEventHandler;

      DefaultSelectRangeToolStripMenuItem.CheckedChanged +=
        this.RangeCheckedChangedEventHandler
        (
          this.GroupService
            .SelectedService
            .GetAllId(),
          DefaultIsChecked
        );
    }

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        base.Dispose();
        this.GroupService = null;

        this.ToolStripMenuItemRepository
          .Dispose();

        this.ToolStripMenuItemRepository = null;
      }

      this.HasDisposed = true;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseController()
    {
      this.GroupService = new BaseGroupService
        <
          ReadonlyRepository
          <
            BaseService
            <
              BaseRepository<TBaseModel>,
              TBaseModel
            >
          >,
          BaseService
          <
            BaseRepository<TBaseModel>,
            TBaseModel
          >,
          BaseRepository<TBaseModel>,
          TBaseModel
        >();

      this.SetDefaultToolStripMenuItems();
      this.Update();
    }

    /// <summary>
    /// Do not change this code. 
    /// Put cleanup code in Dispose(<paramref name="bool"/>
    ///  <typeparamref name="isDisposed"/>) method.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    public void Update()
    {
      var modelEnumerable = this.GroupService
        .SelectedRepository
        .GetAll();

      IEnumerable<ToolStripMenuItem> enumerable = Array.Empty<ToolStripMenuItem>();

      foreach (var item in modelEnumerable)
      {
        var toolStripMenuItem = GetNew
          (
            item.Id,
            DefaultName
          );

        enumerable.Append(toolStripMenuItem);
      }

      this.ToolStripMenuItemRepository =
        new ReadonlyRepository<ToolStripMenuItem>(enumerable);
    }

    #endregion
  }
}