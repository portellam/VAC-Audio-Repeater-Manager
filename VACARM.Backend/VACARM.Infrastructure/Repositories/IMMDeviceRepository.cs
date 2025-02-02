using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public interface IMMDeviceRepository<T> :
    IGenericRepository<T> where T :
    MMDevice
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    MMDevice? Get(string id);

    /// <summary>
    /// Get the default <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <param name="role">The role</param>
    /// <returns>The item.</returns>
    MMDevice? GetDefault
    (
      DataFlow dataFlow,
      Role role
    );

    /// <summary>
    /// Get the default communications <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    MMDevice? GetDefaultCommunications(DataFlow dataFlow);

    /// <summary>
    /// Get the default console <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    MMDevice? GetDefaultConsole(DataFlow dataFlow);

    /// <summary>
    /// Get the default multimedia <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    MMDevice? GetDefaultMultimedia(DataFlow dataFlow);

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
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<MMDevice> GetRange(IEnumerable<string> idEnumerable);

    /// <summary>
    /// Update the enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update the enumerable of all default 
    /// <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    void UpdateAllDefaults();

    #endregion
  }
}