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
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    Task<bool> DoWorkAsync
    (
      Action<T2> action,
      Func<T2, bool> func
    );

    /// <summary>
    /// Do an action for a <typeparamref name="T2"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="item">The item</param>
    Task<bool> DoWorkAsync
    (
      Action<T2> action,
      T2 item
    );

    /// <summary>
    /// Do an action for an enumerable of all <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    IAsyncEnumerable<bool> DoWorkAllAsync(Action<T2> action);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Action<T2> action,
      IEnumerable<T2> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="T2"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Action<T2> action,
      Func<T2, bool> func
    );

    #endregion
  }
}