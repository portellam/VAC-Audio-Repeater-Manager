using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IDeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    >
    where TGroupReadonlyRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TBaseRepository :
    BaseRepository<TDeviceModel>
    where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    CoreAudioService<ReadonlyRepository<Device>, Device> CoreAudioService { get; }
    MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> MMDeviceService { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Restart a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Restart(uint id);

    /// <summary>
    /// Restart the enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    void RestartAll();

    /// <summary>
    /// Restart some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void RestartRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Restart an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RestartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Start a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Start(uint id);

    /// <summary>
    /// Start the enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    void StartAll();

    /// <summary>
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void StartRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void StartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Stop a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Stop(uint id);

    /// <summary>
    /// Stop the enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    void StopAll();

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void StopRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void StopRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Update a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Update(uint id);

    /// <summary>
    /// Update the enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void UpdateRange(IEnumerable<uint> idEnumerable);


    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void UpdateRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Update the selected <typeparamref name="TBaseService"/>.
    /// </summary>
    void UpdateSelectedService();

    #endregion
  }
}