using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

    /// <summary>
    /// Restart a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<int?> RestartAsync(int? id);

    /// <summary>
    /// Restart a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<int?> StartAsync(int? id);

    /// <summary>
    /// Stop a <typeparamref name="RepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<int?> StopAsync(int? id);

    /// <summary>
    /// Restart all <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    IAsyncEnumerable<int?> RestartAllAsync();

    /// <summary>
    /// Restart some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    IAsyncEnumerable<int?> RestartRangeAsync(IEnumerable<int> idEnumerable);

    /// <summary>
    /// Restart some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    IAsyncEnumerable<int?> RestartRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Start all <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    IAsyncEnumerable<int?> StartAllAsync();

    /// <summary>
    /// Start some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    IAsyncEnumerable<int?> StartRangeAsync
    (IEnumerable<int> idEnumerable);

    /// <summary>
    /// Start some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    IAsyncEnumerable<int?> StartRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Stop all <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    IAsyncEnumerable<int?> StopAllAsync();

    /// <summary>
    /// Stop some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    IAsyncEnumerable<int?> StopRangeAsync(IEnumerable<int> idEnumerable);

    /// <summary>
    /// Stop some <typeparamref name="RepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    IAsyncEnumerable<int?> StopRangeAsync
    (
      int startId,
      int endId
    );

    #endregion
  }
}