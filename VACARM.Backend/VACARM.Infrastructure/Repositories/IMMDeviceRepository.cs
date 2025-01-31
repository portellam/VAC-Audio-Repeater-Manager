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
    /// Get a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The <typeparamref name="MMDevice"/> item.</returns>
    MMDevice? Get(Func<MMDevice, bool> func);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The <typeparamref name="MMDevice"/> enumerable of 
    /// item(s).</returns>
    IEnumerable<MMDevice> GetAll();

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
    /// Get an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The <typeparamref name="MMDevice"/> enumerable of 
    /// item(s).</returns>
    IEnumerable<MMDevice> GetRange(Func<MMDevice, bool> func);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="idList">The list of ID(s)</param>
    /// <returns>The <typeparamref name="MMDevice"/> enumerable of 
    /// item(s).</returns>
    List<MMDevice> GetRange(List<string> idList);
    
    #endregion
  }
}