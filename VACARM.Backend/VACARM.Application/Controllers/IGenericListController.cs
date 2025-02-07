using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial interface IGenericListController<T1, T2> :
    IGenericController<IGenericListRepository<T2>, T2> where T1 :
    IGenericListRepository<T2> where T2 :
    class
  {
  }
}