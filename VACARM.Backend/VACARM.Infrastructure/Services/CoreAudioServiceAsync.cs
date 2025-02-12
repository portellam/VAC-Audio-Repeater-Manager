using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class CoreAudioService<TRepository, TDevice> :
    GenericService<CoreAudioRepository<TDevice>, TDevice>,
    ICoreAudioService<CoreAudioRepository<TDevice>, TDevice> where TRepository :
    CoreAudioRepository<TDevice> where TDevice :
    Device
  {
    #region Logic

    /// <summary>
    /// Get the default <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="role">The role</param>
    /// <param name="deviceType">The device type</param>
    /// <returns>The item.</returns>
    private async Task<CoreAudioDevice?> GetDefaultAsync
    (
      Role role,
      DeviceType deviceType
    )
    {
      return await Controller
        .GetDefaultDeviceAsync
        (
          deviceType,
          role
        )
        .ConfigureAwait(false);
    }

    public async Task<bool> DoMuteAsync(string id)
    {
      CoreAudioDevice? model = await this.GetAsync(id);
      return await CoreAudioCommands.DoMute(model);
    }

    public async Task<bool> DoUnmuteAsync(string id)
    {
      CoreAudioDevice? model = await this.GetAsync(id);
      return await CoreAudioCommands.DoUnmute(model);
    }

    public async Task<bool> IsDefaultAsync(string id)
    {
      CoreAudioDevice? model = await this.GetAsync(id);

      if (model == null)
      {
        return false;
      }

      return model.IsDefaultDevice;
    }

    public async Task<bool> IsDefaultCommunicationsAsync(string id)
    {
      CoreAudioDevice? model = await this.GetAsync(id);

      if (model == null)
      {
        return false;
      }

      return model.IsDefaultCommunicationsDevice;
    }

    public async Task<bool> IsMutedAsync(string id)
    {
      CoreAudioDevice? model = await this.GetAsync(id);

      if (model == null)
      {
        return false;
      }

      return model.IsMuted;
    }

    public async Task<bool> SetAsDefaultAsync(string id)
    {
      CoreAudioDevice? model = await this.GetAsync(id);
      return await CoreAudioCommands.SetAsDefault(model);
    }

    public async Task<bool> SetAsDefaultCommunicationsAsync(string id)
    {
      CoreAudioDevice? model = await this.GetAsync(id);
      return await CoreAudioCommands.SetAsDefaultCommunications(model);
    }

    public async Task<bool> SetVolumeAsync
    (
      string id,
      double? volume
    )
    {
      CoreAudioDevice? model = await this.GetAsync(id);

      return CoreAudioCommands.SetVolume
        (
          model,
          volume
        );
    }

    public async Task<CoreAudioDevice?> GetAsync(string id)
    {
      if (Controller == null)
      {
        return null;
      }

      Guid guid = ToGuid(id);

      return await Controller
        .GetDeviceAsync(guid)
        .ConfigureAwait(false);
    }

    public async Task<CoreAudioDevice?> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      DeviceType deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await GetDefaultAsync
        (
          Role.Communications,
          deviceType
        );
    }

    public async Task<CoreAudioDevice?> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      DeviceType deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await GetDefaultAsync
        (
          Role.Console,
          deviceType
        );
    }

    public async Task<CoreAudioDevice?> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      DeviceType deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await GetDefaultAsync
        (
          Role.Multimedia,
          deviceType
        );
    }

    public async Task<bool> UpdateAllAsync()
    {
      var enumerable = await Controller
        .GetDevicesAsync()
        .ConfigureAwait(false);

      if (enumerable == null)
      {
        return false;
      }

      base._Repository.RemoveAll();
      base._Repository.AddRange(enumerable); //FIXME
      return false;
    }

    public async Task<double> GetVolumeAsync(string id)
    {
      CoreAudioDevice? model = await this.GetAsync(id);

      if (model == null)
      {
        return 0;
      }

      return model.Volume;
    }

    #endregion
  }
}