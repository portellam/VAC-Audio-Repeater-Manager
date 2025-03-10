using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface IReadonlyService
    <
      TRepository, 
      TItem
    >
  {
    #region Logic

    /// <summary>
    /// Do an action for the enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <returns>The enumerable of result code(s).</returns>
    Task<IEnumerable<int?>> DoActionAll(Func<TItem, Task<int?>> actionFunc);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <returns>The enumerable of result code(s).</returns>
    Task<IEnumerable<int?>> DoActionRange
    (
      Func<TItem, Task<int?>> actionFunc,
      IEnumerable<TItem> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The enumerable of result code(s).</returns>
    Task<IEnumerable<int?>> DoActionRange
    (
      Func<TItem, Task<int?>> actionFunc,
      Func<TItem, bool> matchFunc
    );


    #endregion
  }
}