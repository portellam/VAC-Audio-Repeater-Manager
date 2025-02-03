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
    /// Insert a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    void Insert(T item);

    /// <summary>
    /// Insert a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    void Insert(Func<T, bool> func);

    /// <summary>
    /// Insert an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    void InsertRange(Func<T, bool> func);

    /// <summary>
    /// Insert an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void InsertRange(IEnumerable<T> enumerable);

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
    /// Remove an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    void RemoveRange(Func<T, bool> func);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void RemoveRange(IEnumerable<T> enumerable);

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
    /// Update an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="item">The item</param>
    void UpdateRange
    (
       Func<T, bool> func,
       T item
    );

    /// <summary>
    /// Update an enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="item">The item</param>
    void UpdateAll
    (
      T item
    );

    #endregion
  }
}