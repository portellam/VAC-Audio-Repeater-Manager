﻿using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public interface IBaseGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TBaseModel
    >
    where TGroupReadonlyRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TBaseModel>,
      TBaseModel
    >
    where TBaseRepository :
    BaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    BaseService<BaseRepository<TBaseModel>, TBaseModel>? SelectedService { get; }
    int MaxCount { get; }
    int SelectedIndex { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Add a <typeparam><typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="baseService">The service</param>
    /// <returns>True/false result.</returns>
    bool Add(BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService);

    /// <summary>
    /// Remove a <typeparam><typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>True/false result.</returns>
    bool Remove(int index);

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    bool Remove(uint id);

    /// <summary>
    /// Get a <typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The repository.</returns>
    BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index);

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

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TBaseModel? Get(uint id);

    /// <summary>
    /// Deselect a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Deselect(uint id);

    /// <summary>
    /// Deselect an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void DeselectRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Select a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Select(uint id);

    /// <summary>
    /// Select an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void SelectRange(IEnumerable<uint> idEnumerable);

    #endregion
  }
}