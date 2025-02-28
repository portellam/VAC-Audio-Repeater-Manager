using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  internal class DeviceController
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

    private IEnumerable<uint> SelectedIdEnumerable { get; set; }

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
        this.PartialSetToolStripItemCollection(value);
      }
    }

    private ToolStripItemCollection DisabledToolStripItemCollection
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
        this.PartialSetToolStripItemCollection(value);
      }
    }

    private ToolStripItemCollection EnabledToolStripItemCollection
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
        this.PartialSetToolStripItemCollection(value);
      }
    }

    private ToolStripItemCollection RenderToolStripItemCollection
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
        this.PartialSetToolStripItemCollection(value);
      }
    }

    private ToolStripItemCollection ToolStripItemCollection
    { get; set; }


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
      this.SetToolStripItemCollection();
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

    private void PartialSetToolStripItemCollection
    (ToolStripItemCollection toolStripItemCollection)
    {
      if (toolStripItemCollection == null)
      {
        return;
      }

      if
      (
        this.ToolStripItemCollection == null
        || this.ToolStripItemCollection
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

        this.ToolStripItemCollection
          .Insert
          (
            tempIndex,
            toolStripItem
          );

        this.ToolStripItemCollection
          .RemoveAt(index);
      }
    }

    private void SetToolStripItemCollection()
    {
      if (this.ToolStrip == null)
      {
        this.ToolStripItemCollection = null;
      }

      var modelEnumerable = this.DeviceGroupService
        .SelectedRepository
        .GetAll();

      var array = this.GetToolStripMenuItemEnumerable(modelEnumerable)
        .ToArray();

      this.ToolStripItemCollection = new ToolStripItemCollection
        (
          this.ToolStrip,
          array
        );
    }

    #endregion
  }
}