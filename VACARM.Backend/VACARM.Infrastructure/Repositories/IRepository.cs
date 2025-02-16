namespace VACARM.Infrastructure.Repositories
{
  public interface IRepository<TItem> where TItem :
    class
  {
    #region Parameters

    Func<int, bool> IsValidIndex { get; }

    /// <summary>
    /// The maximum count of <typeparamref name="TItem"/>(s).
    /// </summary>
    int MaxCount { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Is enumerable of <typeparamref name="TItem"/> item(s) null or empty.
    /// </summary>
    /// <param name="enumerable">The enumerable</param>
    /// <returns>True/false</returns>
    bool IsNullOrEmpty(IEnumerable<TItem> enumerable);

    /// <summary>
    /// Add a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    void Add(TItem item);

    /// <summary>
    /// Add an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void AddRange(IEnumerable<TItem> enumerable);

    /// <summary>
    /// Remove a <typeparamref name="TItem"/>.
    /// </summary>
    /// <param name="func">The function</param>
    void Remove(Func<TItem, bool> func);

    /// <summary>
    /// Remove a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    void Remove(TItem item);

    /// <summary>
    /// Remove an enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    void RemoveRange(Func<TItem, bool> func);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void RemoveRange(IEnumerable<TItem> enumerable);

    #endregion
  }
}