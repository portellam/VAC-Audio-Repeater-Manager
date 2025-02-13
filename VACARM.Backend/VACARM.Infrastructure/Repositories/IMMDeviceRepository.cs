using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public interface IMMDeviceRepository<TMMDevice> :
    IRepository<TMMDevice> where TMMDevice :
    MMDevice
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TMMDevice? Get(string id);

    /// <summary>
    /// Get the default <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <param name="role">The role</param>
    /// <returns>The item.</returns>
    TMMDevice? GetDefault
    (
      DataFlow dataFlow,
      Role role
    );

    /// <summary>
    /// Get the default communications <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice? GetDefaultCommunications(DataFlow dataFlow);

    /// <summary>
    /// Get the default console <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice? GetDefaultConsole(DataFlow dataFlow);

    /// <summary>
    /// Get the default multimedia <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice? GetDefaultMultimedia(DataFlow dataFlow);

    /// <summary>
    /// Get an enumerable of all absent <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetAllAbsent();

    /// <summary>
    /// Get an enumerable of all capture <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetAllCapture();

    /// <summary>
    /// Get an enumerable of all disabled <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetAllDisabled();

    /// <summary>
    /// Get an enumerable of all duplex <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetAllDuplex();

    /// <summary>
    /// Get an enumerable of all enabled <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetAllEnabled();

    /// <summary>
    /// Get an enumerable of all present <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetAllPresent();

    /// <summary>
    /// Get an enumerable of all render <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetAllRender();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable);

    /// <summary>
    /// Update the enumerable of all <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update the enumerable of all default 
    /// <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    void UpdateAllDefaults();

    #endregion
  }
}