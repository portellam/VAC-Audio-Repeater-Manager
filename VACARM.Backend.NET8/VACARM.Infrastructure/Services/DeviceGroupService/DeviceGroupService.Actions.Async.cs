using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class DeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    >
  {
    #region Logic
    private async Task<bool> MuteAsync(TDeviceModel model)
    {
      if (model == null)
      {
        return false;
      }

      return await this.CoreAudioService
        .MuteAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    private async Task<bool> UnmuteAsync(TDeviceModel model)
    {
      if (model == null)
      {
        return false;
      }

      return await this.CoreAudioService
        .UnmuteAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    #endregion
  }
}