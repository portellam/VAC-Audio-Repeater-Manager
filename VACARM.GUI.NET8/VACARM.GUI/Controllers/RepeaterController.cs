using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  /// <summary>
  /// The controller of <typeparamref name="RepeaterGroupService"/>.
  /// </summary>
  internal partial class RepeaterController
    <
      RepeaterGroupService,
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
    > where TRepeaterModel : RepeaterModel
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
    internal RepeaterController() :
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

      this.SetToolStripMenuItems();
    }

    #endregion
  }
}