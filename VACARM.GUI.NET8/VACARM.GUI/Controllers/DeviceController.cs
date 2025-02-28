using System.ComponentModel;
using System.Diagnostics;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
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

    internal IEnumerable<uint> SelectedIdEnumerable { get; set; }

    internal ToolStripItemCollection CaptureToolStripItemCollection
    {
      get
      {
        var array = this.GetPartialParentToolStripItemEnumerable(this.CaptureIdEnumerable)
          .ToArray();

        return new ToolStripItemCollection
          (
            this.CaptureToolStrip,
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

    internal ToolStripItemCollection ParentToolStripItemCollection
    { get; set; }

    internal ToolStripItemCollection RenderToolStripItemCollection
    {
      get
      {
        var array = this.GetPartialParentToolStripItemEnumerable
          (this.RenderIdEnumerable)
          .ToArray();

        return new ToolStripItemCollection
          (
            this.RenderToolStrip,
            array
          );
      }
      set
      {
        this.PartialSetParentToolStripItemCollection(value);
        this.OnPropertyChanged(nameof(this.RenderToolStripItemCollection));
      }
    }

    internal ToolStrip CaptureToolStrip { get; set; }
    internal ToolStrip ParentToolStrip { get; set; }
    internal ToolStrip RenderToolStrip { get; set; }

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

    internal void SetToolStripItemCollection
    (
      ref ToolStripItemCollection toolStripItemCollection,
      Action action,
      Func<ToolStripItem, bool> func
    )
    {
      if (toolStripItemCollection == null)
      {
        return;
      }


      if (action == null)
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      var array = this.GetPartialParentToolStripItemEnumerable(func)
        .ToArray();

      toolStripItemCollection.Clear();
      toolStripItemCollection.AddRange(array);
    }

    internal void SetToolStripItemCollection
    (
      ref ToolStripItemCollection toolStripItemCollection,
      Action action,
      IEnumerable<uint> idEnumerable
    )
    {
      if (toolStripItemCollection == null)
      {
        return;
      }


      if (action == null)
      {
        return;
      }

      if (idEnumerable == null)
      {
        return;
      }

      var array = this.GetPartialParentToolStripItemEnumerable(idEnumerable)
        .ToArray();

      toolStripItemCollection.Clear();
      toolStripItemCollection.AddRange(array);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="parentToolStrip">The parent tool strip</param>
    /// <param name="captureToolStrip">The capture tool strip</param>
    /// <param name="renderToolStrip">The render tool strip</param>
    public DeviceController
    (
      ToolStrip parentToolStrip,
      ToolStrip captureToolStrip,
      ToolStrip renderToolStrip
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
      this.SetParentToolStripItemCollection();

      this.SetToolStripItemCollection
        (
          this.CaptureToolStripItemCollection,
          null,


    }

    private IEnumerable<ToolStripItem> GetPartialParentToolStripItemEnumerable
    (Func<ToolStripItem, bool> func)
    {
      if (func == null)
      {
        return Array.Empty<ToolStripItem>();
      }

      return this.ParentToolStripItemCollection
        .OfType<ToolStripItem>()
        .Where(func);
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

    private uint? GetToolStripItemCollectionId(ToolStripItem toolStripItem)
    {
      if (toolStripItem == null)
      {
        return null;
      }

      var idString = toolStripItem.ToolTipText;

      var result = uint.TryParse
        (
          idString,
          out uint id
        );

      if (!result)
      {
        return null;
      }

      return id;
    }

    private void PartialSetParentToolStripItemCollection
    (ToolStripItemCollection toolStripItemCollection)
    {
      if (toolStripItemCollection == null)
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

      foreach (var item in toolStripItemCollection)
      {
        if (item == null)
        {
          continue;
        }

        if (item.GetType() != typeof(ToolStripItem))
        {
          continue;
        }

        var toolStripItem = item as ToolStripItem;
        uint? id = this.GetToolStripItemCollectionId(toolStripItem);

        if (id == null)
        {
          continue;
        }

        int index;

        try
        {
          index = (int)id;
        }
        catch
        {
          continue;
        }

        int tempIndex = index++;

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