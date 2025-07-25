using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

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