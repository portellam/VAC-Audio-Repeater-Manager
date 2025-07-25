using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial interface IService
  {
    #region Logic

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

    #endregion
  }
}