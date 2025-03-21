using System.Collections.Generic;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface IDeviceGroupService
  {
    #region Logic

    /// <summary>
    /// Mute the enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> MuteAllAsync();

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> MuteRangeAsync
    (IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> MuteRangeAsync
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Unmute the enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> UnmuteAllAsync();

    /// <summary>
    /// Unmute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> UnmuteRangeAsync
    (IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Unmute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result.</returns>
    IAsyncEnumerable<bool> UnmuteRangeAsync
    (
      uint startId,
      uint endId
    );

    #endregion
  }
}