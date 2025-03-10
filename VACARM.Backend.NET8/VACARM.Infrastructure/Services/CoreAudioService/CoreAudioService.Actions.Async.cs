using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class CoreAudioService
    <
      TRepository,
      TDevice
    >
  {
    #region Logic


    public async Task<bool> IsDefaultAsync(string id)
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      if (item == null)
      {
        return false;
      }

      return item.IsDefaultDevice;
    }

    public async Task<bool> IsDefaultCommunicationsAsync(string id)
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      if (item == null)
      {
        return false;
      }

      return item.IsDefaultCommunicationsDevice;
    }

    public async Task<bool> IsMutedAsync(string id)
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      if (item == null)
      {
        return false;
      }

      return item.IsMuted;
    }

    public async Task<bool> MuteAsync(string id)
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      return await CoreAudioCommands.DoMuteAsync(item)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetAsDefaultAsync(string id)
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      return await CoreAudioCommands.SetAsDefaultAsync(item)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetAsDefaultCommunicationsAsync(string id)
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      return await CoreAudioCommands.SetAsDefaultCommunicationsAsync(item)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetVolumeAsync
    (
      string id,
      double? volume
    )
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      return CoreAudioCommands.SetVolume
        (
          item,
          volume
        );
    }

    public async Task<bool> UnmuteAsync(string id)
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      return await CoreAudioCommands.DoUnmuteAsync(item)
        .ConfigureAwait(false);
    }

    #endregion
  }
}