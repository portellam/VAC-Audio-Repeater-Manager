using VACARM.Domain.Models;

namespace VACARM.Application.Controllers
{
  public interface IRepeaterController : IGenericController<RepeaterModel>
  {
    #region Logic

    /// <summary>
    /// Restart a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Restart(uint id);

    /// <summary>
    /// Restart all <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    void RestartAll();

    /// <summary>
    /// Restart some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RestartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Restart some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void RestartRange(List<uint> idList);

    /// <summary>
    /// Restart a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Start(uint id);

    /// <summary>
    /// Start all <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    void StartAll();

    /// <summary>
    /// Start some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void StartRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Start some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void StartRange(List<uint> idList);

    /// <summary>
    /// Stop a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Stop(uint id);

    /// <summary>
    /// Stop all <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    void StopAll();

    /// <summary>
    /// Stop some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void StopRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Stop some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    void StopRange(List<uint> idList);

    #endregion
  }
}