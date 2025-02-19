namespace VACARM.Application.Services
{
  public partial interface IDeviceServiceRepositoryService
    <
      TService,
      TRepository,
      TDeviceModel
    >
  {
    #region Logic

    /// <summary>
    /// Get the default communications <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The audio device.</returns>
    Task<TDeviceModel?> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default console <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The audio device.</returns>
    Task<TDeviceModel?> GetDefaultConsole
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default multimedia <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The audio device.</returns>
    Task<TDeviceModel?> GetDefaultMultimedia
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Mute a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> Mute(uint id);

    /// <summary>
    /// Mute an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> MuteAll();

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> MuteRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Restart a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> Restart(uint id);

    /// <summary>
    /// Restart an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> RestartAll();

    /// <summary>
    /// Restart an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> RestartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Restart some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> RestartRange(List<uint> idList);

    /// <summary>
    /// Set a <typeparamref name="TDeviceModel"/> as the default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultAsync(uint id);

    /// <summary>
    /// Set a <typeparamref name="TDeviceModel"/> as the default communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultCommunicationsAsync(uint id);

    /// <summary>
    /// Set the audio device volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>True/false result.</returns>
    /// <returns>True/false result.</returns>
    Task<bool> SetVolume
    (
      string id,
      double? volume
    );

    /// <summary>
    /// Restart a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> Start(uint id);

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> StartAll();

    /// <summary>
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> StartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    IAsyncEnumerable<bool> StartRange(List<uint> idList);

    /// <summary>
    /// Stop a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> StopAsync(uint id);

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<bool> StopAll();

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> StopRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> StopRange(List<uint> idList);

    /// <summary>
    /// Unmute a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> UnmuteAsync(uint id);

    /// <summary>
    /// Unmute an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<bool> UnmuteAll();

    /// <summary>
    /// Unmute some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> UnmuteRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Update a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> Update(uint id);

    /// <summary>
    /// Update an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<bool> UpdateAll();

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> UpdateRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> UpdateRange(List<uint> idList);

    /// <summary>
    /// Update the service.
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<bool> UpdateServiceAsync();

    #endregion
  }
}