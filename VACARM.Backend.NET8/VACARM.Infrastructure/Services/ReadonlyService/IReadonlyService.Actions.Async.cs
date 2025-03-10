using System;
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
    /// Do an action for a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The result code</returns>
    Task<int?> DoActionAsync
    (
      Func<TItem, Task<int?>> actionFunc,
      Func<TItem, bool> matchFunc
    );

    /// <summary>
    /// Do an action for a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="item">The item</param>
    /// <returns>The result code</returns>
    Task<int?> DoActionAsync
    (
      Func<TItem, Task<int?>> actionFunc,
      TItem item
    );

    #endregion
  }
}