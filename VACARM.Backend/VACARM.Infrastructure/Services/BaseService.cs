using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public class BaseService<TRepository, TBaseModel> :
    ReadonlyService<TRepository, TBaseModel>,
    IBaseService<TRepository, TBaseModel> where TRepository :
    BaseRepository<TBaseModel> where TBaseModel :
    BaseModel
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public BaseService() :
      base()
    {
      this.Repository = new BaseRepository<TBaseModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    [ExcludeFromCodeCoverage]
    public BaseService(BaseRepository<TBaseModel> repository) :
      base(repository)
    {
      this.Repository = repository;
    }

    public IEnumerable<TBaseModel> GetAllById(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return this.Repository
        .GetRange(func);
    }

    #endregion
  }
}