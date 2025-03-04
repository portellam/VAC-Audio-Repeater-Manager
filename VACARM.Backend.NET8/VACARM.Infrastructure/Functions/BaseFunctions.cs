using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Functions
{
  internal static class BaseFunctions<TBaseModel> 
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/> ID.
    /// </summary>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, uint> GetId
    {
      get
      {
        return (TBaseModel model) => model.Id;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Match a <typeparamref name="TBaseModel"/> ID.
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, bool> ContainsId(TBaseModel model)
      => (TBaseModel model) => model.Id == model.Id;

    /// <summary>
    /// Match a <typeparamref name="TBaseModel"/> ID.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, bool> ContainsId(uint? id)
    {
      return (TBaseModel model) => model.Id == id;
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
      return (TBaseModel model) =>
        model.Id >= startId
        && model.Id <= endId;
    }

    /// <summary>
    /// Match an enumerable of <typeparamref name="TBaseModel"/> ID(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, bool> ContainsIdEnumerable
    (IEnumerable<uint> idEnumerable)
    {
      return (TBaseModel model) => idEnumerable.Contains(model.Id);
    }

    /// <summary>
    /// Reverse match a range of <typeparamref name="TBaseModel"/> ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, bool> NotContainsIdRange
    (
      uint? startId,
      uint? endId
    )
    {
      return (TBaseModel model) =>
        !(
          model.Id >= startId
          && model.Id <= endId
        );
    }

    /// <summary>
    /// Reverse match an enumerable of <typeparamref name="TBaseModel"/> ID(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseModel, bool> NotContainsIdEnumerable
    (IEnumerable<uint> idEnumerable)
    {
      return (TBaseModel model) => !idEnumerable.Contains(model.Id);
    }

    #endregion
  }
}