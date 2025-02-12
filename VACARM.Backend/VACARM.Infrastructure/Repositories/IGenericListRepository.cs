namespace VACARM.Infrastructure.Repositories
{
  public interface IGenericListRepository<TItem> :
    IGenericRepository<TItem> where TItem :
    class
  {
    #region Logic
    
    /// <summary>
    /// Get a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The item.</returns>
    TItem? Get(int index);

    /// <summary>
    /// Get the index of a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The index.</returns>
    int? GetIndex(Func<TItem, bool> func);

    /// <summary>
    /// Get the index of a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The index.</returns>
    int? GetIndex(TItem item);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="indexEnumerable">The enumerable of index(es)</param>
    /// <returns>The list of item(s).</returns>
    IEnumerable<TItem> GetRange(IEnumerable<int> indexEnumerable);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="startIndex">The first index</param>
    /// <param name="endIndex">The last index</param>
    /// <returns>The list of item(s).</returns>
    IEnumerable<TItem> GetRange
    (
      int startIndex,
      int endIndex
    );

    /// <summary>
    /// Insert a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="index">The index</param>
    /// <param name="item">The item</param>
    void Insert
    (
      int index,
      TItem item
    );

    /// <summary>
    /// Remove a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="index">The index</param>
    void Remove(int index);

    /// <summary>
    /// Remove a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    void Remove(Func<TItem, bool> func);

    /// <summary>
    /// Remove a list of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="indexEnumerable">The enumerable of index(es)</param>
    void RemoveRange(IEnumerable<int> indexEnumerable);

    /// <summary>
    /// Remove a list of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="startIndex">The first index</param>
    /// <param name="endIndex">The last index</param>
    void RemoveRange
    (
      int startIndex,
      int endIndex
    );

    /// <summary>
    /// Update a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="item">The item</param>
    void Update
    (
      Func<TItem, bool> func,
      TItem item
    );

    /// <summary>
    /// Update a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="index">The index</param>
    /// <param name="item">The item</param>
    void Update
    (
      int index,
      TItem item
    );

    /// <summary>
    /// Update a list of some <typeparamref name="TItem"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="item">The item</param>
    void UpdateRange
    (
       Func<TItem, bool> func,
       TItem item
    );

    #endregion
  }
}