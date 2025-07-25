namespace VACARM.Infrastructure.Services
{
  public partial class Service
  {
    #region Parameters

    int MinId { get; set; }

    #endregion

    #region Logic

    Task<TBaseModel> CreateAsync();
    Task<TBaseModel> RemoveAsync();

    /// <summary>
    /// Validate a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    Task<bool> ValidateAsync(int id);

    /// <summary>
    /// Validate a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The model</returns>
    Task<TBaseModel> ValidateAsync(Func<TBaseModel, bool> matchFunc);

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The model</returns>
    Task<TBaseModel?> GetAsync(int id);

    /// <summary>
    /// Get a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The model</returns>
    Task<TBaseModel?> GetAsync(Func<TBaseModel, bool> matchFunc);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<TBaseModel> GetRange
    (Func<TBaseModel, bool> matchFunc);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The ID enumerable</param>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<TBaseModel> GetRange
    (IEnumerable<int> idEnumerable);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<TBaseModel> GetRange
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Get an enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<TBaseModel> GetAll();

    /// <summary>
    /// Do an action for a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The result code</returns>
    Task<int?> DoActionAsync
    (
      Func<TBaseModel, Task<int?>> actionFunc,
      Func<TBaseModel, bool> matchFunc
    );

    /// <summary>
    /// Do an action for a <typeparamref name="TBaseModel"/>.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="baseModel">The item</param>
    /// <returns>The result code</returns>
    Task<int?> DoActionAsync
    (
      Func<TBaseModel, Task<int?>> actionFunc,
      TBaseModel baseModel
    );

    /// <summary>
    /// Do an action for the enumerable of all <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionAllAsync
    (Func<TBaseModel, Task<int?>> actionFunc);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="enumerable">The enumerable of model(s)</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<TBaseModel, Task<int?>> actionFunc,
      IEnumerable<TBaseModel> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="TBaseModel"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<TBaseModel, Task<int?>> actionFunc,
      Func<TBaseModel, bool> matchFunc
    );

    #endregion
  }
}