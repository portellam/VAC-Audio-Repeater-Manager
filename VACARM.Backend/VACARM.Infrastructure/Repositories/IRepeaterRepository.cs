using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IRepeaterRepository<TRepeaterModel> :
    IBaseRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Logic

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TRepeaterModel"/> item(s) in
    /// alphebetical order.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllAlphabetical();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TRepeaterModel"/> item(s) in
    /// alphebetical descending order.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllAlphabeticalDescending();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TRepeaterModel"/> 
    /// item(s) with a match of the audio device ID of either the capture or render.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllByDeviceId(uint deviceId);

    /// <summary>
    /// Get an enumerable of all started <typeparamref name="TRepeaterModel"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllStarted();

    /// <summary>
    /// Get an enumerable of all started <typeparamref name="TRepeaterModel"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllStopped();

    #endregion
  }
}