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
      Func<TRepeaterModel, bool> func = (TRepeaterModel x) =>
        deviceId == x.InputDeviceId
        || deviceId == x.OutputDeviceId;

      return base.GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllStarted()
    {
      Func<TRepeaterModel, bool> func = (TRepeaterModel x) => x.IsStarted;
      return base.GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllStopped()
    {
      Func<TRepeaterModel, bool> func = (TRepeaterModel x) => !x.IsStarted;
      return base.GetRange(func);
    }

    #endregion
  }
}