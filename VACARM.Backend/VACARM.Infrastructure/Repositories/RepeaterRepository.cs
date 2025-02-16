using System.Collections.Generic;
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
    /// <param name="list">The list of item(s)</param>
    /// <param name="maxCount">The maximum count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository
    (
      List<TRepeaterModel> list,
      int maxCount
    ) :
      base
      (
        list,
        maxCount
      )
    {
      this.List = list;
      this.MaxCount = maxCount;
    }

    #endregion
  }
}