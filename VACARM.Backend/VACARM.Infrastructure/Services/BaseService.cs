using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public class BaseService<TRepository, TBaseModel> :
    ListService<TRepository, TBaseModel>,
    IBaseService<TRepository, TBaseModel> where TRepository :
    BaseRepository<TBaseModel> where TBaseModel :
    BaseModel
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public BaseService()
    {
      base._Repository = new BaseRepository<TBaseModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    [ExcludeFromCodeCoverage]
    public BaseService(BaseRepository<TBaseModel> repository)
    {
      base._Repository = repository;
    }

    #endregion
  }
}