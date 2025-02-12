using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial interface IGenericListController<TRepository, TItem> :
    IGenericController<IGenericListRepository<TItem>, TItem> where TRepository :
    IGenericListRepository<TItem> where TItem :
    class
  {
  }
}