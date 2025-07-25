using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

    #region Validate

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

    #endregion
  }
}