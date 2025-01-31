using AudioSwitcher.AudioApi.CoreAudio;

namespace VACARM.Application.Controllers
{
  public class CoreAudioControllerWrapper : ICoreAudioControllerWrapper
  {
    #region Parameters

    private CoreAudioController Controller { get; set; }

    /// <summary>
    /// The maximum audio volume.
    /// </summary>
    private double MaxVolume
    {
      get
      {
        return 1;
      }
    }

    /// <summary>
    /// The minimum audio volume.
    /// </summary>
    private double MinVolume
    {
      get
      {
        return 0;
      }
    }

    private List<CoreAudioDevice> List { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Is the audio volume valid.
    /// </summary>
    /// <param name="volume">The volume</param>
    /// <returns>True/false is the audio volume valid.</returns>
    private bool IsVolumeValid(double? volume)
    {
      if (volume == null)
      {
        return false;
      }

      return volume <= MaxVolume
        && volume >= MinVolume;
    }

    /// <summary>
    /// Convert an ID from a <typeparamref name="string"/> to a 
    /// <typeparamref name="GUID"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The GUID</returns>
    private Guid ToGuid(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        id = string.Empty;
      }

      return new Guid(id);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public CoreAudioControllerWrapper()
    {
      Controller = new CoreAudioController();
    }

    public async Task<bool> IsDefault(string id)
    {
      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return false;
      }

      return model.IsDefaultDevice;
    }

    public async Task<bool> IsDefaultCommunications(string id)
    {
      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return false;
      }

      return model.IsDefaultCommunicationsDevice;
    }

    public async Task<bool> IsMuted(string id)
    {
      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return false;
      }

      return model.IsMuted;
    }

    /// <summary>
    /// Get the audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio device.</returns>
    public async Task<CoreAudioDevice?> Get(string id)
    {
      if (Controller == null)
      {
        return null;
      }

      Guid guid = ToGuid(id);

      return await Controller
        .GetDeviceAsync(guid)
        .ConfigureAwait(false);
    }

    public async Task<bool> DoMute(string id)
    {
      bool result = false;

      CoreAudioDevice? model = await Get(id);

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

    public async Task<bool> DoUnmute(string id)
    {
      bool result = false;

      CoreAudioDevice? model = await Get(id);

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

    public async Task<bool> SetAsDefault(string id)
    {
      bool result = false;

      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return result;
      }

      result = await model
        .SetAsDefaultAsync()
        .ConfigureAwait(false);

      return result;
    }

    public async Task<bool> SetAsDefaultCommunications(string id)
    {
      bool result = false;

      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return result;
      }

      result = await model
        .SetAsDefaultCommunicationsAsync()
        .ConfigureAwait(false);

      return result;
    }

    public async Task<double> GetVolume(string id)
    {
      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return 0;
      }

      return model.Volume;
    }

    public async Task<bool> SetVolume
    (
      string id,
      double? volume
    )
    {
      bool result = IsVolumeValid(volume);

      if (!result)
      {
        return result;
      }

      CoreAudioDevice? model = await Get(id);

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