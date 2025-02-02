﻿using System.ComponentModel;

namespace VACARM.Application.Controllers
{
  public interface IGenericController<T> where T : class
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Do an action for a given <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The true/false result.</returns>
    Task<bool> DoWorkAsync
    (
      Func<T, Task<bool>> actionFunc,
      Func<T, bool> matchFunc
    );

    /// <summary>
    /// Do an action for a given <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="t">The item</param>
    /// <returns>The true/false result.</returns>
    Task<bool> DoWorkAsync
    (
      Func<T, Task<bool>> actionFunc,
      T t
    );

    /// <summary>
    /// Do an action for all <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <returns>The true/false result.</returns>
    IAsyncEnumerable<bool> DoWorkAllAsync(Func<T, Task<bool>> actionFunc);

    /// <summary>
    /// Do an action for an enumerable of <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <returns>The true/false result enumerable.</returns>
    IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<T, Task<bool>> actionFunc,
      IEnumerable<T> enumerable
    );

    /// <summary>
    /// Do an action for some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The true/false result enumerable.</returns>
    IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<T, Task<bool>> actionFunc,
      Func<T, bool> matchFunc
    );

    /// <summary>
    /// Get a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The item.</returns>
    T? Get(Func<T, bool> func);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<T> GetRange(Func<T, bool> func);

    /// <summary>
    /// Add a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="t">The item</param>
    void Add(T t);

    /// <summary>
    /// Add a range of <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="t">The enumerable of item(s)</param>
    void AddRange(IEnumerable<T> enumerable);

    /// <summary>
    /// Do an action for a given <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoWork
    (
      Action<T> action,
      Func<T, bool> func
    );

    /// <summary>
    /// Do an action for a given <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="t">The item</param>
    void DoWork
    (
      Action<T> action,
      T t
    );

    /// <summary>
    /// Do an action for all <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    void DoWorkAll(Action<T> action);

    /// <summary>
    /// Do an action for an enumerable of <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void DoWorkRange
    (
      Action<T> action,
      IEnumerable<T> enumerable
    );

    /// <summary>
    /// Do an action for some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoWorkRange
    (
      Action<T> action,
      Func<T, bool> func
    );

    /// <summary>
    /// Remove a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="t">The item</param>
    void Remove(T t);

    /// <summary>
    /// Remove a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    void Remove(Func<T, bool> func);

    /// <summary>
    /// Remove all <typeparamref name="T"/> item(s).
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Remove some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    void RemoveRange(Func<T, bool> func);

    /// <summary>
    /// Remove a range of <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void RemoveRange(IEnumerable<T> enumerable);

    #endregion
  }
}