using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IRepeaterRepository : 
    IBaseRepository<RepeaterModel>
  {
    #region Logic

    /// <summary>
    /// Get an enumerable of all started <typeparamref name="RepeaterModel"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<RepeaterModel> GetAllStarted();

    /// <summary>
    /// Get an enumerable of all started <typeparamref name="RepeaterModel"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<RepeaterModel> GetAllStopped();

    #endregion
  }
}