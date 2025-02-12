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
      return await this
        .Controller
        .GetDefaultDeviceAsync
        (
          deviceType,
          role
        )
        .ConfigureAwait(false);
    }

    public async Task<bool> DoMuteAsync(string id)
    {
      CoreAudioDevice? item = await this.GetAsync(id);
      return await CoreAudioCommands.DoMute(item);
    }

    public async Task<bool> DoUnmuteAsync(string id)
    {
      CoreAudioDevice? item = await this.GetAsync(id);
      return await CoreAudioCommands.DoUnmute(item);
    }

    public async Task<bool> IsDefaultAsync(string id)
    {
      CoreAudioDevice? item = await this.GetAsync(id);

      if (item == null)
      {
        return false;
      }

      return item.IsDefaultDevice;
    }

    public async Task<bool> IsDefaultCommunicationsAsync(string id)
    {
      CoreAudioDevice? item = await this.GetAsync(id);

      if (item == null)
      {
        return false;
      }

      return item.IsDefaultCommunicationsDevice;
    }

    public async Task<bool> IsMutedAsync(string id)
    {
      CoreAudioDevice? item = await this.GetAsync(id);

      if (item == null)
      {
        return false;
      }

      return item.IsMuted;
    }

    public async Task<bool> SetAsDefaultAsync(string id)
    {
      CoreAudioDevice? item = await this.GetAsync(id);
      return await CoreAudioCommands.SetAsDefault(item);
    }

    public async Task<bool> SetAsDefaultCommunicationsAsync(string id)
    {
      CoreAudioDevice? item = await this.GetAsync(id);
      return await CoreAudioCommands.SetAsDefaultCommunications(item);
    }

    public async Task<bool> SetVolumeAsync
    (
      string id,
      double? volume
    )
    {
      CoreAudioDevice? item = await this.GetAsync(id);

      return CoreAudioCommands.SetVolume
        (
          item,
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

      return await this
        .Controller
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

      return await this.GetDefaultAsync
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

      return await this.GetDefaultAsync
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

      return await this.GetDefaultAsync
        (
          Role.Multimedia,
          deviceType
        );
    }

    public async Task<bool> UpdateAllAsync()
    {
      var enumerable = await this
        .Controller
        .GetDevicesAsync()
        .ConfigureAwait(false);

      if (enumerable == null)
      {
        return false;
      }

      base._Repository = new CoreAudioRepository<TDevice>
        ((IEnumerable<TDevice>)enumerable);
      
      return false;
    }

    public async Task<double> GetVolumeAsync(string id)
    {
      CoreAudioDevice? item = await this.GetAsync(id);

      if (item == null)
      {
        return 0;
      }

      return item.Volume;
    }

    #endregion
  }
}