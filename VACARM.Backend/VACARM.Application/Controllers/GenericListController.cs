using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial class GenericListController<T1, T2> : 
    GenericController<T1, T2>,
    IGenericListController<T1, T2> where T1 :
    GenericListRepository<T2> where T2 :
    class
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public GenericListController()
    {
      Repository = new GenericRepository<T2>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public GenericListController(IGenericRepository<T2> repository)
    {
      Repository = repository;
    }

    #endregion
  }
}