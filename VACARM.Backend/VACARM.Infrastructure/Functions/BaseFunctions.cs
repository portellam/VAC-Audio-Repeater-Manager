using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Functions
{
  internal static class BaseFunctions<TBaseModel> where TBaseModel :
    BaseModel
  {
    /// <summary>
    /// Match a <typeparamref name="TBaseModel"/> ID.
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, bool> ContainsId(this TBaseModel model)
      => (TBaseModel item) => item.Id == model.Id;

    #region Logic

    internal static Func<TBaseModel, bool> ContainsId(uint? id)
    {
      return (TBaseModel item) => item.Id == id;
    }

    /// <summary>
    /// Match a range of <typeparamref name="TBaseModel"/> ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, bool> ContainsIdRange
    (
      uint? startId,
      uint? endId
    )
    {
      return (TBaseModel item) =>
        item.Id >= startId
        && item.Id <= endId;
    }

    /// <summary>
    /// Match an enumerable of <typeparamref name="TBaseModel"/> ID(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, bool> ContainsIdEnumerable
    (IEnumerable<uint> idEnumerable)
    {
      return (TBaseModel item) => idEnumerable.Contains(item.Id);
    }

    #endregion
  }
}