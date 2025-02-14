using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class ListService<TRepository, TItem> :
    Service<ListRepository<TItem>, TItem>,
    IListService<ListRepository<TItem>, TItem> where TRepository :
    ListRepository<TItem> where TItem :
    class
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public ListService()
    {
      base.WritableRepository = new ListRepository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    [ExcludeFromCodeCoverage]
    public ListService(ListRepository<TItem> repository)
    {
      base.WritableRepository = repository;
    }

    #endregion
  }
}