using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial interface IGenericController<T1, T2> where T1 :
    IGenericRepository<T2> where T2 :
    class
  {
    #region Logic

    /// <summary>
    /// Do an action for a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    Task<bool> DoWorkAsync
    (
      Func<T2, Task<bool>> actionFunc,
      Func<T2, bool> matchFunc
    );

    /// <summary>
    /// Do an action for a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="item">The item</param>
    Task<bool> DoWorkAsync
    (
      Func<T2, Task<bool>> actionFunc,
      T2 item
    );

    /// <summary>
    /// Do an action for an enumerable of all <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    IAsyncEnumerable<bool> DoWorkAllAsync(Func<T2, Task<bool>> actionFunc);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<T2, Task<bool>> actionFunc,
      IEnumerable<T2> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<T2, Task<bool>> actionFunc,
      Func<T2, bool> matchFunc
    );

    #endregion
  }
}