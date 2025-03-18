#warning Differs from projects of earlier NET revisions (below Framework 4.6).

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

    public async Task<bool> UpdateServiceAsync()
    {
      if (this.MMDeviceService == null)
      {
        this.MMDeviceService = new MMDeviceService<MMDevice>();
      }

      else
      {
        this.MMDeviceService
          .UpdateAll();
      }

      if (this.CoreAudioService == null)
      {
        this.CoreAudioService = new CoreAudioService<Device>();

        return true;
      }

      return await this.CoreAudioService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    public async Task<TDeviceModel> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return default;
      }

      var device = await this
         .CoreAudioService
         .GetDefaultCommunicationsAsync
         (
           isInput,
           isOutput
         )
         .ConfigureAwait(false);

      if (device == null)
      {
        return default;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    public async Task<TDeviceModel> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return default;
      }

      var device = await this.CoreAudioService
        .GetDefaultConsoleAsync
        (
          isInput,
          isOutput
        )
        .ConfigureAwait(false);

      if (device == null)
      {
        return default;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    public async Task<TDeviceModel> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return default;
      }

      var device = await this.CoreAudioService
        .GetDefaultMultimediaAsync
        (
          isInput,
          isOutput
        )
        .ConfigureAwait(false);

      if (device == null)
      {
        return default;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    #endregion
  }
}