using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IDeviceRepository : 
    IBaseRepository<DeviceModel>
  {
    #region Logic

    /// <summary>
    /// Get an enumerable of all absent <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllAbsent();

    /// <summary>
    /// Get an enumerable of all alphebetical order 
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
    /// Get an enumerable of all reverse alphebetical order 
    /// <typeparamref name="DeviceModel"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<DeviceModel> GetAllInReverseAlphabeticalOrder();

    #endregion
  }
}