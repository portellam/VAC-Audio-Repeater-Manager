#warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services.MMDeviceService;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Parameters

    CoreAudioService<ReadonlyRepository<Device>, Device> CoreAudioService { get; }
    MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> MMDeviceService { get; }

    #endregion

    #region Logic

    #endregion
  }
}