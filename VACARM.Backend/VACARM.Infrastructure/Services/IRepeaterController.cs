using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public interface IRepeaterService<TRepository, TItem> :
    IBaseService<TRepository, TItem> where TRepository :
    IRepeaterRepository<TItem> where TItem :
    RepeaterModel
  {
    #region Logic

    /// <summary>
    /// Restart a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Restart(uint id);

    /// <summary>
    /// Restart a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Restart(RepeaterModel model);

    /// <summary>
    /// Restart all <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    void RestartAll();

    /// <summary>
    /// Restart some <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RestartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Restart some <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void RestartRange(List<uint> idList);

    /// <summary>
    /// Restart a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Start(uint id);

    /// <summary>
    /// Start all <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    void StartAll();

    /// <summary>
    /// Start some <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void StartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Start some <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void StartRange(List<uint> idList);

    /// <summary>
    /// Stop a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Stop(uint id);

    /// <summary>
    /// Stop all <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    void StopAll();

    /// <summary>
    /// Stop some <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void StopRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Stop some <typeparamref name="RepeaterModel"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void StopRange(List<uint> idList);

    #endregion
  }
}