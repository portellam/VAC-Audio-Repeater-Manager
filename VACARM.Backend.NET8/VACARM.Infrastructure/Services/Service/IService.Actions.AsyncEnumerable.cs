using System;
using System.Collections.Generic;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
    <
      TEnumerable,
      TItem
    >
  {
    #region Logic

    /// <summary>
    /// Do an action for the enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionAllAsync(Func<TItem, Task<int?>> actionFunc);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<TItem, Task<int?>> actionFunc,
      IEnumerable<TItem> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<TItem, Task<int?>> actionFunc,
      Func<TItem, bool> matchFunc
    );

    #endregion
  }
}