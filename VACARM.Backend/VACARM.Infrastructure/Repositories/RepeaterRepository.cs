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
      List = new List<TRepeaterModel>();
    }

    public TRepeaterModel? Get(uint id)
    {
      return base.Get(id);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public RepeaterRepository(IEnumerable<TRepeaterModel> enumerable)
    {
      List = enumerable.ToList();
    }

    public IEnumerable<TRepeaterModel> GetAllAlphabetical()
    {
      return base
        .GetAll()
        .OrderBy(x => x.WindowName);
    }

    public IEnumerable<TRepeaterModel> GetAllAlphabeticalDescending()
    {
      return base
        .GetAll()
        .OrderByDescending(x => x.WindowName);
    }

    public IEnumerable<TRepeaterModel> GetAllByDeviceId(uint deviceId)
    {
      return base
        .GetAll()
        .Where
        (
          x => deviceId == x.InputDeviceId
          || deviceId == x.OutputDeviceId
        );
    }

    public IEnumerable<TRepeaterModel> GetAllStarted()
    {
      return base
        .GetAll()
        .Where(x => x.IsStarted);
    }

    public IEnumerable<TRepeaterModel> GetAllStopped()
    {
      return base
        .GetAll()
        .Where(x => !x.IsStarted);
    }

    #endregion
  }
}