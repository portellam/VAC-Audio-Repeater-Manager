using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class DeviceService<TRepository, TDeviceModel>
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
          new MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice>();
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
          new CoreAudioService<ReadonlyRepository<Device>, Device>();

        return true;
      }

      return await this
        .CoreAudioService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Get the default communications <typeparamref name="TDeviceModel"/>.
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
        .Repository
        .GetByActualId(actualId);
    }

    /// <summary>
    /// Get the default console <typeparamref name="TDeviceModel"/>.
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
        .Repository
        .GetByActualId(actualId);
    }

    /// <summary>
    /// Get the default multimedia <typeparamref name="TDeviceModel"/>.
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
        .Repository
        .GetByActualId(actualId);
    }

    /// <summary>
    /// Mute a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    public async Task<bool> MuteAsync(uint id)
    {
      TDeviceModel? model = this
        .Repository
        .Get(id);

      return await this
        .CoreAudioService
        .MuteAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Mute an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> MuteAllAsync()
    {
      IEnumerable<TDeviceModel> enumerable = this
        .Repository
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
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> MuteRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        .Repository
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
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
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
        .Repository
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
    /// Set the <typeparamref name="TDeviceModel"/> as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    public async Task<bool> SetAsDefaultAsync(uint id)
    {
      TDeviceModel? model = this
        .Repository
        .Get(id);

      return await this
        .CoreAudioService
        .SetAsDefaultAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Set the <typeparamref name="TDeviceModel"/> as default for
    /// communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    public async Task<bool> SetAsDefaultCommunicationsAsync(uint id)
    {
      TDeviceModel? model = this
        .Repository
        .Get(id);

      return await this
        .CoreAudioService
        .SetAsDefaultCommunicationsAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Set the <typeparamref name="TDeviceModel"/> volume.
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
        .Repository
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
    /// Start an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> StartAllAsync()
    {
      IEnumerable<TDeviceModel> enumerable = this
         .Repository
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
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/>(s).
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
        .Repository
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
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> StartRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        .Repository
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
    /// Stop a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    public void Stop(uint id)
    {
      TDeviceModel? model = this
        .Repository
        .Get(id);

      this.MMDeviceService
        .Stop(model.ActualId);
    }

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    public void StopAll()
    {
      IEnumerable<TDeviceModel> enumerable = this
         .Repository
         .GetAll();

      foreach (var item in enumerable)
      {
        this.MMDeviceService
          .Stop(item.ActualId);
      }
    }



    /// <summary>
    /// Unmute a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    public async Task<bool> UnmuteAsync(uint id)
    {
      TDeviceModel? model = this
        .Repository
        .Get(id);

      return await this
        .CoreAudioService
        .UnmuteAsync(model.ActualId)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Unmute an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> UnmuteAllAsync()
    {
      IEnumerable<TDeviceModel> enumerable = this
        .Repository
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
    /// Unmute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        .Repository
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
    /// Unmute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
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
        .Repository
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
    /// Update an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    public async Task<bool> UpdateAllAsync()
    {
      return await this
        .CoreAudioService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    #endregion
  }
}