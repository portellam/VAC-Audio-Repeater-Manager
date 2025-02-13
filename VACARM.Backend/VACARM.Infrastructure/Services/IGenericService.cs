using System.ComponentModel;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IGenericService<TRepository, TItem> where TRepository :
    IRepository<TItem> where TItem :
    class
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;
    TRepository Repository { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Do an action for a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoWork
    (
      Action<TItem> action,
      Func<TItem, bool> func
    );

    /// <summary>
    /// Do an action for a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="item">The item</param>
    void DoWork
    (
      Action<TItem> action,
      TItem item
    );

    /// <summary>
    /// Do an action for an enumerable of all <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    void DoWorkAll(Action<TItem> action);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void DoWorkRange
    (
      Action<TItem> action,
      IEnumerable<TItem> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoWorkRange
    (
      Action<TItem> action,
      Func<TItem, bool> func
    );

    #endregion
  }
}