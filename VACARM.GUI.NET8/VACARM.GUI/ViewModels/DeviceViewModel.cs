using VACARM.Domain.Models;
using VACARM.GUI.Structs;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.ViewModels
{
  /// <summary>
  /// The view model of <typeparamref name="DeviceGroupService"/>.
  /// </summary>
  public partial class DeviceViewModel
    <
      DeviceGroupService,
      TDeviceModel
    > :
    BaseViewModel
    <
      DeviceGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TDeviceModel>,
            TDeviceModel
          >
        >,
        BaseService
        <
          BaseRepository<TDeviceModel>,
          TDeviceModel
        >,
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >,
      TDeviceModel
    > where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    private ToolStripMenuItem SelectAbsentToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsAbsent,
            "Absent"
          );
      }
    }

    private ToolStripMenuItem SelectCaptureToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsCapture,
            "Capture"
          );
      }
    }

    private ToolStripMenuItem SelectCommunicationsToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsCommunications,
            "Communications"
          );
      }
    }

    private ToolStripMenuItem SelectConsoleToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsConsole,
            "Console"
          );
      }
    }

    private ToolStripMenuItem SelectDefaultToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsDefault,
            "Default"
          );
      }
    }
    
    private ToolStripMenuItem SelectDisabledToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsDisabled,
            "Disabled"
          );
      }
    }

    private ToolStripMenuItem SelectEnabledToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsEnabled,
            "Enabled"
          );
      }
    }

    private ToolStripMenuItem SelectMultimediaToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsMultimedia,
            "Multimedia"
          );
      }
    }

    private ToolStripMenuItem SelectMutedToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsMuted,
            "Muted"
          );
      }
    }

    private ToolStripMenuItem SelectPresentToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsPresent,
            "Present"
          );
      }
    }

    private ToolStripMenuItem SelectRenderToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsRender,
            "Output"
          );
      }
    }

    private ToolStripMenuItem SelectUnmutedToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsUnmuted,
            "Unmuted"
          );
      }
    }

    protected override IEnumerable<ToolStripItem> SelectRangeToolStripItemEnumerable
    {
      get
      {
        return new ToolStripItem[]
          {
            SelectAllCaptureToolStripMenuItem,
            SelectAllRenderToolStripMenuItem,
            new ToolStripSeparator(),
            SelectAllDefaultToolStripMenuItem,
            new ToolStripSeparator(),
            SelectAllPresentToolStripMenuItem,
            SelectAllAbsentToolStripMenuItem,
            new ToolStripSeparator(),
            SelectAllEnabledToolStripMenuItem,
            SelectAllDisabledToolStripMenuItem,
            new ToolStripSeparator(),
            SelectAllMutedToolStripMenuItem,
            SelectAllUnmutedToolStripMenuItem,
            new ToolStripSeparator(),
            SelectAllCommunicationsToolStripMenuItem,
            SelectAllConsoleToolStripMenuItem,
            SelectAllMultimediaToolStripMenuItem
          };
      }
    }

    protected override IEnumerable<ToolStripItem> SelectToolStripItemEnumerable
    {
      get
      {
        return new ToolStripItem[]
          {
            SelectCaptureToolStripMenuItem,
            SelectRenderToolStripMenuItem,
            new ToolStripSeparator(),
            SelectDefaultToolStripMenuItem,
            new ToolStripSeparator(),
            SelectPresentToolStripMenuItem,
            SelectAbsentToolStripMenuItem,
            new ToolStripSeparator(),
            SelectEnabledToolStripMenuItem,
            SelectDisabledToolStripMenuItem,
            new ToolStripSeparator(),
            SelectMutedToolStripMenuItem,
            SelectUnmutedToolStripMenuItem,
            new ToolStripSeparator(),
            SelectCommunicationsToolStripMenuItem,
            SelectConsoleToolStripMenuItem,
            SelectMultimediaToolStripMenuItem
          };
      }
    }

    public DeviceGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TDeviceModel>,
            TDeviceModel
          >
        >,
        BaseService
        <
          BaseRepository<TDeviceModel>,
          TDeviceModel
        >,
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >
    GroupService
    { get; set; }

    public ToolStripMenuItem SelectAllAbsentToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsAbsent,
            "Absent"
          );
      }
    }

    public ToolStripMenuItem SelectAllCaptureToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsCapture,
            "Capture"
          );
      }
    }

    public ToolStripMenuItem SelectAllCommunicationsToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsCommunications,
            "Communications"
          );
      }
    }

    public ToolStripMenuItem SelectAllConsoleToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsConsole,
            "Console"
          );
      }
    }

    public ToolStripMenuItem SelectAllDefaultToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsDefault,
            "Default"
          );
      }
    }

    public ToolStripMenuItem SelectAllDisabledToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsDisabled,
            "Disabled"
          );
      }
    }

    public ToolStripMenuItem SelectAllEnabledToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsEnabled,
            "Enabled"
          );
      }
    }

    public ToolStripMenuItem SelectAllMultimediaToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsMultimedia,
            "Multimedia"
          );
      }
    }

    public ToolStripMenuItem SelectAllMutedToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsMuted,
            "Muted"
          );
      }
    }

    public ToolStripMenuItem SelectAllPresentToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsPresent,
            "Present"
          );
      }
    }

    public ToolStripMenuItem SelectAllRenderToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsRender,
            "Render"
          );
      }
    }

    public ToolStripMenuItem SelectAllUnmutedToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            DeviceFunctions<TDeviceModel>.IsUnmuted,
            "Unmuted"
          );
      }
    }

    public override Func<DeviceModel, string> NameFunc
    {
      get
      {
        return (DeviceModel x) => x.Name;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public DeviceViewModel() :
      base()
    {
      this.GroupService = new DeviceGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TDeviceModel>,
            TDeviceModel
          >
        >,
        BaseService
        <
          BaseRepository<TDeviceModel>,
          TDeviceModel
        >,
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >();

      this.Update();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="defaultSize">The default size</param>
    /// <param name="selectDefaultName">The select item default name</param>
    public DeviceViewModel
    (
      Size defaultSize,
      string selectDefaultName
    ) :
      base
    (
      defaultSize,
      selectDefaultName
    )
    {
      this.GroupService = new DeviceGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TDeviceModel>,
            TDeviceModel
          >
        >,
        BaseService
        <
          BaseRepository<TDeviceModel>,
          TDeviceModel
        >,
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >();
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
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}