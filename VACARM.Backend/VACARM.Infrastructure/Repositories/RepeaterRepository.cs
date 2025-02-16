using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A snapshot repository of audio device repeaters.
  /// </summary>
  public class RepeaterRepository<TRepeaterModel> :
    BaseRepository<TRepeaterModel>,
    IRepeaterRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository()
    {
      this.List = new List<TRepeaterModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository(IEnumerable<TRepeaterModel> enumerable)
    {
      this.List = enumerable.ToList();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxCount">The maximum count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository(int maxCount)
    {
      this.List = new List<TRepeaterModel>();
      this.MaxCount = maxCount;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <param name="maxCount">The maximum count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository
    (
      IEnumerable<TRepeaterModel> enumerable,
      int maxCount
    )
    {
      this.List = enumerable.ToList();
      this.MaxCount = maxCount;
    }

    #endregion
  }
}