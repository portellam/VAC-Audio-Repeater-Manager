using NAudio.CoreAudioApi;
using System.Collections.Generic;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services.MMDeviceService
{
  public partial interface IMMDeviceService
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
    TMMDevice Get(string id);

    /// <summary>
    /// Get the default communications <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice GetDefaultCommunications(DataFlow dataFlow);

    /// <summary>
    /// Get the default console <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice GetDefaultConsole(DataFlow dataFlow);

    /// <summary>
    /// Get the default multimedia <typeparamref name="TMMDevice"/>.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    TMMDevice GetDefaultMultimedia(DataFlow dataFlow);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TMMDevice"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable);

    /// <summary>
    /// Update the service.
    /// </summary>
    void UpdateService();

    #endregion
  }
}