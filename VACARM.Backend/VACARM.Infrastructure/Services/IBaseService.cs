using VACARM.Domain.Models;

namespace VACARM.Application.Services
{
  public interface IBaseService<TRepository, TBaseModel> where TBaseModel :
    BaseModel
  {
  }
}