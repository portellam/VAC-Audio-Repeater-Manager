using System.Collections.Generic;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface IDeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    >
  {
    #region Logic

    /// <summary>
    /// Mute the enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<IEnumerable<bool>> MuteAll();

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    Task<IEnumerable<bool>> MuteRange
    (IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Mute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result</returns>
    Task<IEnumerable<bool>> MuteRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Unmute the enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<IEnumerable<bool>> UnmuteAll();

    /// <summary>
    /// Unmute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result.</returns>
    Task<IEnumerable<bool>> UnmuteRange
    (IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Unmute an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result</returns>
    Task<IEnumerable<bool>> UnmuteRange
    (
      uint startId,
      uint endId
    );

    #endregion
  }
}