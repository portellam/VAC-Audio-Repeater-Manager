using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public partial interface ICoreAudioRepository : 
    IGenericRepository<MMDevice>
  {
    #region Logic

    /// <summary>
    /// Is a default <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefault(string id);

    /// <summary>
    /// Is a default communications<typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefaultCommunications(string id);

    /// <summary>
    /// Is a muted <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsMuted(string id);

    /// <summary>
    /// Set the <typeparamref name="CoreAudio"/> item volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    bool SetVolume
    (
      string id,
      double volume
    );

    /// <summary>
    /// Get the volume of the <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    double GetVolume(string id);

    /// <summary>
    /// Get a <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    CoreAudioDevice? Get(string id);

    /// <summary>
    /// Get a default communications <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    CoreAudioDevice? GetDefaultCommunications(string id);

    /// <summary>
    /// Get a default console <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    CoreAudioDevice? GetDefaultConsole(string id);

    /// <summary>
    /// Get a default multimedia <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    CoreAudioDevice? GetDefaultMultimedia(string id);

    /// <summary>
    /// Get an enumerable of all muted <typeparamref name="CoreAudioDevice"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<CoreAudioDevice> GetAllMuted();

    /// <summary>
    /// Get an enumerable of all not muted <typeparamref name="CoreAudioDevice"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<CoreAudioDevice> GetAllNotMuted();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="CoreAudioDevice"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<CoreAudioDevice> GetRange(Func<CoreAudioDevice, bool> func);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="CoreAudioDevice"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<CoreAudioDevice> GetRange(IEnumerable<string> idList);

    #endregion
  }
}