using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public interface IMMDeviceRepository<TMMDevice> where TMMDevice :
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