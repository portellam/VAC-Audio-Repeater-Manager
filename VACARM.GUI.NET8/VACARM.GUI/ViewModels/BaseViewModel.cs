using System.Windows.Forms;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.ViewModels
{
  /// <summary>
  /// The view model of <typeparamref name="TBaseGroupService"/>.
  /// </summary>
  internal partial class BaseViewModel
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

    internal BaseGroupService
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

    #endregion

    #region Logic

    /// <summary>
    /// Get a new <typeparamref name="ToolStripMenuItem"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    internal static ToolStripMenuItem GetNew
    (
      uint id,
      string name
    )
    {
      ToolStripMenuItem toolStripMenuItem = ToolStripMenuItem;
      int maxIdLength = 7;

      string idWhiteSpace = new string
        (
          ' ',
          maxIdLength - id
            .ToString()
            .Length
        );

      string nameWhiteSpace = new string
      (
        ' ',
        maxIdLength
      );

      string text = string.Format
      (
        "ID:{0}{1},{2}Name: {3}",
        idWhiteSpace,
        id,
        nameWhiteSpace,
        name
      );

      toolStripMenuItem.Text = text;

      toolStripMenuItem.ToolTipText = id
        .ToString();

      return toolStripMenuItem;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    internal BaseViewModel()
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

      this.SetDefaultToolStripMenuItems();
      this.Update();
    }

    internal void Update()
    {
      var modelEnumerable = this.GroupService
        .SelectedRepository
        .GetAll();

      IEnumerable<ToolStripMenuItem> enumerable = Array.Empty<ToolStripMenuItem>();

      foreach (var item in modelEnumerable)
      {
        var toolStripMenuItem = GetNew
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