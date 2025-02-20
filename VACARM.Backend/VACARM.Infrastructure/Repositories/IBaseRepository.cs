using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IBaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    Func<int, bool> IsValidIndex { get; }
    int MaxCount { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>True/false result</returns>
    bool Remove(Func<TBaseModel, bool> func);

    /// <summary>
    /// Remove a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="item">The item</param>
    /// <returns>True/false result</returns>
    bool Remove(TBaseModel item);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>True/false result enumerable</returns>
    IEnumerable<bool> RemoveRange(Func<TBaseModel, bool> func);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    /// <returns>True/false result enumerable</returns>
    IEnumerable<bool> RemoveRange(IEnumerable<TBaseModel> enumerable);

    /// <summary>
    /// Add a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="model"></param>
    void Add(TBaseModel model);

    /// <summary>
    /// Add an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="item">The enumerable of item(s)</param>
    void AddRange(IEnumerable<TBaseModel> enumerable);

    /// <summary>
    /// Remove the enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Update a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="model">The item</param>
    void Update(TBaseModel model);

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    void UpdateRange(IEnumerable<TBaseModel> enumerable);

    #endregion
  }
}