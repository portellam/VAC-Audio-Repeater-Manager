using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IListService<TRepository, TItem> where TRepository :
    ListRepository<TItem> where TItem :
    class
  {
  }
}