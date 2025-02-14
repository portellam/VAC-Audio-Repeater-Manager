using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IService<TRepository, TItem> where TRepository :
    Repository<TItem> where TItem :
    class
  {
    #region Logic

    /// <summary>
    /// Do an action for a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    Task<bool> DoWorkAsync
    (
      Func<TItem, Task<bool>> actionFunc,
      Func<TItem, bool> matchFunc
    );

    /// <summary>
    /// Do an action for a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="item">The item</param>
    Task<bool> DoWorkAsync
    (
      Func<TItem, Task<bool>> actionFunc,
      TItem item
    );

    /// <summary>
    /// Do an action for an enumerable of all <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    IAsyncEnumerable<bool> DoWorkAllAsync(Func<TItem, Task<bool>> actionFunc);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<TItem, Task<bool>> actionFunc,
      IEnumerable<TItem> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<TItem, Task<bool>> actionFunc,
      Func<TItem, bool> matchFunc
    );

    #endregion
  }
}