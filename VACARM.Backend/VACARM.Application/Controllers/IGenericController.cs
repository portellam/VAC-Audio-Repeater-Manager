using System.ComponentModel;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial interface IGenericController<T1, T2> where T1 :
    IGenericRepository<T2> where T2 :
    class
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The item.</returns>
    T2? Get(Func<T2, bool> func);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<T2> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<T2> GetRange(Func<T2, bool> func);

    /// <summary>
    /// Add a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    void Add(T2 item);

    /// <summary>
    /// Add an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void AddRange(IEnumerable<T2> enumerable);

    /// <summary>
    /// Do an action for a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoWork
    (
      Action<T2> action,
      Func<T2, bool> func
    );

    /// <summary>
    /// Do an action for a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="item">The item</param>
    void DoWork
    (
      Action<T2> action,
      T2 item
    );

    /// <summary>
    /// Do an action for an enumerable of all <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    void DoWorkAll(Action<T2> action);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void DoWorkRange
    (
      Action<T2> action,
      IEnumerable<T2> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoWorkRange
    (
      Action<T2> action,
      Func<T2, bool> func
    );

    /// <summary>
    /// Remove a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    void Remove(T2 item);

    /// <summary>
    /// Remove a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    void Remove(Func<T2, bool> func);

    /// <summary>
    /// Remove an enumerable of all <typeparamref name="T2"/> item(s).
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    void RemoveRange(Func<T2, bool> func);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void RemoveRange(IEnumerable<T2> enumerable);

    /// <summary>
    /// Update a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="item">The item</param>
    void Update
    (
      Func<T2, bool> func,
      T2 item
    );

    /// <summary>
    /// Update an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="item">The item</param>
    void UpdateRange
    (
       Func<T2, bool> func,
       T2 item
    );

    /// <summary>
    /// Update an enumerable of all <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="item">The item</param>
    void UpdateAll
    (
      T2 item
    );

    #endregion
  }
}