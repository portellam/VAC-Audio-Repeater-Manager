using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  /// <summary>
  /// The controller of <typeparamref name="TBaseGroupService"/>.
  /// </summary>
  public partial class BaseController
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
    IDisposable,
    INotifyPropertyChanged
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

    /// <summary>
    /// Select/Deselect the corresponding <typeparamref name="TBaseModel"/>.
    /// </summary>
    internal EventHandler? CheckedChangedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            if (sender == null)
            {
              return;
            }

            if (sender.GetType() != typeof(ToolStripMenuItem))
            {
              return;
            }

            var toolStripMenuItem = sender as ToolStripMenuItem;
            string idString = this.IdFunc(toolStripMenuItem);

            uint id;

            var result = uint.TryParse
              (
                idString,
                out id
              );

            if (!result)
            {
              return;
            }

            var isChecked = this.SelectedFunc(toolStripMenuItem);

            this.SelectOnCheck
              (
                id,
                isChecked
              );
          };
      }
    }

    internal Func<ToolStripMenuItem, string> IdFunc
    {
      get
      {
        return (ToolStripMenuItem x) => x.ToolTipText;
      }
    }

    internal Func<ToolStripMenuItem, string> NameFunc
    {
      get
      {
        return (ToolStripMenuItem x) => x.Name;
      }
    }

    internal Func<ToolStripMenuItem, bool> SelectedFunc
    {
      get
      {
        return (ToolStripMenuItem x) => x.Checked;
      }
    }

    internal static ToolStripMenuItem DefaultToolStripMenuItem { get; set; } =
    new ToolStripMenuItem()
    {
      CheckOnClick = true,
      DisplayStyle = ToolStripItemDisplayStyle.Text,
    };

    internal static ToolStripMenuItem DefaultSelectToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      CheckOnClick = true,
      DisplayStyle = ToolStripItemDisplayStyle.Text,
      Name = SelectString
    };

    internal static ToolStripMenuItem DefaultSelectRangeToolStripMenuItem
    { get; set; } = new ToolStripMenuItem()
    {
      CheckOnClick = true,
      DisplayStyle = ToolStripItemDisplayStyle.Text,
      Name = SelectRangeString
    };

    private readonly static string SelectString = "Select";

    private readonly static string SelectRangeString = string.Format
      (
        "{0} {1}",
        SelectString,
        "All"
      );


    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal void OnPropertyChanged(string propertyName)
    {
      this.PropertyChanged?
        .Invoke
        (
          this,
          new PropertyChangedEventArgs(propertyName)
        );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    //TODO: select/deselect, and check/uncheck
    internal EventHandler? RangeCheckedChangedEventHandler
    (
      IEnumerable<uint> idEnumerable,
      bool isChecked
    )
    {
      return
        (
          sender,
          eventArgs
        ) =>
        {
          if (isChecked)
          {
            this.GroupService
              .SelectedService
              .SelectRange(idEnumerable);
          }

          else
          {
            this.GroupService
              .SelectedService
              .DeselectRange(idEnumerable);
          }
        };
    }

    /// <summary>
    /// Select/deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="isChecked">True/false</param>
    internal void SelectAllOnCheck(bool isChecked)
    {
      if (isChecked)
      {
        this.GroupService
          .SelectedRepository
          .SelectAll();
      }

      else
      {
        this.GroupService
          .SelectedRepository
          .DeselectAll();
      }
    }

    /// <summary>
    /// Select/deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="isChecked">True/false</param>
    internal void SelectOnCheck
    (
      uint id,
      bool isChecked
    )
    {
      if (isChecked)
      {
        this.GroupService
          .SelectedService
          .Select(id);
      }

      else
      {
        this.GroupService
          .SelectedService
          .Deselect(id);
      }
    }

    /// <summary>
    /// Select/deselect given <see langword="isChecked"/> is true/false.
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="isChecked">True/false</param>
    internal void SelectRangeOnCheck
    (
      IEnumerable<uint> idEnumerable,
      bool isChecked
    )
    {
      if (isChecked)
      {
        this.GroupService
          .SelectedService
          .SelectRange(idEnumerable);
      }

      else
      {
        this.GroupService
          .SelectedService
          .DeselectRange(idEnumerable);
      }
    }

    private void SetDefaultToolStripMenuItem()
    {
      DefaultToolStripMenuItem.CheckedChanged += CheckedChangedEventHandler;
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

        //TODO: add more here.
      }

      this.HasDisposed = true;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseController()
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
    /// Do not change this code. 
    /// Put cleanup code in Dispose(<paramref name="bool"/>
    ///  <typeparamref name="isDisposed"/>) method.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}