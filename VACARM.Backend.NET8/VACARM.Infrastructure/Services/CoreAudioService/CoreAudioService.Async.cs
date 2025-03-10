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

    /// <summary>
    /// Get the default <typeparamref name="CoreAudioDevice"/>.
    /// </summary>
    /// <param name="role">The role</param>
    /// <param name="deviceType">The device type</param>
    /// <returns>The item.</returns>
    private async Task<CoreAudioDevice> GetDefaultAsync
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

    public async Task<bool> UpdateServiceAsync()
    {
      var coreAudioDeviceEnumerable = await this.Controller
        .GetDevicesAsync()
        .ConfigureAwait(false);

      if (coreAudioDeviceEnumerable == null)
      {
        return false;
      }

      ObservableCollection<TDevice> collection =
        new ObservableCollection<TDevice>();

      foreach (var item in coreAudioDeviceEnumerable)
      {
        collection.Add(item as TDevice);
      }

      this.Repository = new ReadonlyRepository<TDevice>(collection);

      return true;
    }

    public async Task<CoreAudioDevice> GetAsync(string id)
    {
      if (this.Controller == null)
      {
        return null;
      }

      var guid = ToGuid(id);

      return await this.Controller
        .GetDeviceAsync(guid)
        .ConfigureAwait(false);
    }

    public async Task<CoreAudioDevice> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      var deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await this.GetDefaultAsync
        (
          Role.Communications,
          deviceType
        ).ConfigureAwait(false);
    }

    public async Task<CoreAudioDevice> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      var deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await this.GetDefaultAsync
        (
          Role.Console,
          deviceType
        ).ConfigureAwait(false);
    }

    public async Task<CoreAudioDevice> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      var deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await this.GetDefaultAsync
        (
          Role.Multimedia,
          deviceType
        ).ConfigureAwait(false);
    }

    public async Task<double> GetVolumeAsync(string id)
    {
      var item = await this.GetAsync(id)
        .ConfigureAwait(false);

      if (item == null)
      {
        return 0;
      }

      return item.Volume;
    }

    #endregion
  }
}