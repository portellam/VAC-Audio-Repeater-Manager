using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface ICoreAudioService<TRepository, TDevice> where
    TRepository :
    CoreAudioRepository<TDevice> where TDevice :
    Device
  {
    #region Logic

    /// <summary>
    /// Is a default <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefault(string id);

    /// <summary>
    /// Is a default communications<typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefaultCommunications(string id);

    /// <summary>
    /// Is a muted <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsMuted(string id);

    /// <summary>
    /// Get the volume of the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    double GetVolume(string id);

    /// <summary>
    /// Get a default <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TDevice? GetDefault();

    /// <summary>
    /// Get a default communications <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TDevice? GetDefaultCommunications();

    /// <summary>
    /// Get an enumerable of all muted <typeparamref name="TDevice"/> 
    ///(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllMuted();

    /// <summary>
    /// Get an enumerable of all unmuted <typeparamref name="TDevice"/> 
    ///(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllUnmuted();

    #endregion
  }
}