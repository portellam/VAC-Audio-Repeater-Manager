using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Services;

namespace VACARM.Infrastructure.Functions
{
  internal class BaseServiceFunctions
    <
      TBaseService,
      TBaseModel
    >
    where TBaseService :
    BaseService<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    /// <summary>
    /// Get a <typeparamref name="TBaseService"/> ID.
    /// </summary>
    /// <returns>The function.</returns>
    internal static Func<TBaseService, uint> GetId
    {
      get
      {
        return (TBaseService baseService) =>
          BaseFunctions<BaseModel>.GetId(baseService.Model);
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Match a <typeparamref name="TBaseService"/> ID.
    /// </summary>
    /// <param name="baseService">The service</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseService, bool> ContainsId(TBaseService baseService)
    {
      return ContainsId(GetId(baseService));
    }

    /// <summary>
    /// Match a <typeparamref name="TBaseService"/> ID.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseService, bool> ContainsId(uint id)
    {
      return (TBaseService baseService) => GetId(baseService) == id;
    }

    /// <summary>
    /// Match a range of <typeparamref name="TBaseService"/> ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseService, bool> ContainsIdRange
    (
      uint? startId,
      uint? endId
    )
    {
      return (TBaseService baseService) =>
        GetId(baseService) >= startId
        && GetId(baseService) <= endId;
    }

    /// <summary>
    /// Match an enumerable of <typeparamref name="TBaseService"/> ID(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseService, bool> ContainsIdEnumerable
    (IEnumerable<uint> idEnumerable)
    {
      return (TBaseService baseService) => idEnumerable.Contains
        (GetId(baseService));
    }

    /// <summary>
    /// Reverse match a range of <typeparamref name="TBaseService"/> ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseService, bool> NotContainsIdRange
    (
      uint? startId,
      uint? endId
    )
    {
      return (TBaseService baseService) =>
        !(
          GetId(baseService) >= startId
          && GetId(baseService) <= endId
        );
    }

    /// <summary>
    /// Reverse match an enumerable of <typeparamref name="TBaseService"/> ID(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The function.</returns>
    internal static Func<TBaseService, bool> NotContainsIdEnumerable
    (IEnumerable<uint> idEnumerable)
    {
      return (TBaseService baseService) => !idEnumerable.Contains
        (GetId(baseService));
    }

    #endregion
  }
}