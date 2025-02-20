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

    public async IAsyncEnumerable<bool> MuteAllAsync()
    {
      var enumerable = this.SelectedRepository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this.CoreAudioService
          .MuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> MuteRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      var enumerable = this.SelectedService
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        yield return await this.CoreAudioService
          .MuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> MuteRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      var enumerable = this.SelectedService
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        yield return await this.CoreAudioService
          .MuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> UnmuteAllAsync()
    {
      var enumerable = this.SelectedRepository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this.CoreAudioService
          .UnmuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      var enumerable = this.SelectedService
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        yield return await this.CoreAudioService
          .UnmuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      var enumerable = this.SelectedService
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        yield return await this.CoreAudioService
          .UnmuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    public async Task<bool> MuteAsync(uint id)
    {
      TDeviceModel? model = this.SelectedService
        .Get(id);

      return await this.CoreAudioService
        .MuteAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetAsDefaultAsync(uint id)
    {
      var model = this.SelectedService
        .Get(id);

      return await this.CoreAudioService
        .SetAsDefaultAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    public async Task<bool> SetAsDefaultCommunicationsAsync(uint id)
    {
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
      var model = this.SelectedService
        .Get(id);

      return await this.CoreAudioService
        .UnmuteAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    public async Task<bool> UpdateAllAsync()
    {
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
      Device? device = await this
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