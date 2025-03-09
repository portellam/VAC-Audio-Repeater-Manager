using VACARM.Domain.Models;
using VACARM.GUI.Structs;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.ViewModels
{
  /// <summary>
  /// The view model of <typeparamref name="TBaseGroupService"/>.
  /// </summary>
  public partial class BaseViewModel
    <
      TBaseGroupService,
      TBaseModel
    >
    where TBaseGroupService :
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
    >
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    private BaseGroupService
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
    > groupService
    { get; set; }

    protected virtual IEnumerable<ToolStripItem> SelectRangeToolStripItemEnumerable
    { get; }

    protected virtual IEnumerable<ToolStripItem> SelectToolStripItemEnumerable
    { get; }

    public ToolStripMenuItem SelectAllToolStripMenuItem
    {
      get
      {
        var toolStripMenuItem = DefaultBaseViewModel.SelectAllToolStripMenuItem;
        toolStripMenuItem.Size = this.DefaultSize;

        var enumerable = this.GroupService
          .SelectedRepository
          .GetAll();

        if
        (
          enumerable == null
          || enumerable.Count() == 0
        )
        {
          toolStripMenuItem.Enabled = false;
        }

        toolStripMenuItem.CheckedChanged +=
          this.SelectAllCheckedChangedEventHandler
          (
            this.SelectAllToolStripMenuItem
              .Checked
          );

        return toolStripMenuItem;
      }
    }

    public virtual BaseGroupService
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
    {
      get
      {
        return this.groupService;
      }
      set
      {
        this.groupService = value;
        this.OnPropertyChanged(nameof(this.GroupService));
      }
    }

    public virtual ToolStripMenuItem SelectRangeToolStripMenuItem
    {
      get
      {
        var toolStripMenuItem = DefaultBaseViewModel.SelectToolStripMenuItem;
        toolStripMenuItem.Size = this.DefaultSize;
        var anyEnabled = false;

        var array = this.SelectRangeToolStripItemEnumerable
          .ToArray();

        foreach (var item in array)
        {
          if (item.Enabled)
          {
            anyEnabled = true;
            break;
          }
        }

        toolStripMenuItem.Enabled = anyEnabled;

        toolStripMenuItem.DropDownItems
          .AddRange(array);

        return toolStripMenuItem;
      }
    }

    public virtual ToolStripMenuItem SelectToolStripMenuItem
    {
      get
      {
        var toolStripMenuItem = DefaultBaseViewModel.SelectToolStripMenuItem;
        toolStripMenuItem.Size = this.DefaultSize;
        var anyEnabled = false;

        var array = this.SelectToolStripItemEnumerable
          .ToArray();

        foreach (var item in array)
        {
          if (item.Enabled)
          {
            anyEnabled = true;
            break;
          }
        }

        toolStripMenuItem.Enabled = anyEnabled;

        toolStripMenuItem.DropDownItems
          .AddRange(array);

        return toolStripMenuItem;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseViewModel()
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
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="defaultSize">The default size</param>
    /// <param name="selectDefaultName">The select item default name</param>
    public BaseViewModel
    (
      Size defaultSize,
      string selectDefaultName
    )
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

      this.DefaultSize = defaultSize;
      this.SelectDefaultName = selectDefaultName;
      this.Update();
    }

    /// <summary>
    /// Update the view model.
    /// </summary>
    public virtual void Update()
    {
      var modelEnumerable = this.GroupService
        .SelectedRepository
        .GetAll();

      IEnumerable<ToolStripMenuItem> enumerable = Array.Empty<ToolStripMenuItem>();

      foreach (var item in modelEnumerable)
      {
        var toolStripMenuItem = GetNewDropDownItem
          (
            item.Id,
            this.NameFunc(item)
          );

        enumerable.Append(toolStripMenuItem);
      }

      this.ToolStripMenuItemRepository =
        new ReadonlyRepository<ToolStripMenuItem>(enumerable);
    }

    #endregion
  }
}