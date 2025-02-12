using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IGenericListService<TRepository, TItem> :
    IGenericService<TRepository, TItem> where TRepository :
    IGenericListRepository<TItem> where TItem :
    class
  {
  }
}