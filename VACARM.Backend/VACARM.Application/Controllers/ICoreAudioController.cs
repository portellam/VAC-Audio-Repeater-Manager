using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public interface ICoreAudioController<T1, T2> :
    IGenericListController<IGenericListRepository<T2>, T2> where T1 :
    ICoreAudioRepository<T2> where T2 :
    Device
  {
    #region

    /// <summary>
    /// Mute the <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> DoMute(string id);

    /// <summary>
    /// Unmute the <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> DoUnmute(string id);

    /// <summary>
    /// Is the <typeparamref name="CoreAudio"/> item the default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsDefault(string id);

    /// <summary>
    /// Is the <typeparamref name="CoreAudio"/> item the default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsDefaultCommunications(string id);

    /// <summary>
    /// Is the <typeparamref name="CoreAudio"/> item muted.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsMuted(string id);

    /// <summary>
    /// Set the <typeparamref name="CoreAudio"/> item as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetAsDefault(string id);

    /// <summary>
    /// Set the <typeparamref name="CoreAudio"/> item as default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetAsDefaultCommunications(string id);

    /// <summary>
    /// Get the default communications <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultCommunications
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default console <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultConsole
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default multimedia <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultMultimedia
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> Get(string id);

    /// <summary>
    /// Get the volume of the <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    Task<double> GetVolume(string id);

    /// <summary>
    /// Set the <typeparamref name="CoreAudio"/> item volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetVolume
    (
      string id,
      double? volume
    );

    #endregion 
  }
}