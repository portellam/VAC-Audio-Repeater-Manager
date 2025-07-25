namespace VACARM.Infrastructure.Services
{
  public partial interface IRepeaterGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TRepeaterModel
    >
  {
    #region Logic

    /// <summary>
    /// Restart a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<int?> RestartAsync(uint? id);

    /// <summary>
    /// Restart a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<int?> StartAsync(uint? id);

    /// <summary>
    /// Stop a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    Task<int?> StopAsync(uint? id);

    /// <summary>
    /// Restart all <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    IAsyncEnumerable<int?> RestartAllAsync();

    /// <summary>
    /// Restart some <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    IAsyncEnumerable<int?> RestartRangeAsync(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Restart some <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    IAsyncEnumerable<int?> RestartRangeAsync
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Start all <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    IAsyncEnumerable<int?> StartAllAsync();

    /// <summary>
    /// Start some <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    IAsyncEnumerable<int?> StartRangeAsync
    (IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Start some <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    IAsyncEnumerable<int?> StartRangeAsync
    (
      uint startId,
      uint endId
    );

    /// <summary>
    /// Stop all <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    IAsyncEnumerable<int?> StopAllAsync();

    /// <summary>
    /// Stop some <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    IAsyncEnumerable<int?> StopRangeAsync(IEnumerable<uint> idEnumerable);

    /// <summary>
    /// Stop some <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    IAsyncEnumerable<int?> StopRangeAsync
    (
      uint startId,
      uint endId
    );

    #endregion
  }
}