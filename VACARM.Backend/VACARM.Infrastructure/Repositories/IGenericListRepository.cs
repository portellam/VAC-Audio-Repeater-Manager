namespace VACARM.Infrastructure.Repositories
{
  public interface IGenericListRepository<T> :
    IGenericRepository<T> where T :
    class
  {
    #region Parameters

    IList<T> List { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The item.</returns>
    T? Get(int index);

    /// <summary>
    /// Get the index of a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The index.</returns>
    int? GetIndex(Func<T, bool> func);

    /// <summary>
    /// Get the index of a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The index.</returns>
    int? GetIndex(T item);

    /// <summary>
    /// Get a enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="indexEnumerable">The enumerable of index(es)</param>
    /// <returns>The list of item(s).</returns>
    IEnumerable<T> GetRange(IEnumerable<int> indexEnumerable);

    /// <summary>
    /// Get a enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="startIndex">The first index</param>
    /// <param name="endIndex">The last index</param>
    /// <returns>The list of item(s).</returns>
    IEnumerable<T> GetRange
    (
      int startIndex,
      int endIndex
    );

    /// <summary>
    /// Insert a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="index">The index</param>
    /// <param name="item">The item</param>
    void Insert
    (
      int index,
      T item
    );

    /// <summary>
    /// Remove a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="index">The index</param>
    void Remove(int index);

    /// <summary>
    /// Remove a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    void Remove(T item);

    /// <summary>
    /// Remove a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    void Remove(Func<T, bool> func);

    /// <summary>
    /// Remove a list of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    void RemoveRange(Func<T, bool> func);

    /// <summary>
    /// Remove a list of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="indexEnumerable">The enumerable of index(es)</param>
    void RemoveRange(IEnumerable<int> indexEnumerable);

    /// <summary>
    /// Remove a list of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void RemoveRange(IEnumerable<T> enumerable);

    /// <summary>
    /// Remove a list of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="startIndex">The first index</param>
    /// <param name="endIndex">The last index</param>
    void RemoveRange
    (
      int startIndex,
      int endIndex
    );

    /// <summary>
    /// Update a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="item">The item</param>
    void Update
    (
      Func<T, bool> func,
      T item
    );

    /// <summary>
    /// Update a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="index">The index</param>
    /// <param name="item">The item</param>
    void Update
    (
      int index,
      T item
    );

    /// <summary>
    /// Update a list of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="item">The item</param>
    void UpdateRange
    (
       Func<T, bool> func,
       T item
    );

    #endregion
  }
}