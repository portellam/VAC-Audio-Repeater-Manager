using System.ComponentModel;

namespace VACARM.Infrastructure.Repositories
{
  public interface IGenericRepository<T> where T : class
  {
    #region Parameters

    IEnumerable<T> Enumerable { get; set; }

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
    /// Add an enumerable of some <typeparamref name="T"/> item(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void AddRange(IEnumerable<T> enumerable);

    /// <summary>
    /// Remove an enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
    void RemoveAll();

    #endregion
  }
}