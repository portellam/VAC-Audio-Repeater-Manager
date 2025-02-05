using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IRepeaterRepository<T> :
    IBaseRepository<T> where T :
    RepeaterModel
  {
    #region Logic

    /// <summary>
    /// Get an enumerable of all <typeparamref name="RepeaterModel"/> item(s) in
    /// alphebetical order.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<RepeaterModel> GetAllAlphabeticalOrder();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="RepeaterModel"/> 
    /// item(s) with a match of the audio device ID of either the capture or render.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<RepeaterModel> GetAllByDeviceId(uint deviceId);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="RepeaterModel"/> item(s) in
    /// reverse alphebetical order.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<RepeaterModel> GetAllReverseAlphabeticalOrder();

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