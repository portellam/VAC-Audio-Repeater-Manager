using System.ComponentModel;

namespace VACARM.Infrastructure.Repositories
{
  public interface IReadonlyRepository<TItem>
    where TItem :
    class
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;
    Func<int, bool> ContainsIndex { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Is enumerable of <typeparamref name="TItem"/>(s) null or empty.
    /// </summary>
    /// <param name="enumerable">The enumerable</param>
    /// <returns>True/false.</returns>
    bool IsNullOrEmpty(IEnumerable<TItem> enumerable);

    /// <summary>
    /// Get a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The item.</returns>
    TItem? Get(Func<TItem, bool> func);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TItem> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TItem> GetRange(Func<TItem, bool> func);

    #endregion
  }
}
