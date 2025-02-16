using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IRepeaterService<TRepository, TRepeaterModel> where
    TRepository :
    RepeaterRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    bool PreferLegacyExecutable { get; set; }
    DeviceService<DeviceRepository<DeviceModel>, DeviceModel> DeviceService { get; }
    string CustomExecutablePathName { get; set; }
    string ExecutableFullPathName { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TRepeaterModel"/>(s) in
    /// alphebetical order.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllAlphabetical();

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TRepeaterModel"/>(s) with a
    /// match of the audio device name of either the capture or render.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllByDeviceId(uint deviceId);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TRepeaterModel"/>(s) with a
    /// match of the audio device name of either the capture or render.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllByDeviceName(string deviceId);

    /// <summary>
    /// Get an enumerable of all started <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllStarted();

    /// <summary>
    /// Get an enumerable of all started <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllStopped();

    #endregion
  }
}