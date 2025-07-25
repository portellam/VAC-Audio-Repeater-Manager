using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

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

    #endregion
  }
}