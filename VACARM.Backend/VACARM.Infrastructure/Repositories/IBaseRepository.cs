using System.ComponentModel;

namespace VACARM.Infrastructure.Repositories
{
  public interface IBaseRepository<BaseModel>
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

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
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<BaseModel> GetRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Get an enumerable of some <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="idList">The range of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<BaseModel> GetRange(List<uint> idList);

    /// <summary>
    /// Remove a range of <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    void RemoveRange
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Remove a range of <typeparamref name="BaseModel"/> item(s).
    /// </summary>
    /// <param name="idList">The range of ID(s)</param>
    void RemoveRange(List<uint> idList);

    #endregion
  }
}