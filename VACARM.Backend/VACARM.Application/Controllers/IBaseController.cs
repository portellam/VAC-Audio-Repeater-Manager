using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public interface IBaseController<BaseModel> : IController<BaseModel>
  {
    #region Parameters

    BaseRepository Repository { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Restart a <typeparamref name="BaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Restart(uint id);

    /// <summary>
    /// Restart all <typeparamref name="BaseModel"/>(s).
    /// </summary>
    void RestartAll();

    /// <summary>
    /// Restart some <typeparamref name="BaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RestartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Restart some <typeparamref name="BaseModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void RestartRange(List<uint> idList);

    /// <summary>
    /// Restart a <typeparamref name="BaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Start(uint id);

    /// <summary>
    /// Start all <typeparamref name="BaseModel"/>(s).
    /// </summary>
    void StartAll();

    /// <summary>
    /// Start some <typeparamref name="BaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void StartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Start some <typeparamref name="BaseModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void StartRange(List<uint> idList);

    /// <summary>
    /// Stop a <typeparamref name="BaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Stop(uint id);

    /// <summary>
    /// Stop all <typeparamref name="BaseModel"/>(s).
    /// </summary>
    void StopAll();

    /// <summary>
    /// Stop some <typeparamref name="BaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void StopRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Stop some <typeparamref name="BaseModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void StopRange(List<uint> idList);

    /// <summary>
    /// Update a <typeparamref name="BaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Update(uint id);

    /// <summary>
    /// Update all <typeparamref name="BaseModel"/>(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update some <typeparamref name="BaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void UpdateRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Update some <typeparamref name="BaseModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void UpdateRange(List<uint> idList);

    #endregion
  }
}