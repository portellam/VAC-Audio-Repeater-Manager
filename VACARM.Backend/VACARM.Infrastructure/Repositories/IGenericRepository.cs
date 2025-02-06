using System.ComponentModel;

namespace VACARM.Infrastructure.Repositories
{
  public interface IGenericRepository<T> where T : class
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// The maximum count of <typeparamref name="T"/> item(s).
    /// </summary>
    int MaxCount { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Is enumerable null or empty.
    /// </summary>
    /// <param name="enumerable">The enumerable</param>
    /// <returns>True/false</returns>
    bool IsNullOrEmpty(IEnumerable<T> enumerable);

    /// <summary>
    /// Is index valid.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>True/false</returns>
    bool IsValidIndex(int index);

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
    /// Add an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void AddRange(IEnumerable<T> enumerable);

    /// <summary>
    /// Remove a <typeparamref name="T"/> item.
    /// </summary>
    /// <param name="item">The item</param>
    void Remove(T item);

    /// <summary>
    /// Remove an enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    void RemoveRange(Func<T, bool> func);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void RemoveRange(IEnumerable<T> enumerable);

    #endregion
  }
}