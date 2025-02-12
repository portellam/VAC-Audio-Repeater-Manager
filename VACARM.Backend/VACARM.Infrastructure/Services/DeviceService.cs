using NAudio.CoreAudioApi;
#warning AudioSwitcher.AudioApi must be used after NAudio.CoreAudioApi
using AudioSwitcher.AudioApi;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

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

    #warning AudioSwitcher.AudioApi must declare after NAudio.CoreAudioApi

    private CoreAudioService<CoreAudioRepository<Device>, Device>
      coreAudioService { get; set; }
    
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
    public DeviceService()
    {
      base._Repository = new DeviceRepository<TDeviceModel>();

      this.MMDeviceService =
        new MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice>();

      #warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

      this.CoreAudioService = 
        new CoreAudioService<CoreAudioRepository<Device>, Device>();
    }

    #endregion
  }
}