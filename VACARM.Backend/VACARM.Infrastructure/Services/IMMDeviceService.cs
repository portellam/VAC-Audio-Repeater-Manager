using NAudio.CoreAudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public interface IMMDeviceService
    <
      TRepository,
      TMMDevice
    >
    where TRepository :
    ReadonlyRepository<MMDevice>
    where TMMDevice :
    MMDevice
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TMMDevice? Get(string id);

    /// <summary>
    /// Get the default communications <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice? GetDefaultCommunications(DataFlow dataFlow);

    /// <summary>
    /// Get the default console <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice? GetDefaultConsole(DataFlow dataFlow);

    /// <summary>
    /// Get the default multimedia <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice? GetDefaultMultimedia(DataFlow dataFlow);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable);

    /// <summary>
    /// Reset a <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Reset(string? id);

    /// <summary>
    /// Reset an enumerable of all <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    void ResetAll();

    /// <summary>
    /// Reset an enumerable of some <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void ResetRange(IEnumerable<string> id);

    /// <summary>
    /// Start a <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Start(string? id);

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    void StartAll();

    /// <summary>
    /// Reset an enumerable of some <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void StartRange(IEnumerable<string> id);

    /// <summary>
    /// Stop a <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Stop(string? id);

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    void StopAll();

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void StopRange(IEnumerable<string> id);

    /// <summary>
    /// Update a <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Update(string? id);

    /// <summary>
    /// Update an enumerable of all <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void UpdateRange(IEnumerable<string> id);

    /// <summary>
    /// Update the service.
    /// </summary>
    void UpdateService();

    #endregion
  }
}