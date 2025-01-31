using AudioSwitcher.AudioApi.CoreAudio;

namespace VACARM.Application.Controllers
{
  public interface ICoreAudioControllerExtension
  {
    #region

    /// <summary>
    /// Get the volume of the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    double GetVolume(string id);

    /// <summary>
    /// Is the audio device the default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false is the audio device the default.</returns>
    Task<bool> IsDefault(string id);

    /// <summary>
    /// Is the audio device the default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false is the audio device the default for 
    /// communications.</returns>
    Task<bool> IsDefaultCommunications(string id);

    /// <summary>
    /// Is the audio device muted.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false is the audio device muted.</returns>
    Task<bool> IsMuted(string id);

    /// <summary>
    /// Get the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio device.</returns>
    Task<CoreAudioDevice?> Get(string id);

    /// <summary>
    /// Mute the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<int> DoMute(string id);

    /// <summary>
    /// Unmute the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<int> DoUnmute(string id);

    /// <summary>
    /// Set the audio device as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The exit code.</returns>
    Task<int> SetAsDefault(string id);

    /// <summary>
    /// Set the audio device as default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The exit code.</returns>
    Task<int> SetAsDefaultCommunications(string id);

    /// <summary>
    /// Set the audio device volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    Task<int> SetVolume
    (
      string id,
      double volume
    );

    #endregion 
  }
}