using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial class GenericListController<T1, T2> : 
    GenericController<GenericRepository<T2>, T2>,
    IGenericListController<GenericListRepository<T2>, T2> where T1 :
    GenericListRepository<T2> where T2 :
    class
  {
    #region Parameters

    internal new virtual GenericListRepository<T2> Repository
    {
      get
      {
        return (GenericListRepository<T2>)base.Repository;
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
      Repository = new GenericListRepository<T2>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public GenericListController(GenericListRepository<T2> repository) : base (repository)
    {
      Repository = repository;
    }

    #endregion
  }
}