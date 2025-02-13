using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IListService<TRepository, TItem> :
    IService<TRepository, TItem> where TRepository :
    IListRepository<TItem> where TItem :
    class
  {
  }
}