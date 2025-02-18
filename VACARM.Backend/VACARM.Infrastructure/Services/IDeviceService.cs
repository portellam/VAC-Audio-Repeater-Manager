using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IDeviceService
    <
      TRepository,
      TDeviceModel
    >
    where TRepository :
    BaseRepository<TDeviceModel>
    where TDeviceModel :
    DeviceModel
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="actualId">The actual ID</param>
    /// <returns>The item.</returns>
    TDeviceModel? GetByActualId(string actualId);

    /// <summary>
    /// Get a default communications <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <returns>The item.</returns>
    TDeviceModel? GetDefaultCommunications();

    /// <summary>
    /// Get a default console <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <returns>The item.</returns>
    TDeviceModel? GetDefaultConsole();

    /// <summary>
    /// Get a default multimedia <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <returns>The item.</returns>
    TDeviceModel? GetDefaultMultimedia();

    /// <summary>
    /// Get an enumerable of all absent <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllAbsent();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TDeviceModel"/>(s) in
    /// alphebetical order.
    /// <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllAlphabetical();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TDeviceModel"/>(s) in
    /// alphebetical descending order.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllAlphabeticalDescending();

    /// <summary>
    /// Get an enumerable of all capture <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllCapture();

    /// <summary>
    /// Get an enumerable of all communications <typeparamref name="TDeviceModel"/> 
    ///(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllCommunications();

    /// <summary>
    /// Get an enumerable of all console <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllConsole();

    /// <summary>
    /// Get an enumerable of all default <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllDefault();

    /// <summary>
    /// Get an enumerable of all disabled <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllDisabled();

    /// <summary>
    /// Get an enumerable of all duplex <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllDuplex();

    /// <summary>
    /// Get an enumerable of all enabled <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllEnabled();

    /// <summary>
    /// Get an enumerable of all multimedia <typeparamref name="TDeviceModel"/>
    ///(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllMultimedia();

    /// <summary>
    /// Get an enumerable of all muted <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllMuted();

    /// <summary>
    /// Get an enumerable of all not muted
    /// <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllUnmuted();

    /// <summary>
    /// Get an enumerable of all present <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllPresent();

    /// <summary>
    /// Get an enumerable of all render <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDeviceModel> GetAllRender();

    #endregion
  }
}