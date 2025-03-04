using System.ComponentModel;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial interface IReadonlyService
    <
      TRepository, 
      TItem
    > where TRepository :
    ReadonlyRepository<TItem> 
    where TItem :
    class
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Do an action for a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoAction
    (
      Action<TItem> action,
      Func<TItem, bool> func
    );

    /// <summary>
    /// Do an action for a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="item">The item</param>
    void DoAction
    (
      Action<TItem> action,
      TItem item
    );

    /// <summary>
    /// Do an action for the enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="action">The action</param>
    void DoActionAll(Action<TItem> action);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void DoActionRange
    (
      Action<TItem> action,
      IEnumerable<TItem> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoActionRange
    (
      Action<TItem> action,
      Func<TItem, bool> func
    );

    #endregion
  }
}