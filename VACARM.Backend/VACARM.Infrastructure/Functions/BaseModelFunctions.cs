using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Functions
{
  internal class BaseModelFunctions<TBaseModel> where TBaseModel : 
    BaseModel
  {
    #region Logic

    /// <summary>
    /// Match a <typeparamref name="TBaseModel"/> item ID.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The function</returns>
    internal static Func<TBaseModel, bool> ContainsId(uint? id)
    {
      return (TBaseModel item) => item.Id == id;
    }

    /// <summary>
    /// Match a range of <typeparamref name="TBaseModel"/> item ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The function</returns>
    internal static Func<TBaseModel, bool> ContainsIdRange
    (
      uint startId,
      uint endId
    )
    {
      return (TBaseModel item) =>
        item.Id >= startId
        && item.Id <= endId;
    }

    /// <summary>
    /// Match an enumerable of <typeparamref name="TBaseModel"/> item ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable function(s)</returns>
    internal static IEnumerable<Func<TBaseModel, bool>> ContainsIdEnumerable
    (IEnumerable<uint> idEnumerable)
    {
      foreach (var item in idEnumerable)
      {
        yield return ContainsId(item);
      }
    }   

    #endregion
  }
}