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
        var array = this.GetToolStripItemEnumerable(this.CaptureIdEnumerable)
          .ToArray();

        return new ToolStripItemCollection
          (
            this.ToolStrip,
            array
          );
      }
      set
      {
        this.PartialSetParentToolStripItemCollection(value);
        this.OnPropertyChanged(nameof(this.CaptureToolStripItemCollection));
      }
    }

    internal ToolStripItemCollection DisabledToolStripItemCollection
    {
      get
      {
        var array = this.GetToolStripItemEnumerable(this.DisabledIdEnumerable)
          .ToArray();

        return new ToolStripItemCollection
          (
            this.ToolStrip,
            array
          );
      }
      set
      {
        this.PartialSetParentToolStripItemCollection(value);
        this.OnPropertyChanged(nameof(this.DisabledToolStripItemCollection));
      }
    }

    internal ToolStripItemCollection EnabledToolStripItemCollection
    {
      get
      {
        var array = this.GetToolStripItemEnumerable(this.EnabledIdEnumerable)
          .ToArray();

        return new ToolStripItemCollection
          (
            this.ToolStrip,
            array
          );
      }
      set
      {
        this.PartialSetParentToolStripItemCollection(value);
        this.OnPropertyChanged(nameof(this.EnabledToolStripItemCollection));
      }
    }

    internal ToolStripItemCollection ParentToolStripItemCollection
    { get; set; }

    internal ToolStripItemCollection RenderToolStripItemCollection
    {
      get
      {
        var array = this.GetToolStripItemEnumerable(this.RenderIdEnumerable)
          .ToArray();

        return new ToolStripItemCollection
          (
            this.ToolStrip,
            array
          );
      }
      set
      {
        this.PartialSetParentToolStripItemCollection(value);
        this.OnPropertyChanged(nameof(this.RenderToolStripItemCollection));
      }
    }

    internal ToolStrip ToolStrip { get; set; }

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

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="toolStripItem">The tool strip</param>
    public DeviceController(ToolStrip toolStrip)
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

      this.ToolStrip = toolStrip;
      this.SetParentToolStripItemCollection();
    }

    private IEnumerable<ToolStripItem> GetToolStripItemEnumerable
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
      if (this.ToolStrip == null)
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
          this.ToolStrip,
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

        this.ToolStrip
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