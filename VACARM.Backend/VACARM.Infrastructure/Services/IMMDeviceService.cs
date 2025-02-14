using NAudio.CoreAudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public interface IMMDeviceService<TRepository, TMMDevice> where TRepository :
    MMDeviceRepository<MMDevice> where TMMDevice :
    MMDevice
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The <typeparamref name="TMMDevice"/> item.</returns>
    TMMDevice? Get(string id);

    /// <summary>
    /// Reset a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<bool> Reset(string? id);

    /// <summary>
    /// Reset an enumerable of all <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    IAsyncEnumerable<Task<bool>> ResetAll();

    /// <summary>
    /// Reset an enumerable of some <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    IAsyncEnumerable<Task<bool>> ResetRange(IEnumerable<string> id);

    /// <summary>
    /// Start a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<bool> StartAsync(string? id);

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    IAsyncEnumerable<Task<bool>> StartAll();

    /// <summary>
    /// Reset an enumerable of some <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    IAsyncEnumerable<Task<bool>> StartRange(IEnumerable<string> id);

    /// <summary>
    /// Stop a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<bool> StopAsync(string? id);

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    IAsyncEnumerable<Task<bool>> StopAll();

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    IAsyncEnumerable<Task<bool>> StopRange(IEnumerable<string> id);

    /// <summary>
    /// Update a <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    void Update(string? id);

    /// <summary>
    /// Update an enumerable of all <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void UpdateRange(IEnumerable<string> id);

    #endregion
  }
}