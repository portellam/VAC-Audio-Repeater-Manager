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
    internal DeviceViewModel
    (
      ref ToolStripMenuItem selectInputToolStripMenuItem,
      ref ToolStripMenuItem selectOutputToolStripMenuItem
    ) :
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
    /// <param name="captureToolStripMenuItem">The tool strip menu item</param>
    /// <param name="renderoolStripMenuItem">The tool strip menu item</param>
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
    internal void Deconstruct
    (
      ref ToolStripMenuItem captureToolStripMenuItem,
      ref ToolStripMenuItem renderoolStripMenuItem,
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
      allAbsentToolStripMenuItem = this.AbsentToolStripMenuItem;
      allDisabledToolStripMenuItem = this.DisabledToolStripMenuItem;
      allCommunicationsToolStripMenuItem = this.CommunicationsToolStripMenuItem;
      allConsoleToolStripMenuItem = this.ConsoleToolStripMenuItem;
      allEnabledToolStripMenuItem = this.EnabledToolStripMenuItem;
      allMultimediaToolStripMenuItem = this.MultimediaToolStripMenuItem;
      allMutedToolStripMenuItem = this.MutedToolStripMenuItem;
      allPresentToolStripMenuItem = this.PresentToolStripMenuItem;
      allUnmutedToolStripMenuItem = this.UnmutedToolStripMenuItem;
      captureToolStripMenuItem = this.CaptureToolStripMenuItem;
      renderoolStripMenuItem = this.RenderToolStripMenuItem;
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