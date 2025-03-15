using VACARM.Domain.Models;
using VACARM.GUI.Structs;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.GUI.ViewModels
{
  /// <summary>
  /// The view model of <typeparamref name="RepeaterGroupService"/>.
  /// Manages <typeparamref name="DeviceViewModel"/>.
  /// </summary>
  public partial class RepeaterViewModel
    <
      TBaseGroupService,
      TRepeaterModel
    > :
    BaseViewModel
    <
      TBaseGroupService,
      TRepeaterModel
    >
    where TBaseGroupService :
    RepeaterGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TRepeaterModel>,
          TRepeaterModel
        >
      >,
      BaseService
      <
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >,
      BaseRepository<TRepeaterModel>,
      TRepeaterModel
    >
    where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    public new RepeaterGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TRepeaterModel>,
            TRepeaterModel
          >
        >,
        BaseService
        <
          BaseRepository<TRepeaterModel>,
          TRepeaterModel
        >,
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >
    GroupService
    {
      get
      {
        return
          (
            RepeaterGroupService
            <
              ReadonlyRepository
              <
                BaseService
                <
                  BaseRepository<TRepeaterModel>,
                  TRepeaterModel
                >
              >,
              BaseService
              <
                BaseRepository<TRepeaterModel>,
                TRepeaterModel
              >,
              BaseRepository<TRepeaterModel>,
              TRepeaterModel
            >
          )base.GroupService;
      }
      set
      {
        base.GroupService = value;
        base.OnPropertyChanged(nameof(this.GroupService));
      }
    }

    private ToolStripMenuItem SelectAllAbsentToolStripMenuItem
    {
      get
      {
        var deviceIdEnumerable = this.DeviceViewModel
          .GroupService
          .GetAllAbsent()
          .Select(x => x.Id);

        return this.GetNew
          (
            deviceIdEnumerable,
            "Absent"
          );
      }
    }

    private ToolStripMenuItem SelectAllDisabledToolStripMenuItem
    {
      get
      {
        List<uint> deviceIdEnumerable = this.DeviceViewModel
          .GroupService
          .GetAllDisabled()
          .Select(x => x.Id)
          .ToList();

        return this.GetNew
          (
            deviceIdEnumerable,
            "Disabled"
          );
      }
    }

    private ToolStripMenuItem SelectAllEnabledToolStripMenuItem
    {
      get
      {
        var deviceIdEnumerable = this.DeviceViewModel
          .GroupService
          .GetAllEnabled()
          .Select(x => x.Id);

        return this.GetNew
          (
            deviceIdEnumerable,
            "Enabled"
          );
      }
    }

    private ToolStripMenuItem SelectAllPresentToolStripMenuItem
    {
      get
      {
        var deviceIdEnumerable = this.DeviceViewModel
          .GroupService
          .GetAllPresent()
          .Select(x => x.Id);

        return this.GetNew
          (
            deviceIdEnumerable,
            "Present"
          );
      }
    }

    private ToolStripMenuItem SelectAllStartedToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            RepeaterFunctions<TRepeaterModel>.IsStarted,
            "Started"
          );
      }
    }

    private ToolStripMenuItem SelectAllStoppedToolStripMenuItem
    {
      get
      {
        return this.GetNew
          (
            RepeaterFunctions<TRepeaterModel>.IsStopped,
            "Stopped"
          );
      }
    }

    private ToolStripMenuItem SelectAbsentToolStripMenuItem
    {
      get
      {
        var deviceIdEnumerable = this.DeviceViewModel
          .GroupService
          .GetAllAbsent()
          .Select(x => x.Id);

        return this.GetNewWithDropDownItems
          (
            deviceIdEnumerable,
            "Absent"
          );
      }
    }

    private ToolStripMenuItem SelectDisabledToolStripMenuItem
    {
      get
      {
        var deviceIdEnumerable = this.DeviceViewModel
          .GroupService
          .GetAllDisabled()
          .Select(x => x.Id);

        return this.GetNewWithDropDownItems
          (
            deviceIdEnumerable,
            "Disabled"
          );
      }
    }

    private ToolStripMenuItem SelectEnabledToolStripMenuItem
    {
      get
      {
        var deviceIdEnumerable = this.DeviceViewModel
          .GroupService
          .GetAllEnabled()
          .Select(x => x.Id);

        return this.GetNewWithDropDownItems
          (
            deviceIdEnumerable,
            "Present"
          );
      }
    }

    private ToolStripMenuItem SelectPresentToolStripMenuItem
    {
      get
      {
        var deviceIdEnumerable = this.DeviceViewModel
          .GroupService
          .GetAllPresent()
          .Select(x => x.Id);

        return this.GetNewWithDropDownItems
          (
            deviceIdEnumerable,
            "Present"
          );
      }
    }

    private ToolStripMenuItem SelectStartedToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            RepeaterFunctions<TRepeaterModel>.IsStarted,
            "Started"
          );
      }
    }

    private ToolStripMenuItem SelectStoppedToolStripMenuItem
    {
      get
      {
        return this.GetNewWithDropDownItems
          (
            this.GroupService
              .SelectedRepository
              .GetAll(),
            RepeaterFunctions<TRepeaterModel>.IsStopped,
            "Stopped"
          );
      }
    }

    protected override ToolStripItem[] SelectRangeToolStripItemArray
    {
      get
      {
        return new ToolStripItem[]
          {
            SelectAllStartedToolStripMenuItem.GetClone(),
            SelectAllStoppedToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectAllPresentToolStripMenuItem.GetClone(),
            SelectAllAbsentToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectAllEnabledToolStripMenuItem.GetClone(),
            SelectAllDisabledToolStripMenuItem.GetClone(),
          };
      }
    }

    protected override ToolStripItem[] SelectToolStripItemArray
    {
      get
      {
        return new ToolStripItem[]
          {
            SelectStartedToolStripMenuItem.GetClone(),
            SelectStoppedToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectPresentToolStripMenuItem.GetClone(),
            SelectAbsentToolStripMenuItem.GetClone(),
            new ToolStripSeparator(),
            SelectEnabledToolStripMenuItem.GetClone(),
            SelectDisabledToolStripMenuItem.GetClone(),
          };
      }
    }

    public DeviceViewModel
      <
        DeviceGroupService
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
        >,
        DeviceModel
      > DeviceViewModel
    { get; set; }

    public override Func<RepeaterModel, string> TextFunc
    {
      get
      {
        return (RepeaterModel x) => x.WindowName;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Get the enumerable of ID(s) given the enumerable of device ID(s).
    /// </summary>
    /// <param name="deviceIdEnumerable">the enumerable of device ID(s)</param>
    /// <returns>The enumerable of ID(s).</returns>
    private IEnumerable<uint> GetIdRange(IEnumerable<uint> deviceIdEnumerable)
    {
      List<uint> idList = new List<uint>();

      if (deviceIdEnumerable.IsNullOrEmpty())
      {
        return idList;
      }

      foreach (var item in deviceIdEnumerable)
      {
        List<uint> thisIdList = this.GroupService
          .SelectedRepository
          .GetRange(RepeaterFunctions<TRepeaterModel>
          .ContainsDeviceId(item))
          .Select(x => x.Id)
          .ToList();

        if (idList.IsNullOrEmpty())
        {
          continue;
        }

        idList.Concat(thisIdList);
      }

      return idList.AsEnumerable();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public RepeaterViewModel() :
      base()
    {
      this.DeviceViewModel = new DeviceViewModel
        <
          DeviceGroupService
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
          >,
          DeviceModel
        >();

      this.GroupService = new RepeaterGroupService
      <
        ReadonlyRepository
        <
          BaseService
          <
            BaseRepository<TRepeaterModel>,
            TRepeaterModel
          >
        >,
        BaseService
        <
          BaseRepository<TRepeaterModel>,
          TRepeaterModel
        >,
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >();

      this.Update();
    }

    // TODO: add constructor to match DeviceViewModel constructor.
    // TODO: instead of DeviceIdEnumerable, use Function, for less redundancy?

    /// <summary>
    /// Get a new <typeparamref name="ToolStripMenuItem"/>.
    /// </summary>
    /// <param name="deviceIdEnumerable">The enumerable of device ID(s)</param>
    /// <param name="text">The text</param>
    /// <returns>The tool strip menu item.</returns>
    public ToolStripMenuItem GetNew
    (
      IEnumerable<uint> deviceIdEnumerable,
      string text
    )
    {
      List<uint> idList = this.GetIdRange(deviceIdEnumerable)
        .ToList();

      ToolStripMenuItem toolStripMenuItem = 
        DefaultBaseViewModel.SelectToolStripMenuItem;

      toolStripMenuItem.Text = string.Format
        (
          toolStripMenuItem.Text,
          "with " + text + " devices"
        );

      if (idList.IsNullOrEmpty())
      {
        toolStripMenuItem.Enabled = false;
      }

      else
      {
        toolStripMenuItem.CheckedChanged +=
          this.SelectRangeCheckedChangedEventHandler
          (
            idList,
            true
          );
      }

      return toolStripMenuItem;
    }

    /// <summary>
    /// Get a new <typeparamref name="ToolStripMenuItem"/> with drop down items.
    /// </summary>
    /// <param name="deviceIdEnumerable">The enumerable of device ID(s)</param>
    /// <param name="name">The name</param>
    /// <returns>The tool strip menu item.</returns>
    public new ToolStripMenuItem GetNewWithDropDownItems
    (
      IEnumerable<uint> deviceIdEnumerable,
      string name
    )
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      var idEnumerable = this.GetIdRange(deviceIdEnumerable);

      ToolStripMenuItem toolStripMenuItem = 
        base.GetNewWithDropDownItems
        (
          idEnumerable,
          name
        );

      return toolStripMenuItem;
    }

    public override void Update()
    {
      if (this.DeviceViewModel == null)
      {
        this.DeviceViewModel = new DeviceViewModel
        <
          DeviceGroupService
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
          >,
          DeviceModel
        >();
      }

      this.DeviceViewModel
        .Update();

      base.Update();
    }

    #endregion
  }
}