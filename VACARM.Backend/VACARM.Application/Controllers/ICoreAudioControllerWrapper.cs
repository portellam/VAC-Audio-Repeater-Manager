using AudioSwitcher.AudioApi.CoreAudio;

namespace VACARM.Application.Controllers
{
  public interface ICoreAudioControllerWrapper
  {
    #region

    /// <summary>
    /// Mute the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> DoMute(string id);

    /// <summary>
    /// Unmute the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> DoUnmute(string id);

    /// <summary>
    /// Is the audio device the default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsDefault(string id);

    /// <summary>
    /// Is the audio device the default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsDefaultCommunications(string id);

    /// <summary>
    /// Is the audio device muted.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsMuted(string id);

    /// <summary>
    /// Set the audio device as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetAsDefault(string id);

    /// <summary>
    /// Set the audio device as default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetAsDefaultCommunications(string id);

    /// <summary>
    /// Get the default communications audio device.
    /// </summary>
    /// <param name="isInput">True/false is the audio device an input</param>
    /// <param name="isOutput">True/false is the audio device an output</param>
    /// <returns>The audio device.</returns>
    Task<CoreAudioDevice?> GetDefaultCommunications
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default console audio device.
    /// </summary>
    /// <param name="isInput">True/false is the audio device an input</param>
    /// <param name="isOutput">True/false is the audio device an output</param>
    /// <returns>The audio device.</returns>
    Task<CoreAudioDevice?> GetDefaultConsole
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default multimedia audio device.
    /// </summary>
    /// <param name="isInput">True/false is the audio device an input</param>
    /// <param name="isOutput">True/false is the audio device an output</param>
    /// <returns>The audio device.</returns>
    Task<CoreAudioDevice?> GetDefaultMultimedia
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio device.</returns>
    Task<CoreAudioDevice?> Get(string id);

    /// <summary>
    /// Get the volume of the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    Task<double> GetVolume(string id);

    /// <summary>
    /// Set the audio device volume.
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