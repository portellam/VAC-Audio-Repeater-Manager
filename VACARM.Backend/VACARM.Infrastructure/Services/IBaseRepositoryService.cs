using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public interface IBaseRepositoryService
    <
      TService,
      TRepository,
      TBaseModel
    >
    where TService :
    BaseService<TRepository, TBaseModel>
    where TRepository :
    BaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
  }
}