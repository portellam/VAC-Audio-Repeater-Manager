using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IBaseRepository<TBaseModel>
    where TBaseModel :
    class,
    IBaseModel
  {
    #region Parameters

    Func<int, bool> IsValidIndex { get; }
    IEnumerable<uint> DeselectedIdEnumerable { get; }
    int MaxCount { get; }
    ObservableCollection<uint> SelectedIdEnumerable { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>True/false result.</returns>
    bool Remove(Func<TBaseModel, bool> func);

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result.</returns>
    bool Remove(uint id);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>True/false result enumerable.</returns>
    IEnumerable<bool> RemoveRange(Func<TBaseModel, bool> func);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>True/false result enumerable.</returns>
    IEnumerable<bool> RemoveRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Remove an enumerable of <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>True/false result enumerable.</returns>
    IEnumerable<bool> RemoveRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TBaseModel Get(uint id);

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
    /// Add a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="model">The item.</param>
    void Add(TBaseModel model);

    /// <summary>
    /// Add an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void AddRange(IEnumerable<TBaseModel> enumerable);

    /// <summary>
    /// Deselect a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="item">The item</param>
    void Deselect(TBaseModel item);

    /// <summary>
    /// Deselect a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Deselect(uint id);

    /// <summary>
    /// Deselect an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    void DeselectRange(Func<TBaseModel, bool> func);

    /// <summary>
    /// Deselect an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void DeselectRange(IEnumerable<TBaseModel> enumerable);

    /// <summary>
    /// Deselect an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void DeselectRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Deselect the enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    void DeselectAll();

    /// <summary>
    /// Select a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="func">The function</param>
    void Select(Func<TBaseModel, bool> func);

    /// <summary>
    /// Select a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    void Select(uint id);

    /// <summary>
    /// Select a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="item">The item</param>
    void Select(TBaseModel item);

    /// <summary>
    /// Select an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    void SelectRange(Func<TBaseModel, bool> func);

    /// <summary>
    /// Select an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void SelectRange(IEnumerable<TBaseModel> enumerable);

    /// <summary>
    /// Select an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void SelectRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Select the enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    void SelectAll();

    #endregion
  }
}