using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A snapshot of audio device repeaters.
  /// </summary>
  public class RepeaterRepository :
    BaseRepository<RepeaterModel>,
    IRepeaterRepository
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository()
    {
      List = new List<RepeaterModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository(IEnumerable<RepeaterModel> enumerable)
    {
      List = enumerable.ToList();
    }

    public IEnumerable<RepeaterModel> GetAllStarted()
    {
      return List
        .Select(x => x.IsStarted);
    }

    public IEnumerable<RepeaterModel> GetAllStopped()
    {
      return List
        .Select(x => !x.IsStarted);
    }

    #endregion
  }
}