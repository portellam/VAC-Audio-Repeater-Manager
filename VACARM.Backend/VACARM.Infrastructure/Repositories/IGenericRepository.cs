using System.ComponentModel;

namespace VACARM.Infrastructure.Repositories
{
  public interface IGenericRepository<TItem> where TItem : 
    class
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// The maximum count of <typeparamref name="TItem"/>(s).
    /// </summary>
    int MaxCount { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Is enumerable null or empty.
    /// </summary>
    /// <param name="enumerable">The enumerable</param>
    /// <returns>True/false</returns>
    bool IsNullOrEmpty(IEnumerable<TItem> enumerable);

    /// <summary>
    /// True/false is the index valid.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>True/false</returns>
    bool IsValidIndex(int index);

    /// <summary>
    /// Get a <typeparamref name="TItem"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The item.</returns>
    TItem? Get(Func<TItem, bool> func);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <returns>The enumerable of(s).</returns>
    IEnumerable<TItem> GetAll();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of(s).</returns>
    IEnumerable<TItem> GetRange(Func<TItem, bool> func);

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