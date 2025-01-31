using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public interface IMMDeviceRepository
  {
    #region Logic

    /// <summary>
    /// Is the audio device present.
    /// </summary>
    /// <param name="mMDevice">The audio device</param>
    /// <returns>True/false is the audio device present.</returns>
    bool IsPresent(MMDevice? mMDevice);

    /// <summary>
    /// Is the audio device started.
    /// </summary>
    /// <param name="mMDevice">The audio device</param>
    /// <returns>True/false is the audio device started.</returns>
    bool IsStarted(MMDevice? mMDevice);

    /// <summary>
    /// Get an audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio device.</returns>
    MMDevice? Get(string id);

    /// <summary>
    /// Get a list of all started audio device(s).
    /// </summary>
    /// <returns>The list of all started audio device(s).</returns>
    List<MMDevice> GetAllStarted();

    /// <summary>
    /// Get a list of all stopped audio device(s).
    /// </summary>
    /// <returns>The list of all stopped audio device(s).</returns>
    List<MMDevice> GetAllStopped();

    /// <summary>
    /// Get a list of some audio device(s).
    /// </summary>
    /// <param name="idList">The list of ID(s)</param>
    /// <returns>The list of some audio device(s).</returns>
    List<MMDevice> GetRange(List<string> idList);
    
    #endregion
  }
}