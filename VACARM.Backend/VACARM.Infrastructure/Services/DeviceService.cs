#warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

using NAudio.CoreAudioApi;
using AudioSwitcher.AudioApi;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="DeviceRepository"/>.
  /// </summary>
  public class DeviceService<TRepository, TDeviceModel> :
    BaseService<DeviceRepository<TDeviceModel>, TDeviceModel>,
    IDeviceService<DeviceRepository<TDeviceModel>, TDeviceModel> where TRepository :
    DeviceRepository<TDeviceModel> where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    private MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice>
      mMDeviceService
    { get; set; }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice> MMDeviceService
    {
      get
      {
        return this.mMDeviceService;
      }
      private set
      {
        this.mMDeviceService = value;
        base.OnPropertyChanged(nameof(MMDeviceService));
      }
    }

    private CoreAudioService<CoreAudioRepository<Device>, Device>
      coreAudioService
    { get; set; }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public CoreAudioService<CoreAudioRepository<Device>, Device> CoreAudioService
    {
      get
      {
        return this.coreAudioService;
      }
      private set
      {
        this.coreAudioService = value;
        base.OnPropertyChanged(nameof(CoreAudioService));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceService()
    {
      base._Repository = new DeviceRepository<TDeviceModel>();

      this.MMDeviceService =
        new MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice>();

      this.CoreAudioService =
        new CoreAudioService<CoreAudioRepository<Device>, Device>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <param name="mMDeviceService">The MMDevice service</param>
    /// <param name="coreAudioService">The Core Audio service</param>
    public DeviceService
    (
      DeviceRepository<TDeviceModel> repository,
      MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice> mMDeviceService,
      CoreAudioService<CoreAudioRepository<Device>, Device> coreAudioService
    )
    {
      base._Repository = new DeviceRepository<TDeviceModel>();
      this.MMDeviceService = mMDeviceService;
      this.CoreAudioService = coreAudioService;
    }

    #endregion
  }
}