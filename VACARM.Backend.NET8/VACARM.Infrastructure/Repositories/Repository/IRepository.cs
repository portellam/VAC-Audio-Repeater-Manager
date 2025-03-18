﻿using System;
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

    Action<string> OnPropertyChangedCallback { get; set; }

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
