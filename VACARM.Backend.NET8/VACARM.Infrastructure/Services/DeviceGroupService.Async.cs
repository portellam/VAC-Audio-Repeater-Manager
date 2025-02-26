using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
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

    public async IAsyncEnumerable<bool> MuteAllAsync()
    {
      if (this.CoreAudioService == null)
      {
        yield return false;
      }

      var enumerable = this.SelectedRepository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this.MuteAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> MuteRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      if (this.CoreAudioService == null)
      {
        yield return false;
      }

      var enumerable = this.SelectedService
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        yield return await this.MuteAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> MuteRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      if (this.CoreAudioService == null)
      {
        yield return false;
      }

      var enumerable = this.SelectedService
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        yield return await this.MuteAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> UnmuteAllAsync()
    {
      if (this.CoreAudioService == null)
      {
        yield return false;
      }

      var enumerable = this.SelectedRepository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this.UnmuteAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      if (this.CoreAudioService == null)
      {
        yield return false;
      }

      var enumerable = this.SelectedService
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        yield return await this.UnmuteAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      if (this.CoreAudioService == null)
      {
        yield return false;
      }

      var enumerable = this.SelectedService
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        yield return await this.UnmuteAsync(item)
          .ConfigureAwait(false);
      }
    }

    public async Task<bool> MuteAsync(uint id)
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      var model = this.SelectedService
        .Get(id);

      return await this.MuteAsync(model)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetAsDefaultAsync(uint id)
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      var model = this.SelectedService
        .Get(id);

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

      var model = this.SelectedService
        .Get(id);

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

      var model = this.SelectedService
        .Get(id);

      return await this.CoreAudioService
        .SetVolumeAsync
        (
          model.ActualId,
          volume
        ).ConfigureAwait(false);
    }

    public async Task<bool> UnmuteAsync(uint id)
    {
      if (this.CoreAudioService == null)
      {
        return false;
      }

      var model = this.SelectedService
        .Get(id);

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

    public async Task<bool> UpdateServiceAsync()
    {
      if (this.MMDeviceService == null)
      {
        this.MMDeviceService =
          new MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice>();
      }

      else
      {
        this.MMDeviceService
          .UpdateAll();
      }

      if (this.CoreAudioService == null)
      {
        this.CoreAudioService =
          new CoreAudioService<ReadonlyRepository<Device>, Device>();

        return true;
      }

      return await this.CoreAudioService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    public async Task<TDeviceModel?> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return null;
      }

      var device = await this
         .CoreAudioService
         .GetDefaultCommunicationsAsync
         (
           isInput,
           isOutput
         ).ConfigureAwait(false);

      if (device == null)
      {
        return null;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    public async Task<TDeviceModel?> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return null;
      }

      var device = await this.CoreAudioService
        .GetDefaultConsoleAsync
        (
          isInput,
          isOutput
        ).ConfigureAwait(false);

      if (device == null)
      {
        return null;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    public async Task<TDeviceModel?> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return null;
      }

      var device = await this.CoreAudioService
        .GetDefaultMultimediaAsync
        (
          isInput,
          isOutput
        ).ConfigureAwait(false);

      if (device == null)
      {
        return null;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    #endregion
  }
}