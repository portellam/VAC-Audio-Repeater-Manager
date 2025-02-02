using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public interface IMMDeviceRepository<T> :
    IGenericRepository<T> where T :
    MMDevice
  {
    #region Logic

    /// <summary>
    /// Is a capture <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsCapture(string id);

    /// <summary>
    /// Is a default <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefault(string id);

    /// <summary>
    /// Is a duplex <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDuplex(string id);

    /// <summary>
    /// Is an enabled <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false is an enabled item.</returns>
    bool IsEnabled(string id);

    /// <summary>
    /// Is a <typeparamref name="MMDevice"/> item present.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsPresent(string id);

    /// <summary>
    /// Is a render <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsRender(string id);

    /// <summary>
    /// Get a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    MMDevice? Get(string id);

    /// <summary>
    /// Get the default <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    MMDevice? GetDefault();

    /// <summary>
    /// Get an enumerable of all absent <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetAllAbsent();

    /// <summary>
    /// Get an enumerable of all capture <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetAllCapture();

    /// <summary>
    /// Get an enumerable of all disabled <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetAllDisabled();

    /// <summary>
    /// Get an enumerable of all duplex <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetAllDuplex();

    /// <summary>
    /// Get an enumerable of all enabled <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetAllEnabled();

    /// <summary>
    /// Get an enumerable of all present <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetAllPresent();

    /// <summary>
    /// Get an enumerable of all render <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetAllRender();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetRange(IEnumerable<string> idList);

    #endregion
  }
}