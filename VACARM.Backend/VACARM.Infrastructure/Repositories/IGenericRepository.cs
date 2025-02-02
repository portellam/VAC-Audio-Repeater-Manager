using System.ComponentModel;

namespace VACARM.Infrastructure.Repositories
{
  public interface IGenericRepository<T> where T : class
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The item.</returns>
    T? Get(Func<T, bool> func);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<T> GetRange(Func<T, bool> func);

    /// <summary>
    /// Add a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    void Add(T item);

    /// <summary>
    /// Add a range of <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void AddRange(IEnumerable<T> enumerable);

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
    /// Update a range of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="item">The item</param>
    void UpdateRange
    (
       Func<T, bool> func,
       T item
    );

    /// <summary>
    /// Update all <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="item">The item</param>
    void UpdateAll
    (
      T item
    );

    #endregion
  }
}