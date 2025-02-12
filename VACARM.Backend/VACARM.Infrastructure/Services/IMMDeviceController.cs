using NAudio.CoreAudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public interface IMMDeviceService<TRepository, TItem> :
    IGenericService<GenericRepository<MMDevice>, MMDevice> where TRepository :
    MMDeviceRepository<MMDevice> where TItem :
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
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The enumerable of <typeparamref name="MMDevice"/> items.</returns>
    IEnumerable<MMDevice> GetRange(IEnumerable<string> idList);

    /// <summary>
    /// Reset a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<bool> Reset(string? id);

    /// <summary>
    /// Reset an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    Task<bool> ResetAll();

    /// <summary>
    /// Reset an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    IAsyncEnumerable<Task<bool>> ResetRange(IEnumerable<string> id);

    /// <summary>
    /// Start a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<bool> Start(string? id);

    /// <summary>
    /// Start an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    IAsyncEnumerable<Task<bool>> StartAll();

    /// <summary>
    /// Reset an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    IAsyncEnumerable<Task<bool>> StartRange(IEnumerable<string> id);

    /// <summary>
    /// Stop a <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<bool> Stop(string? id);

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    IAsyncEnumerable<Task<bool>> StopAll();

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    IAsyncEnumerable<Task<bool>> StopRange(IEnumerable<string> id);

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