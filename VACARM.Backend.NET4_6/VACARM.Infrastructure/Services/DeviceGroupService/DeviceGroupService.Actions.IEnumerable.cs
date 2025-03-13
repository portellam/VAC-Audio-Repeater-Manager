#warning Differs from project of later NET revisions (above Framework 4.6).

using AudioSwitcher.AudioApi;
using MoreLinq;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<IEnumerable<bool>> MuteAll()
    {
      var enumerable = Array.Empty<bool>();

      if (this.CoreAudioService == null)
      {
        return enumerable;
      }

      var modelEnumerable = this.SelectedRepository
        .GetAll();

      foreach (var item in modelEnumerable)
      {
        var result = await this.MuteAsync(item)
          .ConfigureAwait(false);

        enumerable.Append(result);
      }

      return enumerable;
    }

    public async Task<IEnumerable<bool>> MuteRange
    (IEnumerable<uint> idEnumerable)
    {
      var enumerable = Array.Empty<bool>();

      if (this.CoreAudioService == null)
      {
        return enumerable;
      }

      var modelEnumerable = this.GetRange(idEnumerable);

      foreach (var item in modelEnumerable)
      {
        var result = await this.MuteAsync(item)
          .ConfigureAwait(false);

        enumerable.Append(result);
      }

      return enumerable;
    }

    public async Task<IEnumerable<bool>> MuteRange
    (
      uint startId,
      uint endId
    )
    {
      var enumerable = Array.Empty<bool>();

      if (this.CoreAudioService == null)
      {
        return enumerable;
      }

      var modelEnumerable = this.GetRange
        (
          startId,
          endId
        );

      foreach (var item in modelEnumerable)
      {
        var result = await this.MuteAsync(item)
          .ConfigureAwait(false);

        enumerable.Append(result);
      }

      return enumerable;
    }

    public async Task<IEnumerable<bool>> UnmuteAll()
    {
      var enumerable = Array.Empty<bool>();

      if (this.CoreAudioService == null)
      {
        return enumerable;
      }

      var modelEnumerable = this.SelectedRepository
        .GetAll();

      foreach (var item in modelEnumerable)
      {
        var result = await this.UnmuteAsync(item)
          .ConfigureAwait(false);

        enumerable.Append(result);
      }

      return enumerable;
    }

    public async Task<IEnumerable<bool>> UnmuteRange
    (IEnumerable<uint> idEnumerable)
    {
      var enumerable = Array.Empty<bool>();

      if (this.CoreAudioService == null)
      {
        return enumerable;
      }

      var modelEnumerable = this.GetRange(idEnumerable);

      foreach (var item in modelEnumerable)
      {
        var result = await this.UnmuteAsync(item)
          .ConfigureAwait(false);

        enumerable.Append(result);
      }

      return enumerable;
    }

    public async Task<IEnumerable<bool>> UnmuteRange
    (
      uint startId,
      uint endId
    )
    {
      var enumerable = Array.Empty<bool>();

      if (this.CoreAudioService == null)
      {
        return enumerable;
      }

      var modelEnumerable = this.GetRange
        (
          startId,
          endId
        );

      foreach (var item in modelEnumerable)
      {
        var result = await this.UnmuteAsync(item)
          .ConfigureAwait(false);

        enumerable.Append(result);
      }

      return enumerable;
    }

    #endregion
  }
}