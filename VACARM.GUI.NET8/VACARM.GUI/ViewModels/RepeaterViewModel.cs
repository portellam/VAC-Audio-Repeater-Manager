using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.ViewModels
{
  /// <summary>
  /// The view model of <typeparamref name="RepeaterGroupService"/>.
  /// </summary>
  public partial class RepeaterViewModel
    <
      TBaseGroupService,
      TRepeaterModel
    > :
    BaseViewModel
    <
      TBaseGroupService,
      TRepeaterModel
    >
    where TBaseGroupService :
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
    >
    where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    // TODO: any SelectAll needs to be just a button with an event handler.

    // TODO: needs to be just a button with an event handler.
    //public ToolStripMenuItem AllToolStripMenuItem
    //{
    //  get
    //  {
    //    var toolStripMenuItem
    //  }
    //}

    public ToolStripMenuItem AllAbsentToolStripMenuItem
    {
      get;
    }

    public ToolStripMenuItem AllDisabledToolStripMenuItem
    {
      get;
    }

    public ToolStripMenuItem AllEnabledToolStripMenuItem
    {
      get;
    }

    public ToolStripMenuItem AllPresentToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            RepeaterFunctions<TRepeaterModel>.IsStarted,
            "Started"
          );
      }
    }

    public ToolStripMenuItem StartedToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            RepeaterFunctions<TRepeaterModel>.IsStarted,
            "Started"
          );
      }
    }

    public ToolStripMenuItem StoppedToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            RepeaterFunctions<TRepeaterModel>.IsStopped,
            "Stopped"
          );
      }
    }

    public override Func<RepeaterModel, string> NameFunc
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
    public RepeaterViewModel() :
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

    #endregion
  }
}