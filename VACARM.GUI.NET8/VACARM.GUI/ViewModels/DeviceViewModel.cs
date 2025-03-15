using System.Reflection;
using VACARM.Domain.Models;
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
      TBaseGroupService,
      TDeviceModel
    > :
    BaseViewModel
    <
      TBaseGroupService,
      TDeviceModel
    >
    where TBaseGroupService :
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
    >
    where TDeviceModel :
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

    private ToolStripMenuItem SelectAllAbsentToolStripMenuItem
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

    private ToolStripMenuItem SelectAllCaptureToolStripMenuItem
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

    private ToolStripMenuItem SelectAllCommunicationsToolStripMenuItem
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

    private ToolStripMenuItem SelectAllConsoleToolStripMenuItem
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

    private ToolStripMenuItem SelectAllDefaultToolStripMenuItem
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

    private ToolStripMenuItem SelectAllDisabledToolStripMenuItem
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

    private ToolStripMenuItem SelectAllEnabledToolStripMenuItem
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

    private ToolStripMenuItem SelectAllMultimediaToolStripMenuItem
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

    private ToolStripMenuItem SelectAllMutedToolStripMenuItem
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

    private ToolStripMenuItem SelectAllPresentToolStripMenuItem
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

    private ToolStripMenuItem SelectAllRenderToolStripMenuItem
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

    private ToolStripMenuItem SelectAllUnmutedToolStripMenuItem
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

    protected override ToolStripItem[] SelectRangeToolStripItemArray
    {
      get
      {
        return new ToolStripItem[]
          {
            SelectAllCaptureToolStripMenuItem.GetClone(),
            SelectAllRenderToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectAllDefaultToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectAllPresentToolStripMenuItem.GetClone(),
            SelectAllAbsentToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectAllEnabledToolStripMenuItem.GetClone(),
            SelectAllDisabledToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectAllMutedToolStripMenuItem.GetClone(),
            SelectAllUnmutedToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectAllCommunicationsToolStripMenuItem.GetClone(),
            SelectAllConsoleToolStripMenuItem.GetClone(),
            SelectAllMultimediaToolStripMenuItem.GetClone(),
          };
      }
    }

    protected override ToolStripItem[] SelectToolStripItemArray
    {
      get
      {
        return new ToolStripItem[]
          {
            SelectCaptureToolStripMenuItem.GetClone(),
            SelectRenderToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectDefaultToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectPresentToolStripMenuItem.GetClone(),
            SelectAbsentToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectEnabledToolStripMenuItem.GetClone(),
            SelectDisabledToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectMutedToolStripMenuItem.GetClone(),
            SelectUnmutedToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectCommunicationsToolStripMenuItem.GetClone(),
            SelectConsoleToolStripMenuItem.GetClone(),
            SelectMultimediaToolStripMenuItem.GetClone(),
          };
      }
    }

    public new DeviceGroupService
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
    {
      get
      {
        return
          (
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
            >
          )base.GroupService;
      }
      set
      {
        base.GroupService = value;
        base.OnPropertyChanged(nameof(this.GroupService));
      }
    }

    public override Func<DeviceModel, string> TextFunc
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

    #endregion
  }
}