using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public interface IBaseService<TRepository, TBaseModel> :
    IGenericListService<TRepository, TBaseModel> where TRepository :
    IBaseRepository<TBaseModel> where TBaseModel :
    BaseModel
  {
  }
}