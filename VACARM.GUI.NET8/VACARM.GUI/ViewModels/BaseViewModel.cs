using System.Windows.Forms;
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

    /// <summary>
    /// <see langword="NOTE:"/> Please use <see cref="GetClone"/> for each
    /// referenced <typeparamref name="ToolStripItem"/> when appending to the
    /// array.
    /// </summary>
    protected virtual ToolStripItem[] SelectRangeToolStripItemArray
    { get; }

    /// <summary>
    /// <see langword="NOTE:"/> Please use <see cref="GetClone"/> for each
    /// referenced <typeparamref name="ToolStripItem"/> when appending to the
    /// array.
    /// </summary>
    protected virtual ToolStripItem[] SelectToolStripItemArray
    { get; }

    public ToolStripMenuItem SelectAllToolStripMenuItem
    {
      get
      {
        ToolStripMenuItem toolStripMenuItem =
          DefaultBaseViewModel.SelectAllToolStripMenuItem;

        toolStripMenuItem.Size = this.DefaultSize;
        toolStripMenuItem.ToolTipText = string.Empty;

        var enumerable = this.GroupService
          .SelectedRepository
          .IsNullOrEmpty;

        toolStripMenuItem.Enabled = this.GroupService
          .SelectedRepository
          .IsNullOrEmpty;

        toolStripMenuItem.CheckedChanged +=
          this.SelectAllCheckedChangedEventHandler();

        return toolStripMenuItem.GetClone();
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

    public ToolStripMenuItem SelectRangeToolStripMenuItem
    {
      get
      {
        return this.GetModified
          (
            DefaultBaseViewModel.SelectRangeToolStripMenuItem,
            this.SelectRangeToolStripItemArray,
            DefaultBaseViewModel.SelectRangeText
          )
          .GetClone();
      }
    }

    public ToolStripMenuItem SelectToolStripMenuItem
    {
      get
      {
        return this.GetModified
          (
            DefaultBaseViewModel.SelectToolStripMenuItem,
            this.SelectToolStripItemArray,
            DefaultBaseViewModel.SelectText
          )
          .GetClone();
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
      this.SelectDefaultText = selectDefaultName;
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

      var list = new List<ToolStripMenuItem>();

      foreach (var item in modelEnumerable)
      {
        ToolStripMenuItem toolStripMenuItem = GetNewDropDownItem
          (
            item.Id,
            this.TextFunc(item)
          );

        list.Add(toolStripMenuItem);
      }

      this.ToolStripMenuItemRepository =
        new ReadonlyRepository<ToolStripMenuItem>(list);
    }

    #endregion
  }
}