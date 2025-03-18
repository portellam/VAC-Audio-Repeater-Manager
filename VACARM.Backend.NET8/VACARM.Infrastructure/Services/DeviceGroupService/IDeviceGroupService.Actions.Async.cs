using System.Collections.Generic;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface IDeviceGroupService<TDeviceModel>
  {
    #region Logic

    /// <summary>
    /// Mute a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> MuteAsync(uint id);

    /// <summary>
    /// Set the <typeparamref name="TDeviceModel"/> as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultAsync(uint id);

    /// <summary>
    /// Set the <typeparamref name="TDeviceModel"/> as default for
    /// communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultCommunicationsAsync(uint id);

    /// <summary>
    /// Set the <typeparamref name="TDeviceModel"/> volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetVolumeAsync
    (
      uint id,
      double? volume
    );

    /// <summary>
    /// Unmute a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> UnmuteAsync(uint id);

    /// <summary>
    /// Update an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> UpdateAllAsync();

    #endregion
  }
}