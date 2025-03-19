using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VACARM.Infrastructure.Repositories
{
  public interface IRepository
    <
      TEnumerable,
      TItem
    >
    where TEnumerable :
    IEnumerable<TItem>
    where TItem :
    class
  {
    #region Parameters

    /// <summary>
    /// Is the enumerable of all <typeparamref name="TItem"/>(s) null or empty.
    /// </summary>
    /// <returns>True/false.</returns>
    bool IsNullOrEmpty { get; }

    event PropertyChangedEventHandler PropertyChanged;
    Func<int, bool> ContainsIndex { get; }
    int Count { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Remove a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>True/false result.</returns>
    bool Remove(int index);

    /// <summary>
    /// Remove a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="itemToRemove">The item to remove</param>
    /// <returns>True/false result.</returns>
    bool Remove(TItem itemToRemove);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="indexEnumerable">The enumerable of index(es)</param>
    /// <returns>True/false result.</returns>
    IEnumerable<bool> RemoveRange(IEnumerable<int> indexEnumerable);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="enumerableToRemove">
    /// The enumerable of <typeparamref name="TItem"/>(es)
    /// </param>
    /// <returns>True/false result.</returns>
    IEnumerable<bool> RemoveRange(IEnumerable<TItem> enumerableToRemove);

    /// <summary>
    /// Get a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The item.</returns>
    TItem Get(Func<TItem, bool> func);

    /// <summary>
    /// Get the enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TItem> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TItem> GetRange(Func<TItem, bool> func);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="indexEnumerable">The enumerable of index(es)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TItem> GetRange(IEnumerable<int> indexEnumerable);

    /// <summary>
    /// Add a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="func">The function</param>
    void Add(TItem item);

    /// <summary>
    /// Add an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    void AddRange(IEnumerable<TItem> enumerable);

    #endregion
  }
}
