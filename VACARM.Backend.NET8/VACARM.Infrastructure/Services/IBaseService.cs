using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public interface IBaseService
    <
      TRepository, 
      TBaseModel
    > 
    where TRepository :
    BaseRepository<TBaseModel> 
    where TBaseModel :
    BaseModel
  {
    #region Logic

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    bool Remove(uint id);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result enumerable</returns>
    IEnumerable<bool> RemoveRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result enumerable</returns>
    IEnumerable<bool> RemoveRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TBaseModel? Get(uint id);

    /// <summary>
    /// Get an enumerable of <typeparamref name="TBaseModel"/>(s) by an enumerable
    /// of ID(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TBaseModel> GetAllById(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TBaseModel> GetAntiRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TBaseModel> GetAntiRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TBaseModel> GetRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Get an enumerable of all ID(s).
    /// </summary>
    /// <returns>The enumerable of ID(s).</returns>
    IEnumerable<uint> GetAllId();

    #endregion
  }
}