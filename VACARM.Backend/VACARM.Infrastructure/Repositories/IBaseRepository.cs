using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IBaseRepository<TBaseModel> where TBaseModel :
    BaseModel
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TBaseModel? Get(uint id);

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
    /// Add a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="model"></param>
    void Add(TBaseModel model);

    /// <summary>
    /// Add an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void AddRange(IEnumerable<TBaseModel> enumerable);

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Remove(uint id);

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="func">The function</param>
    void Remove(Func<TBaseModel, bool> func);

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="item">The item</param>
    void Remove(TBaseModel item);

    /// <summary>
    /// Remove an enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    void RemoveRange(Func<TBaseModel, bool> func);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void RemoveRange(IEnumerable<TBaseModel> enumerable);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="id">The ID</param>
    void RemoveRange(uint id);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RemoveRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void RemoveRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Update a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="model">The item</param>
    void Update(TBaseModel model);

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void UpdateRange(IEnumerable<TBaseModel> enumerable);

    #endregion
  }
}