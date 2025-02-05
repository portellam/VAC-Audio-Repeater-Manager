using NAudio.CoreAudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public interface IMMDeviceController<T1, T2> :
    IGenericListController<IGenericListRepository<T2>, T2> where T1 :
    IMMDeviceRepository<T2> where T2 :
    MMDevice
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The <typeparamref name="MMDevice"/> item.</returns>
    MMDevice? Get(string id);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <returns>The enumerable of <typeparamref name="MMDevice"/> items.</returns>
    IEnumerable<MMDevice> GetAll();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The enumerable of <typeparamref name="MMDevice"/> items.</returns>
    IEnumerable<MMDevice> GetRange(IEnumerable<string> idList);

    /// <summary>
    /// Reset a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    void Reset(string? id);

    /// <summary>
    /// Reset an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    void ResetAll();

    /// <summary>
    /// Reset an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void ResetRange(IEnumerable<string> id);

    /// <summary>
    /// Start a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    void Start(string? id);

    /// <summary>
    /// Start an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    void StartAll();

    /// <summary>
    /// Reset an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void StartRange(IEnumerable<string> id);

    /// <summary>
    /// Stop a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    void Stop(string? id);

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    void StopAll();

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void StopRange(IEnumerable<string> id);

    /// <summary>
    /// Update a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    void Update(string? id);

    /// <summary>
    /// Update an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void UpdateRange(IEnumerable<string> id);

    #endregion
  }
}