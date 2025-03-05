using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.ViewModels
{
  /// <summary>
  /// The view model of <typeparamref name="RepeaterGroupService"/>.
  /// </summary>
  internal partial class RepeaterViewModel
    <
      RepeaterGroupService,
      TRepeaterModel
    > :
    BaseViewModel
    <
      RepeaterGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TRepeaterModel>,
            TRepeaterModel
          >
        >,
        BaseService
        <
          BaseRepository<TRepeaterModel>,
          TRepeaterModel
        >,
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >,
      TRepeaterModel
    > where TRepeaterModel : 
    RepeaterModel
  {
    #region Parameters

    internal RepeaterGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TRepeaterModel>,
            TRepeaterModel
          >
        >,
        BaseService
        <
          BaseRepository<TRepeaterModel>,
          TRepeaterModel
        >,
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >
    GroupService
    { get; set; }

    internal ToolStripMenuItem StartedToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            RepeaterFunctions<TRepeaterModel>.IsStarted,
            "Started"
          );
      }
    }

    internal ToolStripMenuItem StoppedToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            RepeaterFunctions<TRepeaterModel>.IsStopped,
            "Stopped"
          );
      }
    }

    internal override Func<RepeaterModel, string> NameFunc
    {
      get
      {
        return (RepeaterModel x) => x.WindowName;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    internal RepeaterViewModel() :
      base()
    {
      this.GroupService = new RepeaterGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TRepeaterModel>,
            TRepeaterModel
          >
        >,
        BaseService
        <
          BaseRepository<TRepeaterModel>,
          TRepeaterModel
        >,
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >();
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
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}