using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial interface ICoreAudioController<TRepository, TItem> :
    IGenericController<CoreAudioRepository<Device>, Device> where TRepository :
    CoreAudioRepository<Device> where TItem :
    Device
  {
    #region Logic

    /// <summary>
    /// Mute the <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> DoMuteAsync(string id);

    /// <summary>
    /// Unmute the <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> DoUnmuteAsync(string id);

    /// <summary>
    /// Is the <typeparamref name="CoreAudioDevice"/> item the default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsDefaultAsync(string id);

    /// <summary>
    /// Is the <typeparamref name="CoreAudioDevice"/> item the default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsDefaultCommunicationsAsync(string id);

    /// <summary>
    /// Is the <typeparamref name="CoreAudioDevice"/> item muted.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsMutedAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="CoreAudioDevice"/> item as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetAsDefaultAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="CoreAudioDevice"/> item as default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetAsDefaultCommunicationsAsync(string id);

    /// <summary>
    /// Get the <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetAsync(string id);

    /// <summary>
    /// Get the default communications <typeparamref name="CoreAudioDevice"/> item.
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
    /// Get the default console <typeparamref name="CoreAudioDevice"/> item.
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
    /// Get the default multimedia <typeparamref name="CoreAudioDevice"/> item.
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
    /// Get the volume of the <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    Task<double> GetVolumeAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="CoreAudioDevice"/> item volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetVolumeAsync
    (
      string id,
      double? volume
    );

    /// <summary>
    /// Update the enumerable of all <typeparamref name="CoreAudioDevice"/>
    /// items.
    /// </summary>
    /// <returns>The true/false result.</returns>
    Task<bool> UpdateAllAsync();

    #endregion 
  }
}