using AudioSwitcher.AudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public partial interface ICoreAudioRepository<TDevice> :
    IGenericRepository<TDevice> where TDevice :
    Device
  {
    #region Logic

    /// <summary>
    /// Is a default <typeparamref name="TDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefault(string id);

    /// <summary>
    /// Is a default communications<typeparamref name="TDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefaultCommunications(string id);

    /// <summary>
    /// Is a muted <typeparamref name="TDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsMuted(string id);

    /// <summary>
    /// Get the volume of the <typeparamref name="TDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    double GetVolume(string id);

    /// <summary>
    /// Get a <typeparamref name="TDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TDevice? Get(string id);

    /// <summary>
    /// Get a default communications <typeparamref name="TDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TDevice? GetDefaultCommunications();

    /// <summary>
    /// Get a default <typeparamref name="TDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TDevice? GetDefault();

    /// <summary>
    /// Get an enumerable of all muted <typeparamref name="TDevice"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllMuted();

    /// <summary>
    /// Get an enumerable of all not muted <typeparamref name="TDevice"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllNotMuted();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TDevice"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetRange(IEnumerable<string> idEnumerable);

    #endregion
  }
}