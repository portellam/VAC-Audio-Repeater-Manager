using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

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

    #endregion
  }
}