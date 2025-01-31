using System.ComponentModel;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public interface IGenericController<T>
  {
    #region Parameters

    /// <summary>
    /// The <typeparamref name="T"/> repository.
    /// </summary>
    IGenericRepository<T> Repository { get; set; }
    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The <typeparamref name="T"/> item.</returns>
    T? Get(Func<T, bool> func);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
    /// <returns>The enumerable of <typeparamref name="T"/> items.</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of <typeparamref name="T"/> items.</returns>
    IEnumerable<T> GetRange(Func<T, bool> func);

    /// <summary>
    /// Do an action for a given <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void Do
    (
      Action<T> action,
      Func<T, bool> func
    );

    /// <summary>
    /// Do an action for a given <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="t">The <typeparamref name="T"/> item</param>
    void Do
    (
      Action<T> action,
      T t
    );

    /// <summary>
    /// Do an action for all <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    void DoAll(Action<T> action);

    /// <summary>
    /// Do an action for an enumerable of <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="enumerable">The enumerable of <typeparamref name="T"/>
    /// item(s)</param>
    void DoRange
    (
      Action<T> action,
      IEnumerable<T> enumerable
    );

    /// <summary>
    /// Do an action for some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="action">The action</param>
    /// <param name="func">The function</param>
    void DoRange
    (
      Action<T> action,
      Func<T, bool> func
    );

    #endregion
  }
}