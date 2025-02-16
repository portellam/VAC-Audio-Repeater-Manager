using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IRepeaterRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
  }
}