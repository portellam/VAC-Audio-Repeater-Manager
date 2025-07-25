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
    /// Remove a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    Task<bool> RemoveAsync(int id);

    /// <summary>
    /// Save a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>True/false result</returns>
    Task<bool> SaveAsync(IBaseModel model);

    /// <summary>
    /// Update a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>True/false result</returns>
    Task<bool> UpdateAsync(IBaseModel model);

    /// <summary>
    /// Validate a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>True/false result</returns>
    Task<bool> ValidateAsync(int id);

    /// <summary>
    /// Create a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>The model</returns>
    Task<IBaseModel> CreateAsync(IBaseModel model);

    /// <summary>
    /// Import a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The model</returns>
    Task<IBaseModel> ImportAsync(int id);

    /// <summary>
    /// Validate a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The model</returns>
    Task<IBaseModel> ValidateAsync(Func<IBaseModel, bool> matchFunc);

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
    /// Get an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<IBaseModel> GetRangeAsync
    (Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The ID enumerable</param>
    /// <returns>The enumerable</returns>
    IAsyncEnumerable<IBaseModel> GetRangeAsync
    (IEnumerable<int> idEnumerable);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="IBaseModel"/>(s).
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