using AudioSwitcher.AudioApi.CoreAudio;

namespace VACARM.Application.Commands
{
  public static class CoreAudioCommands
  {
    #region Parameters

    /// <summary>
    /// The maximum audio volume.
    /// </summary>
    private static double MaxVolume
    {
      get
      {
        return 1;
      }
    }

    /// <summary>
    /// The minimum audio volume.
    /// </summary>
    private static double MinVolume
    {
      get
      {
        return 0;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Is the audio volume valid.
    /// </summary>
    /// <param name="volume">The volume</param>
    /// <returns>True/false is the audio volume valid.</returns>
    private static bool IsVolumeValid(double? volume)
    {
      if (volume == null)
      {
        return false;
      }

      return volume <= MaxVolume
        && volume >= MinVolume;
    }

    /// <summary>
    /// Set the audio device volume.
    /// </summary>
    /// <param name="model">The audio device</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    public static bool SetVolume
    (
      CoreAudioDevice? model,
      double? volume
    )
    {
      bool result = IsVolumeValid(volume);

      if (!result)
      {
        return result;
      }

      if (model == null)
      {
        return result;
      }

      model.Volume = (double)volume;
      return result;
    }

    /// <summary>
    /// Mute the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    public async static Task<bool> DoMute(CoreAudioDevice? model)
    {
      bool result = false;

      if (model == null)
      {
        return result;
      }

      result = model.IsMuted;

      if (result)
      {
        return result;
      }

      result = await model
        .ToggleMuteAsync()
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Unmute the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    public async static Task<bool> DoUnmute(CoreAudioDevice? model)
    {
      bool result = false;

      if (model == null)
      {
        return result;
      }

      result = !model.IsMuted;

      if (result)
      {
        return result;
      }

      result = await model
        .ToggleMuteAsync()
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Set the audio device as default.
    /// </summary>
    /// <param name="model">The audio device</param>
    /// <returns>The true/false result.</returns>
    public async static Task<bool> SetAsDefault(CoreAudioDevice? model)
    {
      bool result = false;

      if (model == null)
      {
        return result;
      }

      result = await model
        .SetAsDefaultAsync()
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Set the audio device as default for communications.
    /// </summary>
    /// <param name="model">The audio device</param>
    /// <returns>The true/false result.</returns>
    public async static Task<bool> SetAsDefaultCommunications
    (CoreAudioDevice? model)
    {
      bool result = false;

      if (model == null)
      {
        return result;
      }

      result = await model
        .SetAsDefaultCommunicationsAsync()
        .ConfigureAwait(false);

      return result;
    }

    #endregion
  }
}