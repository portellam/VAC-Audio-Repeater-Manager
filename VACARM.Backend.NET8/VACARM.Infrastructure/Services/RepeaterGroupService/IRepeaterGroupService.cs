using System.Collections.Generic;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial interface IRepeaterGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TRepeaterModel
    >
    where TGroupReadonlyRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TRepeaterModel>,
      TRepeaterModel
    >
    where TBaseRepository :
    BaseRepository<TRepeaterModel>
    where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    bool PreferLegacyExecutable { get; set; }
    string CustomExecutablePathName { get; set; }
    string ExecutableFullPathName { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Get the enumerable of all <typeparamref name="TRepeaterModel"/>(s) in
    /// alphebetical order.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllAlphabetical();

    /// <summary>
    /// Get the enumerable of all <typeparamref name="TRepeaterModel"/>(s) with a
    /// match of the audio device name of either the capture or render.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllByDeviceId(uint deviceId);

    /// <summary>
    /// Get the enumerable of all <typeparamref name="TRepeaterModel"/>(s) with a
    /// match of the audio device name of either the capture or render.
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllByDeviceName(string deviceId);

    /// <summary>
    /// Get the enumerable of all started <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllStarted();

    /// <summary>
    /// Get the enumerable of all started <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TRepeaterModel> GetAllStopped();

    #endregion
  }
}