using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public interface IDeviceController<T1, T2> :
    IBaseController<T1, T2> where T1 :
    IDeviceRepository<T2> where T2 :
    DeviceModel
  {
    #region Logic

    /// <summary>
    /// Get the default communications <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The audio device.</returns>
    Task<DeviceModel?> GetDefaultCommunications
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default console <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The audio device.</returns>
    Task<DeviceModel?> GetDefaultConsole
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default multimedia <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The audio device.</returns>
    Task<DeviceModel?> GetDefaultMultimedia
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Mute a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> Mute(uint id);

    /// <summary>
    /// Mute all <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The true/false result.</returns>
    Task<bool> MuteAll();

    /// <summary>
    /// Mute some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> MuteRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Restart a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> Restart(uint id);

    /// <summary>
    /// Restart all <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The true/false result.</returns>
    Task<bool> RestartAll();

    /// <summary>
    /// Restart some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> RestartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Restart some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The true/false result.</returns>
    Task<bool> RestartRange(List<uint> idList);

    /// <summary>
    /// Set a <typeparamref name="DeviceModel"/> as the default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetAsDefault(uint id);

    /// <summary>
    /// Set a <typeparamref name="DeviceModel"/> as the default communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetAsDefaultCommunications(uint id);

    /// <summary>
    /// Set the audio device volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    /// <returns>The true/false result.</returns>
        Task<bool> SetVolume
    (
      string id,
      double? volume
    );

    /// <summary>
    /// Restart a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> Start(uint id);

    /// <summary>
    /// Start all <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The true/false result.</returns>
    Task<bool> StartAll();

    /// <summary>
    /// Start some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> StartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Start some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    Task<bool> StartRange(List<uint> idList);

    /// <summary>
    /// Stop a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> Stop(uint id);

    /// <summary>
    /// Stop all <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The true/false result.</returns>
    Task<bool> StopAll();

    /// <summary>
    /// Stop some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> StopRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Stop some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The true/false result.</returns>
    Task<bool> StopRange(List<uint> idList);

    /// <summary>
    /// Unmute a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> Unmute(uint id);

    /// <summary>
    /// Unmute all <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The true/false result.</returns>
    Task<bool> UnmuteAll();

    /// <summary>
    /// Unmute some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> UnmuteRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Update a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> Update(uint id);

    /// <summary>
    /// Update all <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The true/false result.</returns>
    Task<bool> UpdateAll();

    /// <summary>
    /// Update some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> UpdateRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Update some <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The true/false result.</returns>
    Task<bool> UpdateRange(List<uint> idList);

    #endregion
  }
}