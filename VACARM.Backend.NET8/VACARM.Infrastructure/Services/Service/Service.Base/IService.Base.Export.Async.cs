using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
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

    #endregion
  }
}