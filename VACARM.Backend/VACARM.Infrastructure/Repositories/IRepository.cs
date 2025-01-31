namespace VACARM.Infrastructure.Repositories
{
  public interface IRepository<T>
  {
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
    /// <returns>The <typeparamref name="T"/> enumerable of item(s).</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The <typeparamref name="T"/> enumerable of item(s).</returns>
    IEnumerable<T> GetRange(Func<T, bool> func);

    /// <summary>
    /// Add a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="t">The <typeparamref name="T"/> item</param>
    void Add(T t);

    /// <summary>
    /// Add a range of <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="t">The enumerable of <typeparamref name="T"/> item(s)</param>
    void AddRange(IEnumerable<T> enumerable);

    /// <summary>
    /// Remove a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="t">The <typeparamref name="T"/> item</param>
    void Remove(T t);

    /// <summary>
    /// Remove a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    void Remove(Func<T, bool> func);

    /// <summary>
    /// Remove all <typeparamref name="T"/> item(s).
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Remove some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    void RemoveRange(Func<T, bool> func);

    /// <summary>
    /// Remove a range of <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of <typeparamref name="T"/> items</param>
    void RemoveRange(IEnumerable<T> enumerable);

    #endregion
  }
}