using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Parameters

    int MinId { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Export a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    Task<bool> ExportAsync(int id);

    /// <summary>
    /// Export an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> ExportRangeAsync(Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Export an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> ExportRangeAsync(IEnumerable<int> idEnumerable);

    /// <summary>
    /// Export an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> ExportRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Export an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> ExportAllAsync();

    /// <summary>
    /// Remove a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    Task<bool> RemoveAsync(int id);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> RemoveRangeAsync(Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> RemoveRangeAsync(IEnumerable<int> idEnumerable);

    /// <summary>
    /// Remove an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> RemoveRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Remove an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> RemoveAllAsync();

    /// <summary>
    /// Save a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>True/false result</returns>
    Task<bool> SaveAsync(IBaseModel model);

    /// <summary>
    /// Save an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> SaveRangeAsync(IEnumerable<IBaseModel> enumerable);

    /// <summary>
    /// Update a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>True/false result</returns>
    Task<bool> UpdateAsync(IBaseModel model);

    /// <summary>
    /// Update an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> UpdateRangeAsync(IEnumerable<IBaseModel> enumerable);

    /// <summary>
    /// Validate a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    Task<bool> ValidateAsync(int id);

    /// <summary>
    /// Validate a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>True/false result</returns>
    Task<bool> ValidateAsync(Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Validate an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> ValidateRangeAsync(Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Validate an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> ValidateRangeAsync(IEnumerable<int> idEnumerable);

    /// <summary>
    /// Validate an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> ValidateRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Validate an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<bool> ValidateAllAsync();

    /// <summary>
    /// Create a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>The model</returns>
    Task<IBaseModel> CreateAsync(IBaseModel model);

    /// <summary>
    /// Create an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<IBaseModel> CreateRangeAsync
    (IEnumerable<IBaseModel> enumerable);

    /// <summary>
    /// Get a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The model</returns>
    Task<IBaseModel?> GetAsync(int id);

    /// <summary>
    /// Get a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The model</returns>
    Task<IBaseModel?> GetAsync(Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Get an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<IBaseModel> GetRangeAsync(Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Get an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The ID enumerable</param>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<IBaseModel> GetRangeAsync(IEnumerable<int> idEnumerable);

    /// <summary>
    /// Get an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<IBaseModel> GetRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Get an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<IBaseModel> GetAllAsync();

    /// <summary>
    /// Import a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The model</returns>
    Task<IBaseModel> ImportAsync(int id);

    /// <summary>
    /// Import an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<IBaseModel> ImportRangeAsync(Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Import an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<IBaseModel> ImportRangeAsync(IEnumerable<int> idEnumerable);

    /// <summary>
    /// Import an enumerable of <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<IBaseModel> ImportRangeAsync
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Import an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>True/false result</returns>
    IAsyncEnumerable<IBaseModel> ImportAllAsync();

    /// <summary>
    /// Do an action for a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The result code</returns>
    Task<int?> DoActionAsync
    (
      Func<IBaseModel, Task<int?>> actionFunc,
      Func<IBaseModel, bool> matchFunc
    );

    /// <summary>
    /// Do an action for a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="baseModel">The item</param>
    /// <returns>The result code</returns>
    Task<int?> DoActionAsync
    (
      Func<IBaseModel, Task<int?>> actionFunc,
      IBaseModel baseModel
    );

    /// <summary>
    /// Do an action for the enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionAllAsync
    (Func<IBaseModel, Task<int?>> actionFunc);

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="enumerable">The enumerable of model(s)</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<IBaseModel, Task<int?>> actionFunc,
      IEnumerable<IBaseModel> enumerable
    );

    /// <summary>
    /// Do an action for an enumerable of some <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="actionFunc">The action function</param>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The result code</returns>
    IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<IBaseModel, Task<int?>> actionFunc,
      Func<IBaseModel, bool> matchFunc
    );

    #endregion
  }
}