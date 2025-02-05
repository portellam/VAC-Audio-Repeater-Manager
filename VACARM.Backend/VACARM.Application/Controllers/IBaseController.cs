using System.ComponentModel;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public interface IBaseController<T1, T2> :
    IGenericListController<T1, T2> where T1 :
    IBaseRepository<T2> where T2 :
    BaseModel
  {
    #region Parameters

    new event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="BaseModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    BaseModel? Get(uint id);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<BaseModel> GetRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<BaseModel> GetRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Remove a <typeparamref name="BaseModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    void Remove(uint id);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    void RemoveRange(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Remove an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RemoveRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Update a <typeparamref name="BaseModel"/> item.
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="model">The item</param>
    void Update
    (
      Func<BaseModel, bool> func,
      BaseModel model
    );

    /// <summary>
    /// Update an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="modelEnumerable">The enumerable of item(s)</param>
    /// <param name="model">The item</param>
    void UpdateRange
    (
       IEnumerable<BaseModel> modelEnumerable,
       BaseModel model
    );

    /// <summary>
    /// Update an enumerable of all <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    void UpdateAll();

    /// <summary>
    /// Update an enumerable of all <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <param name="model">The item</param>
    void UpdateAll
    (
      Func<BaseModel, bool> func,
      BaseModel model
    );

    #endregion
  }
}