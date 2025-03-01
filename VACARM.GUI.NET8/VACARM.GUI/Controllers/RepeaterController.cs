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

    internal ToolStripItemCollection StartedToolStripItemCollection
    {
      get
      {
        var idEnumerable = this.GroupService
          .SelectedRepository
          .GetAll()
          .Where(x => x.IsStarted)
          .Select(x => x.Id);

        IEnumerable<ToolStripMenuItem> enumerable = Array.Empty<ToolStripMenuItem>();

        foreach(var item in idEnumerable)
        {
          var func = ContainsId(item);

          var toolStripMenuItem = base.ToolStripMenuItemRepository
            .Get(func);

          if (toolStripMenuItem == null)
          {
            continue;
          }

          enumerable.Append(toolStripMenuItem);
        }

        return new ToolStripItemCollection
          (
            this.OwnerToolStrip,
            enumerable.ToArray()
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