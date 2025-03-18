using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class DeviceGroupService<TDeviceModel>
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

    public async Task<bool> MuteAsync(uint id)
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      var model = this.Get(id);

      return await this.MuteAsync(model)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetAsDefaultAsync(uint id)
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      var model = this.Get(id);

      return await this.CoreAudioService
        .SetAsDefaultAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetAsDefaultCommunicationsAsync(uint id)
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      var model = this.Get(id);

      return await this.CoreAudioService
        .SetAsDefaultCommunicationsAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetVolumeAsync
    (
      uint id,
      double? volume
    )
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      var model = this.Get(id);

      return await this.CoreAudioService
        .SetVolumeAsync
        (
          model.ActualId,
          volume
        )
        .ConfigureAwait(false);
    }

    public async Task<bool> UnmuteAsync(uint id)
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      var model = this.Get(id);

      return await this.UnmuteAsync(model)
        .ConfigureAwait(false);
    }

    public async Task<bool> UpdateAllAsync()
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      return await this.CoreAudioService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    #endregion
  }
}