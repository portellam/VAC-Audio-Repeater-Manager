using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  /* 
   * GOAL:
   * - consistent UI.
   * - responsive UI.
   * - dynamic tool strip menu items given repository.
   * - selections are reflected between tool strip menu items and elsewhere.
   * 
   * TODO:
   * 1. Click method validates ToolTipText
   * 2. Add/Delete from Selected HashSet
   * 3. On update of Selected HashSet, update Parent ToolStripItemCollection.
   * 4. Every item in Parent ToolStripItemCollection uses an CheckedChanged
   *    EventHandler which points to the aforementioned Click method (#1).
   * 
   * TODO: determine if we can have a Select-All or Select-Many check all the
   * related nested ToolStripMenuItems.
   * TODO: determine if we can have Select nested ToolStripMenuItems check
   * the Select-All or Select-Many if the selected groups match.
   */


  /// <summary>
  /// Controller for <typeparamref name="DeviceGroupService"/>.
  /// </summary>
  internal class DeviceController :
    IDisposable,
    INotifyPropertyChanged
  {
    #region Parameters

    internal DeviceGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<DeviceModel>,
          DeviceModel
        >
      >,
      BaseService
      <
        BaseRepository<DeviceModel>,
        DeviceModel
      >,
      BaseRepository<DeviceModel>,
      DeviceModel
    > DeviceGroupService
    { get; set; }

    internal EventHandler? AllCheckedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            this.SetParentToolStripItemCollection
              (
                this.SelectAllPropertyToolStripMenuItem
                  .Checked
              );
          };
      }
    }

    internal EventHandler? AllCaptureCheckedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            this.SetParentToolStripItemCollection
              (
                this.AllCaptureToolStripMenuItem
                  .Checked,
                this.CaptureIdEnumerable
              );
          };
      }
    }

    internal EventHandler? AllDisabledCheckedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            this.SetParentToolStripItemCollection
              (
                this.AllCaptureToolStripMenuItem
                  .Checked,
                this.DisabledIdEnumerable
              );
          };
      }
    }

    internal EventHandler? AllEnabledCheckedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            this.SetParentToolStripItemCollection
              (
                this.AllCaptureToolStripMenuItem
                  .Checked,
                this.EnabledIdEnumerable
              );
          };
      }
    }

    internal EventHandler? AllRenderCheckedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            this.SetParentToolStripItemCollection
              (
                this.AllCaptureToolStripMenuItem
                  .Checked,
                this.RenderIdEnumerable
              );
          };
      }
    }

    internal EventHandler? CaptureCheckedEventHandler { get; set; }

    // NOTE: does the new logic *and* intent behind AllSomethingChecked... make this redundant?
    internal EventHandler? DisabledCheckedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            var enumerable = this.GetModifiedParentToolStripItemEnumerable
              (
                this.DisabledIdEnumerable,
                this.SelectAllDisabledToolStripMenuItem.Checked
              );

            this.PartialSetParentToolStripItemCollection
              (
                enumerable,
                this.DisabledIdEnumerable
              );
          };
      }
    }

    internal EventHandler? EnabledCheckedEventHandler
    {
      get
      {
        return
          (
            sender,
            eventArgs
          ) =>
          {
            var enumerable = this.GetModifiedParentToolStripItemEnumerable
              (
                this.EnabledIdEnumerable,
                this.SelectAllEnabledToolStripMenuItem.Checked
              );

            this.PartialSetParentToolStripItemCollection
              (
                enumerable,
                this.EnabledIdEnumerable
              );
          };
      }
    }

    internal EventHandler? RenderCheckedEventHandler { get; set; }

    internal IEnumerable<uint> SelectedIdEnumerable
    {
      get
      {
        var idStringEnumerable = this.ParentToolStripItemCollection
          .Cast<ToolStripMenuItem>()
          .Where(x => x.Checked)
          .Select(x => x.ToolTipText);

        return uintExtension.TryParse(idStringEnumerable);
      }
    }

    internal ToolStripItemCollection CaptureToolStripItemCollection
    {
      get
      {
        var array = this.GetModifiedParentToolStripItemEnumerable
          (
            this.CaptureIdEnumerable,
            this.CaptureCheckedEventHandler
          )
          .ToArray();

        return new ToolStripItemCollection
          (
            this.SelectCaptureToolStripMenuItem
              .Owner,
            array
          );
      }
      set
      {
        this.PartialSetParentToolStripItemCollection(value);
        this.OnPropertyChanged(nameof(this.CaptureToolStripItemCollection));
      }
    }

    internal IEnumerable<ToolStripItem> DisabledToolStripItemEnumerable
    {
      get
      {
        return this.GetPartialParentToolStripItemEnumerable
          (this.DisabledIdEnumerable);
      }
    }

    internal IEnumerable<ToolStripItem> EnabledToolStripItemEnumerable
    {
      get
      {
        return this.GetPartialParentToolStripItemEnumerable
          (this.EnabledIdEnumerable);
      }
    }

    internal ToolStripItemCollection ParentToolStripItemCollection;

    internal ToolStripItemCollection RenderToolStripItemCollection
    {
      get
      {
        var array = this.GetModifiedParentToolStripItemEnumerable
          (
            this.RenderIdEnumerable,
            this.RenderCheckedEventHandler
          )
          .ToArray();

        return new ToolStripItemCollection
          (
            this.SelectRenderToolStripMenuItem
              .Owner,
            array
          );
      }
      set
      {
        this.PartialSetParentToolStripItemCollection(value);
        this.OnPropertyChanged(nameof(this.RenderToolStripItemCollection));
      }
    }
    internal ToolStrip ParentToolStrip { get; set; }
    internal ToolStripMenuItem SelectAllPropertyToolStripMenuItem { get; set; }
    internal ToolStripMenuItem SelectPropertyToolStripMenuItem { get; set; }
    internal ToolStripMenuItem SelectPropertyToolStripMenuItemDropDown { get; set; }

    private IEnumerable<uint> CaptureIdEnumerable
    {
      get
      {
        return this.DeviceGroupService
          .GetAllCapture()
          .Select(x => x.Id);
      }
    }

    private IEnumerable<uint> DisabledIdEnumerable
    {
      get
      {
        return this.DeviceGroupService
          .GetAllDisabled()
          .Select(x => x.Id);
      }
    }

    private IEnumerable<uint> EnabledIdEnumerable
    {
      get
      {
        return this.DeviceGroupService
          .GetAllEnabled()
          .Select(x => x.Id);
      }
    }

    private IEnumerable<uint> RenderIdEnumerable
    {
      get
      {
        return this.DeviceGroupService
          .GetAllRender()
          .Select(x => x.Id);
      }
    }

    protected virtual bool HasDisposed { get; set; }
    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    //private void DoSelect
    //(
    //  uint id
    //)
    //{
    //  this.SetParentToolStripItemCollection
    //}

    internal ToolStripItemCollection GetPropertyToolStripItemCollection
    (
      IEnumerable<uint> idEnumerable,
      EventHandler? checkedChangedEventHandler,
      ToolStrip toolStripOwner
    )
    {
      var array = this.GetModifiedParentToolStripItemEnumerable
        (
          idEnumerable,
          checkedChangedEventHandler
        )
        .ToArray();

      return new ToolStripItemCollection
        (
          toolStripOwner,
          array
        );
    }

    internal void SelectAll_Click
    (
      object sender,
      EventArgs eventArgs
    )
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

      //TODO: write code here.

    }

    internal void SelectAllCapture_Click
    (
      object sender,
      EventArgs eventArgs
    )
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

      //TODO: write code here.
    }

    internal void SelectAllDisabled_Click
    (
      object sender,
      EventArgs eventArgs
    )
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

      //TODO: write code here.
    }

    internal void SelectAllEnabled_Click
    (
      object sender,
      EventArgs eventArgs
    )
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

      //TODO: write code here.

    }

    internal void SelectAllRender_Click
    (
      object sender,
      EventArgs eventArgs
    )
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

      //TODO: write code here.

    }

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

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parentToolStrip">The tool strip</param>
    /// <param name="selectAllPropertyToolStripMenuItem">
    /// The tool strip menu item</param>
    /// <param name="selectPropertyToolStripMenuItem">
    /// The tool strip menu item</param>
    /// <param name="selectPropertyToolStripMenuItemDropDown">
    /// The tool strip menu item</param>
    public DeviceController
    (
      ToolStrip parentToolStrip,
      ToolStripMenuItem selectAllPropertyToolStripMenuItem,
      ToolStripMenuItem selectPropertyToolStripMenuItem,
      ToolStripMenuItem selectPropertyToolStripMenuItemDropDown
    )
    {
      this.DeviceGroupService =
        new DeviceGroupService
        <
          ReadonlyRepository
          <
            BaseService
            <
              BaseRepository<DeviceModel>,
              DeviceModel
            >
          >,
          BaseService
          <
            BaseRepository<DeviceModel>,
            DeviceModel
          >,
          BaseRepository<DeviceModel>,
          DeviceModel
        >();

      this.ParentToolStrip = parentToolStrip;
      this.SelectAllPropertyToolStripMenuItem = selectAllPropertyToolStripMenuItem;

      this.SelectPropertyToolStripMenuItemDropDown =
        selectPropertyToolStripMenuItemDropDown;

      this.SelectPropertyToolStripMenuItem = selectPropertyToolStripMenuItem;

      this.SetParentToolStripItemCollection();
    }

    private IEnumerable<uint> GetIdEnumerable
    (ToolStripItemCollection toolStripItemCollection)
    {
      if (toolStripItemCollection == null)
      {
        return Array.Empty<uint>();
      }

      var idStringEnumerable = toolStripItemCollection.Cast<ToolStripItem>()
        .ToArray()
        .Select(x => x.ToolTipText);

      return uintExtension.TryParse(idStringEnumerable);
    }

    private IEnumerable<ToolStripItem> GetModifiedParentToolStripItemEnumerable
    (
      IEnumerable<uint> idEnumerable,
      bool isChecked
    )
    {
      if (idEnumerable == null)
      {
        yield break;
      }

      foreach (var item in idEnumerable)
      {
        int index;

        try
        {
          index = (int)item;
        }
        catch
        {
          continue;
        }

        var toolStripItem = this.ParentToolStripItemCollection[index];

        if (toolStripItem.GetType() == typeof(ToolStripMenuItem))
        {
          (toolStripItem as ToolStripMenuItem).Checked = isChecked;
        }

        yield return toolStripItem;
      }
    }

    private IEnumerable<ToolStripItem> GetModifiedParentToolStripItemEnumerable
    (
      IEnumerable<uint> idEnumerable,
      EventHandler? checkedChanged
    )
    {
      if (idEnumerable == null)
      {
        yield break;
      }

      foreach (var item in idEnumerable)
      {
        int index;

        try
        {
          index = (int)item;
        }
        catch
        {
          continue;
        }

        var toolStripItem = this.ParentToolStripItemCollection[index];

        if (toolStripItem.GetType() == typeof(ToolStripMenuItem))
        {
          (toolStripItem as ToolStripMenuItem).CheckedChanged += checkedChanged;
        }

        yield return toolStripItem;
      }
    }

    private IEnumerable<ToolStripItem> GetPartialParentToolStripItemEnumerable
    (IEnumerable<uint> idEnumerable)
    {
      if (idEnumerable == null)
      {
        yield break;
      }

      foreach (var item in idEnumerable)
      {
        int index;

        try
        {
          index = (int)item;
        }
        catch
        {
          continue;
        }

        yield return this.ParentToolStripItemCollection[index];
      }
    }

    private IEnumerable<ToolStripMenuItem> GetToolStripMenuItemEnumerable
    (IEnumerable<DeviceModel> modelEnumerable)
    {
      if (modelEnumerable == null)
      {
        yield break;
      }

      foreach (var item in modelEnumerable)
      {
        yield return this.GetToolStripMenuItem(item);
      }
    }

    private ToolStripMenuItem GetToolStripMenuItem(DeviceModel deviceModel)
    {
      int maxIdLength = 7;

      string idWhiteSpace = new string
        (
          ' ',
          maxIdLength - deviceModel.Id
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
        deviceModel.Id,
        nameWhiteSpace,
        deviceModel.Name
      );

      return new ToolStripMenuItem(text)
      {
        CheckOnClick = true,

        ToolTipText = deviceModel.Id
          .ToString(),
      };
    }

    private void PartialSetParentToolStripItemCollection
    (ToolStripItemCollection toolStripItemCollection)
    {
      var idEnumerable = this.GetIdEnumerable(toolStripItemCollection);

      this.PartialSetParentToolStripItemCollection
        (
          toolStripItemCollection.Cast<ToolStripItem>()
            .ToArray(),
          idEnumerable
        );
    }

    private void PartialSetParentToolStripItemCollection
    (
      IEnumerable<ToolStripItem> enumerable,
      IEnumerable<uint> idEnumerable
    )
    {
      if (enumerable == null)
      {
        return;
      }

      if (idEnumerable == null)
      {
        return;
      }

      if
      (
        this.ParentToolStripItemCollection == null
        || this.ParentToolStripItemCollection
          .Count == 0
      )
      {
        this.SetParentToolStripItemCollection();
      }

      foreach (var item in idEnumerable)
      {
        int index;

        try
        {
          index = (int)item;
        }
        catch
        {
          continue;
        }

        int tempIndex = index++;
        var toolStripItem = enumerable.ElementAt(index);

        this.ParentToolStripItemCollection
          .Insert
          (
            tempIndex,
            toolStripItem
          );

        this.ParentToolStripItemCollection
          .RemoveAt(index);
      }
    }

    private void SetParentToolStripItemCollection()
    {
      if (this.ParentToolStrip == null)
      {
        this.ParentToolStripItemCollection = null;
      }

      var modelEnumerable = this.DeviceGroupService
        .SelectedRepository
        .GetAll();

      var array = this.GetToolStripMenuItemEnumerable(modelEnumerable)
        .ToArray();

      this.ParentToolStripItemCollection = new ToolStripItemCollection
        (
          this.ParentToolStrip,
          array
        );
    }

    private void SetParentToolStripItemCollection
    (
      bool isChecked
    )
    {
      if (this.ParentToolStrip == null)
      {
        this.ParentToolStripItemCollection = null;
      }

      var modelEnumerable = this.DeviceGroupService
        .SelectedRepository
        .GetAll();

      var array = this.GetToolStripMenuItemEnumerable(modelEnumerable)
        .ToArray();

      for (int index = 0; index < array.Length; index++)
      {
        array[index].Checked = isChecked;
      }

      this.ParentToolStripItemCollection = new ToolStripItemCollection
        (
          this.ParentToolStrip,
          array
        );
    }

    private void SortParentToolStripItemCollection()
    {
      if (this.ParentToolStripItemCollection == null)
      {
        return;
      }

      var array = this.ParentToolStripItemCollection
        .Cast<ToolStripMenuItem>()
        .OrderBy(x => x.ToolTipText)
        .ToArray();

      this.ParentToolStripItemCollection.Clear();
      this.ParentToolStripItemCollection.AddRange(array);
    }

    private void SetParentToolStripItemCollection
    (
      bool isChecked,
      IEnumerable<uint> idEnumerable
    )
    {
      if (this.ParentToolStrip == null)
      {
        this.ParentToolStripItemCollection = null;
      }

      var modelEnumerable = this.DeviceGroupService
        .SelectedService
        .GetRange(idEnumerable);

      var array = this.GetToolStripMenuItemEnumerable(modelEnumerable)
        .ToArray();

      for (int index = 0; index < array.Length; index++)
      {
        array[index].Checked = isChecked;
      }

      this.ParentToolStripItemCollection = new ToolStripItemCollection
        (
          this.ParentToolStrip,
          array
        );

      modelEnumerable = this.DeviceGroupService
        .SelectedService
        .GetAntiRange(idEnumerable);

      array = this.GetToolStripMenuItemEnumerable(modelEnumerable)
        .ToArray();

      this.ParentToolStripItemCollection.AddRange(array);
    }

    /// <summary>
    /// Dispose of unmanaged objects and true/false dispose of managed objects.
    /// </summary>
    /// <param name="isDisposed">True/false</param>
    protected virtual void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.DeviceGroupService
          .Dispose();

        this.ParentToolStripItemCollection = null;

        this.ParentToolStrip
          .Dispose();
      }

      this.HasDisposed = true;
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