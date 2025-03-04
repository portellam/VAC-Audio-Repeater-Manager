using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.Controllers
{
  /// <summary>
  /// The controller of <typeparamref name="DeviceGroupService"/>.
  /// </summary>
  internal partial class DeviceController
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

    internal DeviceGroupService
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

    internal ToolStripMenuItem AbsentToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsAbsent,
            "Absent"
          );
      }
    }

    internal ToolStripMenuItem CaptureToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsCapture,
            "Capture"
          );
      }
    }

    internal ToolStripMenuItem CommunicationsToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsCommunications,
            "Communications"
          );
      }
    }

    internal ToolStripMenuItem ConsoleToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsConsole,
            "Console"
          );
      }
    }

    internal ToolStripMenuItem DefaultToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsDefault,
            "Default"
          );
      }
    }

    internal ToolStripMenuItem DisabledToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsDisabled,
            "Disabled"
          );
      }
    }

    internal ToolStripMenuItem EnabledToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsEnabled,
            "Enabled"
          );
      }
    }

    internal ToolStripMenuItem MultimediaToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsMultimedia,
            "Multimedia"
          );
      }
    }

    internal ToolStripMenuItem MutedToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsMuted,
            "Muted"
          );
      }
    }

    internal ToolStripMenuItem PresentToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsPresent,
            "Present"
          );
      }
    }

    internal ToolStripMenuItem RenderToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsRender,
            "Output"
          );
      }
    }

    internal ToolStripMenuItem UnmutedToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsUnmuted,
            "Unmuted"
          );
      }
    }

    internal override Func<DeviceModel, string> NameFunc
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
    internal DeviceController() :
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