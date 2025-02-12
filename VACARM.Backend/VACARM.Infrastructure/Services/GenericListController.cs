using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial class GenericListController<TRepository, TItem> : 
    GenericController<GenericRepository<TItem>, TItem>,
    IGenericListController<GenericListRepository<TItem>, TItem> where TRepository :
    GenericListRepository<TItem> where TItem :
    class
  {
    #region Parameters

    internal new virtual GenericListRepository<TItem> Repository
    {
      get
      {
        return (GenericListRepository<TItem>)base.Repository;
      }
      set
      {
        base.Repository = value;
        OnPropertyChanged(nameof(Repository));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public GenericListController()
    {
      Repository = new GenericListRepository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public GenericListController(GenericListRepository<TItem> repository) : base (repository)
    {
      Repository = repository;
    }

    #endregion
  }
}