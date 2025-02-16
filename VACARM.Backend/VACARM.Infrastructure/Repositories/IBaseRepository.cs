﻿using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IBaseRepository<TBaseModel> where TBaseModel :
    BaseModel
  {
    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TBaseModel? Get(uint id);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TBaseModel"/> item(s).
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
    /// Get an enumerable of some <typeparamref name="TBaseModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Add a <typeparamref name="TBaseModel"/> item.
    /// </summary>
    /// <param name="model"></param>
    void Add(TBaseModel model);

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    void Remove(uint id);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/> item(s).
    /// </summary>
    /// <param name="id">The ID</param>
    void RemoveRange(uint id);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RemoveRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void RemoveRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Update a <typeparamref name="TBaseModel"/> item.
    /// </summary>
    /// <param name="model">The item</param>
    void Update(TBaseModel model);

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TBaseModel"/> item(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void UpdateRange(IEnumerable<TBaseModel> enumerable);

    #endregion
  }
}