using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class GenericListService<TRepository, TItem> :
    GenericService<ListRepository<TItem>, TItem>,
    IGenericListService<ListRepository<TItem>, TItem> where TRepository :
    ListRepository<TItem> where TItem :
    class
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public GenericListService()
    {
      base._Repository = new ListRepository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    [ExcludeFromCodeCoverage]
    public GenericListService(ListRepository<TItem> repository)
    {
      base._Repository = repository;
    }

    #endregion
  }
}