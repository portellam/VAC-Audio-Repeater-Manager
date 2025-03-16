using NAudio.CoreAudioApi;
using System.Collections.Generic;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services.MMDeviceService
{
  public partial interface IMMDeviceService
    <
      TRepository,
      TEnumerable,
      TMMDevice
    >
  {
    #region Logic

    /// <summary>
    /// Reset a <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Reset(string id);

    /// <summary>
    /// Reset the enumerable of all <typeparamref name="TMMDevice"/>(s).
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
    void Start(string id);

    /// <summary>
    /// Start the enumerable of all <typeparamref name="TMMDevice"/>(s).
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
    void Stop(string id);

    /// <summary>
    /// Stop the enumerable of all <typeparamref name="TMMDevice"/>(s).
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
    void Update(string id);

    /// <summary>
    /// Update the enumerable of all <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    /// <param name="id">The enumerable of ID(s)</param>
    void UpdateRange(IEnumerable<string> id);

    #endregion
  }
}