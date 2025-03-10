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

      var enumerable = this.GetRange(idEnumerable);

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

      var enumerable = this.GetRange
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

      var enumerable = this.GetRange(idEnumerable);

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

      var enumerable = this.GetRange
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