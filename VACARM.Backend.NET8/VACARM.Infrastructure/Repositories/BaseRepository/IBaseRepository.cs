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
    ObservableCollection<uint> SelectedIdEnumerable { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    TBaseModel Get(uint id);

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

    #endregion
  }
}