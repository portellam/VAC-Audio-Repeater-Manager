using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface ICoreAudioService
    <
      TRepository,
      TDevice
    >
    where TRepository :
    ReadonlyRepository<TDevice>
    where TDevice :
    Device
  {
    #region Logic


    /// <summary>
    /// Is an absent <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsAbsent(string id);

    /// <summary>
    /// Is a capture <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>

    bool IsCapture(string id);

    /// <summary>
    /// Is a default <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsDefault(string id);

    /// <summary>
    /// Is a default communications<typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsDefaultCommunications(string id);

    /// <summary>
    /// Is a disabled <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsDisabled(string id);

    /// <summary>
    /// Is a duplex <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsDuplex(string id);

    /// <summary>
    /// Is a enabled <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsEnabled(string id);

    /// <summary>
    /// Is a muted <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsMuted(string id);

    /// <summary>
    /// Is a playback <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsPlayback(string id);

    /// <summary>
    /// Is a present <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsPresent(string id);

    /// <summary>
    /// Is an unmuted <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool IsUnmuted(string id);

    /// <summary>
    /// Get the volume of the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    double GetVolume(string id);

    /// <summary>
    /// Get a <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TDevice? Get(string id);

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
    /// Get an enumerable of all absent <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllAbsent();

    /// <summary>
    /// Get an enumerable of all capture <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllCapture();

    /// <summary>
    /// Get an enumerable of all disabled <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllDisabled();

    /// <summary>
    /// Get an enumerable of all enabled <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllEnabled();

    /// <summary>
    /// Get an enumerable of all muted <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllMuted();

    /// <summary>
    /// Get an enumerable of all playback <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllPlayback();

    /// <summary>
    /// Get an enumerable of all present <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllPresent();

    /// <summary>
    /// Get an enumerable of all unmuted <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetAllUnmuted();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetRange(IEnumerable<string> idEnumerable);

    #endregion
  }
}