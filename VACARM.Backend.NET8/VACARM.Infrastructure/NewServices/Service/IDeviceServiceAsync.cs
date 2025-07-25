using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

    /// <summary>
    /// Mute the enumerable of all <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> MuteAllAsync();

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> MuteRangeAsync
    (IEnumerable<int> idEnumerable);

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> MuteRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Unmute the enumerable of all <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> UnmuteAllAsync();

    /// <summary>
    /// Unmute an enumerable of some <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> UnmuteRangeAsync
    (IEnumerable<int> idEnumerable);

    /// <summary>
    /// Unmute an enumerable of some <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> UnmuteRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Mute a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> MuteAsync(int id);

    /// <summary>
    /// Set the <typeparamref name="DeviceModel"/> as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultAsync(int id);

    /// <summary>
    /// Set the <typeparamref name="DeviceModel"/> as default for
    /// communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultCommunicationsAsync(int id);

    /// <summary>
    /// Set the <typeparamref name="DeviceModel"/> volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetVolumeAsync
    (
      int id,
      double? volume
    );

    /// <summary>
    /// Unmute a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> UnmuteAsync(int id);

    /// <summary>
    /// Update the service.
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<bool> UpdateServiceAsync();

    /// <summary>
    /// Get the default communications <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<DeviceModel?> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default console <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<DeviceModel?> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default multimedia <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<DeviceModel?> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    );

    #endregion
  }
}