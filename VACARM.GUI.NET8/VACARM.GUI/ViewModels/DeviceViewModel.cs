using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.ViewModels
{
  /// <summary>
  /// The view model of <typeparamref name="DeviceGroupService"/>.
  /// </summary>
  internal partial class DeviceViewModel
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

    internal ToolStripMenuItem AllAbsentToolStripMenuItem
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

    internal ToolStripMenuItem AllCaptureToolStripMenuItem
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

    internal ToolStripMenuItem AllCommunicationsToolStripMenuItem
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

    internal ToolStripMenuItem AllConsoleToolStripMenuItem
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

    internal ToolStripMenuItem AllDisabledToolStripMenuItem
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

    internal ToolStripMenuItem AllEnabledToolStripMenuItem
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

    internal ToolStripMenuItem AllMultimediaToolStripMenuItem
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

    internal ToolStripMenuItem AllMutedToolStripMenuItem
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

    internal ToolStripMenuItem AllPresentToolStripMenuItem
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

    internal ToolStripMenuItem AllRenderToolStripMenuItem
    {
      get
      {
        return this.GetToolStripMenuItemWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            DeviceFunctions<TDeviceModel>.IsRender,
            "Render"
          );
      }
    }

    internal ToolStripMenuItem AllUnmutedToolStripMenuItem
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
    internal DeviceViewModel() :
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

    /// <summary>
    /// Deconstructor
    /// </summary>
    /// <param name="allAbsentToolStripMenuItem">The tool strip menu item</param>
    /// <param name="allCaptureToolStripMenuItem">The tool strip menu item</param>
    /// <param name="allCommunicationsToolStripMenuItem">
    /// The tool strip menu item
    /// </param>
    /// <param name="allConsoleToolStripMenuItem">The tool strip menu item</param>
    /// <param name="allDisabledToolStripMenuItem">The tool strip menu item</param>
    /// <param name="allEnabledToolStripMenuItem">The tool strip menu item</param>
    /// <param name="allMultimediaToolStripMenuItem">
    /// The tool strip menu item
    /// </param>
    /// <param name="allMutedToolStripMenuItem">The tool strip menu item</param>
    /// <param name="allPresentToolStripMenuItem">The tool strip menu item</param>
    /// <param name="allRenderToolStripMenuItem">The tool strip menu item</param>
    /// <param name="allUnmutedToolStripMenuItem">The tool strip menu item</param>
    /// <param name="captureToolStripMenuItem">The tool strip menu item</param>
    /// <param name="defaultToolStripMenuItem">The tool strip menu item</param>
    /// <param name="renderToolStripMenuItem">The tool strip menu item</param>
    internal void Deconstruct
    (
      ref ToolStripMenuItem captureToolStripMenuItem,
      ref ToolStripMenuItem defaultToolStripMenuItem,
      ref ToolStripMenuItem renderToolStripMenuItem,
      ref ToolStripMenuItem allAbsentToolStripMenuItem,
      ref ToolStripMenuItem allCaptureToolStripMenuItem,
      ref ToolStripMenuItem allCommunicationsToolStripMenuItem,
      ref ToolStripMenuItem allConsoleToolStripMenuItem,
      ref ToolStripMenuItem allDisabledToolStripMenuItem,
      ref ToolStripMenuItem allEnabledToolStripMenuItem,
      ref ToolStripMenuItem allMultimediaToolStripMenuItem,
      ref ToolStripMenuItem allMutedToolStripMenuItem,
      ref ToolStripMenuItem allPresentToolStripMenuItem,
      ref ToolStripMenuItem allRenderToolStripMenuItem,
      ref ToolStripMenuItem allUnmutedToolStripMenuItem
    )
    {
      allAbsentToolStripMenuItem = this.AllAbsentToolStripMenuItem;
      allCaptureToolStripMenuItem = this.AllCaptureToolStripMenuItem;
      allDisabledToolStripMenuItem = this.AllDisabledToolStripMenuItem;
      allCommunicationsToolStripMenuItem = this.AllCommunicationsToolStripMenuItem;
      allConsoleToolStripMenuItem = this.AllConsoleToolStripMenuItem;
      allEnabledToolStripMenuItem = this.AllEnabledToolStripMenuItem;
      allMultimediaToolStripMenuItem = this.AllMultimediaToolStripMenuItem;
      allMutedToolStripMenuItem = this.AllMutedToolStripMenuItem;
      allPresentToolStripMenuItem = this.AllPresentToolStripMenuItem;
      allRenderToolStripMenuItem = this.AllRenderToolStripMenuItem;
      allUnmutedToolStripMenuItem = this.AllUnmutedToolStripMenuItem;
      captureToolStripMenuItem = this.CaptureToolStripMenuItem;
      defaultToolStripMenuItem = this.DefaultToolStripMenuItem;
      renderToolStripMenuItem = this.RenderToolStripMenuItem;
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