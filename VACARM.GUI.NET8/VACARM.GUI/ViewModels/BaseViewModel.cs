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
    IDisposable
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    public BaseGroupService
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

    public ToolStripMenuItem SelectAllToolStripMenuItem
    {
      get
      {
        var toolStripMenuItem = DefaultBaseViewModel.SelectAllToolStripMenuItem;

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

    public virtual ToolStripMenuItem SelectRangeToolStripMenuItem { get; }
    public virtual ToolStripMenuItem SelectToolStripMenuItem { get; }

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

      this.Update();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseViewModel
    (
      ToolStripMenuItem selectAllToolStripMenuItem,
      ToolStripMenuItem selectRangeToolStripMenuItem,
      ToolStripMenuItem selectToolStripMenuItem
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

      this.Update();
    }

    public void Update()
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