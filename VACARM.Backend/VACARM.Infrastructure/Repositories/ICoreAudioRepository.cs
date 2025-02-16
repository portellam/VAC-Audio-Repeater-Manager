using AudioSwitcher.AudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public partial interface ICoreAudioRepository<TDevice> where TDevice :
    Device
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TDevice? Get(string id);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TDevice"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TDevice> GetRange(IEnumerable<string> idEnumerable);

    #endregion
  }
}