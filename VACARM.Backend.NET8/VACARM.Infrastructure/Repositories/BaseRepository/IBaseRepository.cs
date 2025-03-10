using System;
using System.Collections.Generic;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories.BaseRepository
{
  public interface IBaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    Func<int, bool> IsValidIndex { get; }
    IEnumerable<uint> DeselectedIdEnumerable { get; }
    HashSet<uint> SelectedIdHashSet { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>True/false result</returns>
    bool Remove(Func<TBaseModel, bool> func);

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="item">The item</param>
    /// <returns>True/false result</returns>
    bool Remove(TBaseModel item);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>True/false result enumerable</returns>
    IEnumerable<bool> RemoveRange(Func<TBaseModel, bool> func);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <returns>True/false result enumerable</returns>
    IEnumerable<bool> RemoveRange(IEnumerable<TBaseModel> enumerable);

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TBaseModel Get(uint id);

    /// <summary>
    /// Add a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="model"></param>
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
    /// Deselect the enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    void DeselectAll();

    /// <summary>
    /// Remove the enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Select a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="func">The function</param>
    void Select(Func<TBaseModel, bool> func);

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
    /// Select the enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    void SelectAll();

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