using VACARM.Domain.Models;

namespace VACARM.Application.Services
{
  public interface IBaseService<TRepository, TBaseModel> where TBaseModel :
    BaseModel
  {
    #region Logic

    /// <summary>
    /// Get enumerable of <typeparamref name="TBaseModel"/>(s) by an enumerable
    /// of ID(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<TBaseModel> GetAllById(IEnumerable<uint> idEnumerable);

    #endregion
  }
}