using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  internal partial class RepeaterController
    <
      TBaseGroupService,
      TRepeaterModel
    > :
    BaseController
    <
      RepeaterGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<RepeaterModel>,
            RepeaterModel
          >
        >,
        BaseService
        <
          BaseRepository<RepeaterModel>,
          RepeaterModel
        >,
        BaseRepository<RepeaterModel>,
        RepeaterModel
      >,
      RepeaterModel
    >
  {
    #region Parameters

    internal ToolStrip OwnerToolStrip { get; set; }
    #endregion

    #region Logic

    public RepeaterController() :
      base()
    {
      this.GroupService = new RepeaterGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<RepeaterModel>,
            RepeaterModel
          >
        >,
        BaseService
        <
          BaseRepository<RepeaterModel>,
          RepeaterModel
        >,
        BaseRepository<RepeaterModel>,
        RepeaterModel
      >();
    }


    internal void SetToolStripItemCollectionProperties()
    {
      if (this.AllToolStripItemCollection == null)
      {
        this.AllToolStripItemCollection = new ToolStripItemCollection
          (
            this.
          );
      }
    }


    internal void SetToolStripItemCollectionProperties
    (
      ref ToolStripItemCollection toolStripItemCollection
    )
    {
      if (toolStripItemCollection == null)
      {
        yield break;
      }

      var enumerable = this.Get
    }

    #endregion
  }
}