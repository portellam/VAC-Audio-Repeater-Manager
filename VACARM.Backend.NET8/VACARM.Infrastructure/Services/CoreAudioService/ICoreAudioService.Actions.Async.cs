using AudioSwitcher.AudioApi.CoreAudio;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface ICoreAudioService
    <
      TRepository,
      TEnumerable,
      TDevice
    >
  {
    #region Logic

    /// <summary>
    /// Is the <typeparamref name="TDevice"/> the default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> IsDefaultAsync(string id);

    /// <summary>
    /// Is the <typeparamref name="TDevice"/> the default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> IsDefaultCommunicationsAsync(string id);

    /// <summary>
    /// Is the <typeparamref name="TDevice"/> muted.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> IsMutedAsync(string id);

    /// <summary>
    /// Mute the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> MuteAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="TDevice"/> as default.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="TDevice"/> as default for communications.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetAsDefaultCommunicationsAsync(string id);

    /// <summary>
    /// Set the <typeparamref name="TDevice"/> volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>True/false result.</returns>
    Task<bool> SetVolumeAsync
    (
      string id,
      double? volume
    );

    /// <summary>
    /// Unmute the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    Task<bool> UnmuteAsync(string id);

    #endregion 
  }
}