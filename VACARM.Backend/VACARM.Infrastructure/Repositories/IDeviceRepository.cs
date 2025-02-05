using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IDeviceRepository<T> :
    IBaseRepository<T> where T : 
    DeviceModel
  {
    #region Logic

    /// <summary>
    /// Get a default communications <typeparamref name="DeviceModel"/> item.
    /// </summary>
    /// <returns>The item.</returns>
    public DeviceModel? GetDefaultCommunications();

    /// <summary>
    /// Get a default console <typeparamref name="DeviceModel"/> item.
    /// </summary>
    /// <returns>The item.</returns>
    public DeviceModel? GetDefaultConsole();

    /// <summary>
    /// Get a default multimedia <typeparamref name="DeviceModel"/> item.
    /// </summary>
    /// <returns>The item.</returns>
    public DeviceModel? GetDefaultMultimedia();

    /// <summary>
    /// Get an enumerable of all absent <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllAbsent();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="DeviceModel"/> item(s) in
    /// alphebetical order.
    /// <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllAlphabeticalOrder();

    /// <summary>
    /// Get an enumerable of all capture <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllCapture();

    /// <summary>
    /// Get an enumerable of all communications <typeparamref name="DeviceModel"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllCommunications();

    /// <summary>
    /// Get an enumerable of all console <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllConsole();

    /// <summary>
    /// Get an enumerable of all default <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllDefault();

    /// <summary>
    /// Get an enumerable of all disabled <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllDisabled();

    /// <summary>
    /// Get an enumerable of all duplex <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllDuplex();

    /// <summary>
    /// Get an enumerable of all enabled <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllEnabled();

    /// <summary>
    /// Get an enumerable of all multimedia <typeparamref name="DeviceModel"/>
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllMultimedia();

    /// <summary>
    /// Get an enumerable of all muted <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllMuted();

    /// <summary>
    /// Get an enumerable of all not muted
    /// <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllNotMuted();

    /// <summary>
    /// Get an enumerable of all present <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllPresent();

    /// <summary>
    /// Get an enumerable of all render <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllRender();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="DeviceModel"/> item(s) in
    /// reverse alphebetical order.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllReverseAlphabeticalOrder();

    #endregion
  }
}