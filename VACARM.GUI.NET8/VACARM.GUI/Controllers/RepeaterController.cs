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

    internal override Func<RepeaterModel, string> NameFunc
    { 
      get
      {
        return (RepeaterModel x) => x.WindowName;
      }
    }

    #endregion

    #region Logic

    private void SetToolStripMenuItems()
    {

    }

    /// <summary>
    /// Constructor
    /// </summary>
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

      this.SetToolStripMenuItems();
    }

    #endregion
  }
}