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

    Task<IBaseModel> CreateAsync();
    Task<IBaseModel> RemoveAsync();
    Task<IBaseModel> ValidateAsync();

    /// <summary>
    /// Get a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The model</returns>
    Task<IBaseModel> Get(int id);

    /// <summary>
    /// Get a <typeparamref name="IBaseModel"/>.
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The model</returns>
    Task<IBaseModel> Get(Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="matchFunc">The match function</param>
    /// <returns>The enumerable</returns>
    Task<IEnumerable<IBaseModel>> GetRange
    (Func<IBaseModel, bool> matchFunc);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The ID enumerable</param>
    /// <returns>The enumerable</returns>
    Task<IEnumerable<IBaseModel>> GetRange
    (IEnumerable<int> idEnumerable);

    /// <summary>
    /// Get an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The enumerable</returns>
    Task<IEnumerable<IBaseModel>> GetRange
    (
      int startId,
      int endId
    );

    /// <summary>
    /// Get an enumerable of all <typeparamref name="IBaseModel"/>(s).
    /// </summary>
    /// <returns>The enumerable</returns>
    Task<IEnumerable<IBaseModel>> GetAll();

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