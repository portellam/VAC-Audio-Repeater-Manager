using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  internal partial class RepeaterController
  {
    #region Parameters

    internal RepeaterGroupService
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
    > GroupService
    { get; set; }

    #endregion

    #region Logic

    internal EventHandler? SelectAllPropertyCheckedChangedEventHandler
    (
      ref IEnumerable<uint> propertyIdEnumerable,
      bool isChecked
    )
    {
      if (propertyIdEnumerable == null)
      {
        return null;
      }

      var enumerable = this.GetToolStripMenuItemEnumerable
        (
          this.ToolStrip,
          propertyIdEnumerable
        );

      return
        (
          sender,
          eventArgs
        ) =>
        {
          foreach (var item in enumerable)
          {
            item.Checked = isChecked;
          }
        };
    }

    

    internal ToolStripItemCollection GetToolStripItemCollection
    (
      ref ToolStripMenuItem propertyToolStripMenuItem,
      IEnumerable<uint> propertyIdEnumerable
    )
    {

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