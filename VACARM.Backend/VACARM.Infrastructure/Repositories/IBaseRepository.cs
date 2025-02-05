using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IBaseRepository<T> :
    IGenericListRepository<T> where T :
    BaseModel
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="BaseModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    IBaseModel? Get(uint id);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<IBaseModel> GetRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Get an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<IBaseModel> GetRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Add a <typeparamref name="BaseModel"/> item.
    /// </summary>
    /// <param name="model"></param>
    void Add(IBaseModel model);

    /// <summary>
    /// Remove a <typeparamref name="BaseModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    void Remove(uint id);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="id">The ID</param>
    void RemoveRange(uint id);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RemoveRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Remove an enumerable of <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void RemoveRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Update a <typeparamref name="BaseModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="model">The item</param>
    void Update
    (
      uint id,
      IBaseModel model
    );

    /// <summary>
    /// Update an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <param name="model">The item</param>
    void UpdateRange
    (
       IEnumerable<uint> idEnumerable,
       IBaseModel model
    );

    #endregion
  }
}