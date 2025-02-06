using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;

namespace VACARM.Infrastructure.Repositories
{
  public partial interface ICoreAudioRepository<T> :
    IGenericRepository<T> where T :
    Device
  {
    #region Logic

    /// <summary>
    /// Is a default <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsDefaultAsync(string id);

    /// <summary>
    /// Is a default communications<typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsDefaultCommunicationsAsync(string id);

    /// <summary>
    /// Is a muted <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    Task<bool> IsMutedAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="CoreAudioDevice"/> item volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    Task<bool> SetVolumeAsync
    (
      string id,
      double volume
    );

    /// <summary>
    /// Get the volume of the <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    Task<double> GetVolumeAsync(string id);

    /// <summary>
    /// Get a <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetAsync(string id);

    /// <summary>
    /// Get a default communications <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultCommunicationsAsync(string id);

    /// <summary>
    /// Get a default console <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultConsoleAsync(string id);

    /// <summary>
    /// Get a default multimedia <typeparamref name="CoreAudioDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice?> GetDefaultMultimediaAsync(string id);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="CoreAudioDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    Task<IEnumerable<CoreAudioDevice>> GetAllAsync();

    /// <summary>
    /// Get an enumerable of all muted <typeparamref name="CoreAudioDevice"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    Task<IEnumerable<CoreAudioDevice>> GetAllMutedAsync();

    /// <summary>
    /// Get an enumerable of all not muted <typeparamref name="CoreAudioDevice"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    Task<IEnumerable<CoreAudioDevice>> GetAllNotMutedAsync();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="CoreAudioDevice"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of item(s).</returns>
    Task<IEnumerable<CoreAudioDevice>> GetRangeAsync
    (Func<CoreAudioDevice, Task<bool>> func);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="CoreAudioDevice"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    Task<IEnumerable<CoreAudioDevice>> GetRangeAsync(IEnumerable<string> idList);

    #endregion
  }
}