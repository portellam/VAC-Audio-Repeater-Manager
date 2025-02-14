using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

/*
 * TODO:
 * - use DoWork and DoWorkAsync methods (this, range, all) to avoid repetitive code?
 * 
 */

namespace VACARM.Application.Services
{
  public partial class DeviceService<TRepository, TDeviceModel> :
    BaseService<DeviceRepository<TDeviceModel>, TDeviceModel>,
    IDeviceService<DeviceRepository<TDeviceModel>, TDeviceModel> where TRepository :
    DeviceRepository<TDeviceModel> where TDeviceModel :
    DeviceModel
  {
    #region Logic

    /// <summary>
    /// Update the service.
    /// </summary>
    /// <returns>True/false result.</returns>
    public async Task<bool> UpdateServiceAsync()
    {
      if (this.MMDeviceService == null)
      {
        this.MMDeviceService =
          new MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice>();
      }

      else
      {
        this
          .MMDeviceService
          .UpdateAll();
      }

      if (this.CoreAudioService == null)
      {
        this.CoreAudioService =
          new CoreAudioService<CoreAudioRepository<Device>, Device>();

        return true;
      }

      return await this
        .CoreAudioService
        .UpdateAllAsync()
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Get the default communications <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
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

      string actualId = device
        .Id
        .ToString();

      return this
        ._Repository
        .GetByActualId(actualId);
    }

    /// <summary>
    /// Get the default console <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    public async Task<TDeviceModel?> GetDefaultConsole
    (
      bool isInput,
      bool isOutput
    )
    {
      Device? device = await this
        .CoreAudioService
        .GetDefaultConsoleAsync
        (
          isInput,
          isOutput
        ).ConfigureAwait(false);

      if (device == null)
      {
        return null;
      }

      string actualId = device
        .Id
        .ToString();

      return this
        ._Repository
        .GetByActualId(actualId);
    }

    /// <summary>
    /// Get the default multimedia <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    public async Task<TDeviceModel?> GetDefaultMultimedia
    (
      bool isInput,
      bool isOutput
    )
    {
      Device? device = await this
        .CoreAudioService
        .GetDefaultMultimediaAsync
        (
          isInput,
          isOutput
        ).ConfigureAwait(false);

      if (device == null)
      {
        return null;
      }

      string actualId = device
        .Id
        .ToString();

      return this
        ._Repository
        .GetByActualId(actualId);
    }

    /// <summary>
    /// Mute a <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    public async Task<bool> MuteAsync(uint id)
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      return await this
        .CoreAudioService
        .MuteAsync(model.ActualId)
        .ConfigureAwait(false);
    }


    /// <summary>
    /// Mute an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> MuteAllAsync()
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this
          .CoreAudioService
          .MuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> MuteRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        yield return await this
          .CoreAudioService
          .MuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result</returns>
    public async IAsyncEnumerable<bool> MuteRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        yield return await this
          .CoreAudioService
          .MuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Set the <typeparamref name="TDeviceModel"/> item as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    public async Task<bool> SetAsDefaultAsync(uint id)
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      return await this
        .CoreAudioService
        .SetAsDefaultAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Set the <typeparamref name="TDeviceModel"/> item as default for
    /// communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    public async Task<bool> SetAsDefaultCommunicationsAsync(uint id)
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      return await this
        .CoreAudioService
        .SetAsDefaultCommunicationsAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Set the <typeparamref name="TDeviceModel"/> item volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    public async Task<bool> SetVolumeAsync
    (
      uint id,
      double? volume
    )
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      return await this
        .CoreAudioService
        .SetVolumeAsync
        (
          model.ActualId,
          volume
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Start a <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    public async Task<bool> Start(uint id)
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      return await this
        .MMDeviceService
        .StartAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    public void StartAll()
    {
      this
        .MMDeviceService
        .StartAll();
    }

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> StartAllAsync()
    {
      IEnumerable<TDeviceModel> enumerable = this
         ._Repository
         .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this
          .MMDeviceService
          .StartAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> StartRange
    (
      uint startId,
      uint endId
    )
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        yield return await this
          .MMDeviceService
          .StartAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> StartRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        yield return await this
          .MMDeviceService
          .StartAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Stop a <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    public async Task<bool> StopAsync(uint id)
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      return await this
        .MMDeviceService
        .StopAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    public void StopAll()
    {
      this
        .MMDeviceService
        .StopAll();
    }

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> StopAllAsync()
    {
      IEnumerable<TDeviceModel> enumerable = this
         ._Repository
         .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this
          .MMDeviceService
          .StopAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> StopRange
    (
      uint startId,
      uint endId
    )
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        yield return await this
          .MMDeviceService
          .StopAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> StopRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        yield return await this
          .MMDeviceService
          .StopAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Unmute a <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    public async Task<bool> UnmuteAsync(uint id)
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      return await this
        .CoreAudioService
        .UnmuteAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Unmute an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> UnmuteAllAsync()
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetAll();

      foreach (var item in enumerable)
      {
        yield return await this
          .CoreAudioService
          .UnmuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Unmute an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        yield return await this
          .CoreAudioService
          .UnmuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Unmute an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result</returns>
    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        yield return await this
          .CoreAudioService
          .UnmuteAsync(item.ActualId)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Update a <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    public void Update(uint id)
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      this
         .MMDeviceService
         .Update(model.ActualId);
    }

    /// <summary>
    /// Update an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    public void UpdateAll()
    {
      this
        .MMDeviceService
        .UpdateAll();
    }

    /// <summary>
    /// Update an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async Task<bool> UpdateAllAsync()
    {
      return await this
        .CoreAudioService
        .UpdateAllAsync()
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    public void UpdateRange
    (
      uint startId,
      uint endId
    )
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        this
          .MMDeviceService
          .Update(item.ActualId);
      }
    }

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    public void UpdateRange(IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        this
          .MMDeviceService
          .Update(item.ActualId);
      }
    }

    #endregion
  }
}