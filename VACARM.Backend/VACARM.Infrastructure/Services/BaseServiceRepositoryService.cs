using VACARM.Application.Services;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// <c>
  /// "Good morning saar.
  /// Please do not redeem this <typeparamref name="saarvice"/> more than once..."
  /// </c>
  /// </summary>
  public class BaseServiceRepositoryService
    <
      TBaseServiceRepository,
      TBaseService,
      TBaseRepository,
      TBaseModel
    > :
    IDisposable
    where TBaseServiceRepository :
    BaseServiceRepository
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >,
      TBaseModel
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TBaseModel>,
      TBaseModel
    >
    where TBaseRepository :
    BaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region

    #endregion
  }
}
