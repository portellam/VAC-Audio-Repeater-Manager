using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

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

    #endregion
  }
}