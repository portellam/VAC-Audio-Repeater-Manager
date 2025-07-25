using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

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

    #endregion
  }
}