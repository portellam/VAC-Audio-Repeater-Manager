using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  internal class DeviceController
  {
    #region Parameters

    internal HashSet<uint> SelectedDeviceIdHashSet
    {
      get
      {
        return this.selectedDeviceIdHashSet;
      }
      set
      {
        this.selectedDeviceIdHashSet = value;
        this.OnPropertyChanged(nameof(this.SelectedDeviceIdHashSet));
      }
    }

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

    internal ToolStrip ToolStrip { get; set; }

    private HashSet<uint> selectedDeviceIdHashSet { get; set; } =
      new HashSet<uint>();

    private IEnumerable<string> GetAllDisabledId
    {
      get
      {
        var idEnumerable = this.DeviceGroupService
          .GetAllDisabled()
          .Select(x => x.Id);

        foreach (var item in idEnumerable)
        {
          yield return item.ToString();
        }
      }
    }

    private IEnumerable<string> GetAllEnabledId
    {
      get
      {
        var idEnumerable = this.DeviceGroupService
          .GetAllEnabled()
          .Select(x => x.Id);

        foreach (var item in idEnumerable)
        {
          yield return item.ToString();
        }
      }
    }

    private ToolStripItemCollection InputToolStripItemCollection { get; set; }

    private ToolStripItemCollection? ToolStripItemCollection
    { get; set; }

    private ToolStripItemCollection CaptureToolStripItemCollection
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
        this.SetToolStripItemCollection(value);
      }
    }

    IEnumerable<uint> CaptureIdEnumerable
    {
      get
      {
        return this.DeviceGroupService
          .GetAllCapture()
          .Select(x => x.Id);
      }
    }

    IEnumerable<string> DisabledIdEnumerable
    {
      get
      {
        return this.DeviceGroupService
          .GetAllDisabled()
          .Select
          (
            x =>
            x.Id
              .ToString()
          );
      }
    }

    IEnumerable<string> EnabledIdEnumerable
    {
      get
      {
        return this.DeviceGroupService
          .GetAllEnabled()
          .Select
          (
            x =>
            x.Id
              .ToString()
          );
      }
    }
    
    IEnumerable<string> RenderIdEnumerable
    {
      get
      {
        return this.DeviceGroupService
          .GetAllRender()
          .Select
          (
            x =>
            x.Id
              .ToString()
          );
      }
    }

    IEnumerable<string> SelectedIdEnumerable { get; set; }

    #endregion

    #region Logic

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

        yield return this.ToolStripItemCollection[index];
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

    private ToolStripItemCollection? GetToolStripItemCollection()
    {
      if (this.ToolStrip == null)
      {
        return null;
      }

      var modelEnumerable = this.DeviceGroupService
        .SelectedRepository
        .GetAll();

      var array = this.GetToolStripMenuItemEnumerable(modelEnumerable)
        .ToArray();

      return new ToolStripItemCollection
        (
          this.ToolStrip,
          array
        );
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

    private void SetToolStripItemCollection
    (ToolStripItemCollection toolStripItemCollection)
    {
      if (toolStripItemCollection == null)
      {
        return;
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

        this.ToolStripItemCollection.Insert
          (
            index,
            toolStripItem
          );

        int oldIndex = index + 1;

        this.ToolStripItemCollection
          .RemoveAt(oldIndex);
      }
    }

    #endregion
  }
}