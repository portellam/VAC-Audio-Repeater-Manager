using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class DeviceGroupService
    <
      TBaseService,
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

    #endregion
  }
}