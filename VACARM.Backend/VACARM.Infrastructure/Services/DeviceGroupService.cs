#warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service to manage multiple configurations of system audio device(s). 
  /// Configurations may be from a foreign system or a previous state of the 
  /// current system.
  /// Manages <typeparamref name="DeviceService"/>,
  ///  <typeparamref name="CoreAudioService"/>,
  ///  and <typeparamref name="MMDeviceService"/>.
  /// </summary>
  public partial class DeviceGroupService
    <
      TServiceRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    > :
    BaseGroupService
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
    where TServiceRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TBaseRepository :
    BaseRepository<TDeviceModel>
    where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    private CoreAudioService<ReadonlyRepository<Device>, Device> coreAudioService
    { get; set; }

    private MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> mMDeviceService
    { get; set; }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public CoreAudioService<ReadonlyRepository<Device>, Device> CoreAudioService
    {
      get
      {
        return this.coreAudioService;
      }
      private set
      {
        this.coreAudioService = value;
        this.OnPropertyChanged(nameof(CoreAudioService));
      }
    }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> MMDeviceService
    {
      get
      {
        return this.mMDeviceService;
      }
      private set
      {
        this.mMDeviceService = value;
        this.OnPropertyChanged(nameof(MMDeviceService));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceGroupService() :
      base()
    {
      this.BaseServiceList =
        new List<BaseService<BaseRepository<TDeviceModel>, TDeviceModel>>();

      this.MMDeviceService =
        new MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice>();

      this.CoreAudioService =
        new CoreAudioService<ReadonlyRepository<Device>, Device>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="baseServiceList">The list of service(s)</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    /// <param name="mMDeviceService">The MMDevice service</param>
    /// <param name="coreAudioService">The Core Audio service</param>
    public DeviceGroupService
    (
      List<BaseService<BaseRepository<TDeviceModel>, TDeviceModel>> baseServiceList,
      int maxCount,
      MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> mMDeviceService,
      CoreAudioService<ReadonlyRepository<Device>, Device> coreAudioService
    ) :
      base
      (
        baseServiceList,
        maxCount
      )
    {
      this.BaseServiceList = baseServiceList;
      this.MMDeviceService = mMDeviceService;
      this.CoreAudioService = coreAudioService;
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

        this.CoreAudioService
          .Dispose();

        this.MMDeviceService
          .Dispose();
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}