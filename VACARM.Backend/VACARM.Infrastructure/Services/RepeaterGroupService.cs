using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service to manage multiple configurations of audio repeaters. 
  /// Configurations are user-defined, 
  /// and may be from a foreign system or a previous state of the current system.
  /// Manages <typeparamref name="DeviceGroupService"/>.
  /// </summary>
  public partial class RepeaterGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TRepeaterModel
    > :
    BaseGroupService
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
    >,
    IRepeaterGroupService
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
    where TGroupReadonlyRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TRepeaterModel>,
      TRepeaterModel
    >
    where TBaseRepository :
    BaseRepository<TRepeaterModel>
    where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    private bool preferLegacyExecutable { get; set; } = false;

    private DeviceGroupService
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
    > deviceGroupService
    { get; set; }

    private string customExecutablePathName { get; set; } =
      Common.Info.ExpectedExecutablePathName;

    private string ExecutableName
    {
      get
      {
        if (this.PreferLegacyExecutable)
        {
          return Common.Info.MMEExecutableName;
        }

        return Common.Info.KSExecutableName;
      }
    }

    public bool PreferLegacyExecutable
    {
      get
      {
        return this.preferLegacyExecutable;
      }
      set
      {
        this.preferLegacyExecutable = value;
        this.OnPropertyChanged(nameof(PreferLegacyExecutable));
      }
    }

    public DeviceGroupService
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
    {
      get
      {
        return this.deviceGroupService;
      }
      private set
      {
        this.deviceGroupService = value;
        this.OnPropertyChanged(nameof(DeviceGroupService));
      }
    }

    public string CustomExecutablePathName
    {
      get
      {
        return this.customExecutablePathName;
      }
      set
      {
        if
        (
          string.IsNullOrEmpty(value)
          || string.IsNullOrWhiteSpace(value)
        )
        {
          value = Common.Info.ExpectedExecutablePathName;
        }

        this.customExecutablePathName = value;
        this.OnPropertyChanged(nameof(CustomExecutablePathName));
      }
    }

    public string ExecutableFullPathName
    {
      get
      {
        return CustomExecutablePathName + ExecutableName;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public RepeaterGroupService() :
      base()
    {
      this.List =
        new List<BaseService<BaseRepository<TRepeaterModel>, TRepeaterModel>>();

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
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of service(s)</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    /// <param name="deviceGroupService">The device group service</param>
    public RepeaterGroupService
    (
      List<BaseService<BaseRepository<TRepeaterModel>, TRepeaterModel>> list,
      int maxCount,
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
        > deviceGroupService
    ) :
      base
      (
        list,
        maxCount
      )
    {
      this.List = list;
      this.MaxCount = maxCount;
      this.DeviceGroupService = deviceGroupService;
    }

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.Dispose();

        this.DeviceGroupService
          .Dispose();
      }

      this.HasDisposed = true;
    }

    public IEnumerable<TRepeaterModel> GetAllAlphabetical()
    {
      return this.SelectedRepository
        .GetAll()
        .OrderBy(x => x.WindowName);
    }

    public IEnumerable<TRepeaterModel> GetAllByDeviceId(uint deviceId)
    {
      var func = RepeaterFunctions<TRepeaterModel>.ContainsDeviceId(deviceId);

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllByDeviceName(string deviceName)
    {
      var func = RepeaterFunctions<TRepeaterModel>.ContainsDeviceName(deviceName);

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllStarted()
    {
      var func = RepeaterFunctions<TRepeaterModel>.IsStarted;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllStopped()
    {
      var func = RepeaterFunctions<TRepeaterModel>.IsStopped;

      return this.SelectedRepository
        .GetRange(func);
    }

    #endregion
  }
}