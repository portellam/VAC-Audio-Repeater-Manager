﻿using AudioSwitcher.AudioApi.CoreAudio;

namespace VACARM.Infrastructure.Services
{
  public partial interface ICoreAudioService
    <
      TRepository,
      TDevice
    >
  {
    #region Logic

    /// <summary>
    /// Is the <typeparamref name="TDevice"/> the default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> IsDefaultAsync(string id);

    /// <summary>
    /// Is the <typeparamref name="TDevice"/> the default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> IsDefaultCommunicationsAsync(string id);

    /// <summary>
    /// Is the <typeparamref name="TDevice"/> muted.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> IsMutedAsync(string id);

    /// <summary>
    /// Mute the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> MuteAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="TDevice"/> as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="TDevice"/> as default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultCommunicationsAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="TDevice"/> volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetVolumeAsync
    (
      string id,
      double? volume
    );

    /// <summary>
    /// Unmute the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> UnmuteAsync(string id);

    /// <summary>
    /// Update the enumerable of all <typeparamref name="TDevice"/>
    /// items.
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<bool> UpdateServiceAsync();

    /// <summary>
    /// Get the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetAsync(string id);

    /// <summary>
    /// Get the default communications <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default console <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default multimedia <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the volume of the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    Task<double> GetVolumeAsync(string id);

    #endregion 
  }
}