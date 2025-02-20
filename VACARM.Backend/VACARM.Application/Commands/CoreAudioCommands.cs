using AudioSwitcher.AudioApi.CoreAudio;

namespace VACARM.Application.Commands
{
  /// <summary>
  /// Extended functionality over <typeparamref name="MMDevice"/>.
  /// </summary>
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
    /// <param name="item">The audio device</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    public static bool SetVolume
    (
      CoreAudioDevice? item,
      double? volume
    )
    {
      bool result = IsVolumeValid(volume);

      if (!result)
      {
        return result;
      }

      if (item == null)
      {
        return result;
      }

      item.Volume = (double)volume;
      return result;
    }

    /// <summary>
    /// Mute the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    public async static Task<bool> DoMuteAsync(CoreAudioDevice? item)
    {
      bool result = false;

      if (item == null)
      {
        return result;
      }

      result = item.IsMuted;

      if (result)
      {
        return result;
      }

      result = await item
        .ToggleMuteAsync()
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Unmute the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    public async static Task<bool> DoUnmuteAsync(CoreAudioDevice? item)
    {
      bool result = false;

      if (item == null)
      {
        return result;
      }

      result = !item.IsMuted;

      if (result)
      {
        return result;
      }

      result = await item
        .ToggleMuteAsync()
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Set the audio device as default.
    /// </summary>
    /// <param name="item">The audio device</param>
    /// <returns>The true/false result.</returns>
    public async static Task<bool> SetAsDefaultAsync(CoreAudioDevice? item)
    {
      bool result = false;

      if (item == null)
      {
        return result;
      }

      result = await item
        .SetAsDefaultAsync()
        .ConfigureAwait(false);

      return result;
    }

    /// <summary>
    /// Set the audio device as default for communications.
    /// </summary>
    /// <param name="item">The audio device</param>
    /// <returns>The true/false result.</returns>
    public async static Task<bool> SetAsDefaultCommunicationsAsync
    (CoreAudioDevice? item)
    {
      bool result = false;

      if (item == null)
      {
        return result;
      }

      result = await item
        .SetAsDefaultCommunicationsAsync()
        .ConfigureAwait(false);

      return result;
    }

    #endregion
  }
}